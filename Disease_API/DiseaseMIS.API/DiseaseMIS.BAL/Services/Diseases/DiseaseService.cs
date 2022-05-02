using DiseaseMIS.BAL.Configurations;
using DiseaseMIS.BAL.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiseaseMIS.BAL.Services
{
    class DiseaseService : IDiseaseService, IDisposable
    {
        readonly ApplicationDbContext _context;
        readonly ILogger<DiseaseService> _logger;

        public DiseaseService(ApplicationDbContext context,
            ILogger<DiseaseService> logger)
        {
            _context = context;
            _logger = logger;

        }

        public async Task<bool> Create(List<Disease> diseases, CancellationToken ct = default)
        {
            if (diseases.Count <= 0)
                throw new Exception("Invalid Data");

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                MetaDataHelper.SetBaseData(diseases);
                foreach (var (disease, symptom) in
                    from disease in diseases
                    from symptom in disease.Symptoms
                    select (disease, symptom))
                {
                    symptom.Disease = disease;
                    disease.ListingOrder = await _context.Diseases.CountAsync() + 1;
                    disease.DiseaseType = await _context.DiseaseType.FindAsync(disease.DiseaseType.Id);
                }

                await _context.Diseases.AddRangeAsync(diseases);
                await _context.SaveChangesAsync();
                transaction.Commit();
                _logger.LogInformation($"{diseases.Count} created");
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError("Error saving object => ", ex);
                return false;
            }
        }

        public async Task<ServiceResult> Delete(Guid? id, CancellationToken ct = default)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var disease = await _context.Diseases.Include(a => a.Symptoms).
              SingleOrDefaultAsync(a => a.Id == id);

            var _checkForms = await _context.FormDiseaseValues
               .Include(a => a.Disease)
               .Include(a => a.Symptom)
               .AnyAsync(a => a.Disease.Id == disease.Id);

            if (_checkForms)
            {
                return new ServiceResult
                {
                    Errors = new List<string>
                    {
                        $"Could not delete {disease.Name} since multiple forms are dependent on this Disease"
                    }
                };
            }


            if (disease.Symptoms.Count > 0)
            {
                _context.Symptoms.RemoveRange(disease.Symptoms);
            }

            _context.Diseases.Remove(disease);
            return new ServiceResult
            {
                Errors = await _context.SaveChangesAsync() > 0 ? null : new List<string>
                {
                    $"Error deleting Disease: {disease.Name}. Try again later."
                }
            };
        }

        public async Task<bool> Edit(Guid? id, Disease disease, CancellationToken ct = default)
        {
            Disease data = (Disease)await Get(id);
            if (data == null)
                throw new Exception("Disease not found");

            data.Name = disease.Name;
            data.DiseaseType = await _context.DiseaseType.FindAsync(disease.DiseaseType.Id);

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var valueChanges = data.Symptoms.
                    Join(disease.Symptoms, r => r.Id, p => p.Id, (r, p) => p).ToList();

                _context.DetachLocal(data, data.Id.ToString());
                _context.Symptoms.RemoveRange(data.Symptoms);

                await _context.SaveChangesAsync();

                if (valueChanges.Count == 0)
                {
                    data.Symptoms = disease.Symptoms;
                }
                else
                {
                    foreach (var item in valueChanges)
                    {
                        item.Disease = data;
                        _context.Symptoms.Add(item);
                    }
                }

                MetaDataHelper.UpdateBaseData(data);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex.Message);
                return false;
            }

        }

        public async Task<object> Get(Guid? id, CancellationToken ct = default)
        {
            if (id == null)
            {
                return await _context.Diseases.Include(a => a.DiseaseType)
                    .Include(a => a.Symptoms).ThenInclude(a => a.SubSymptoms)
                    .OrderBy(a => a.ListingOrder)
                    .ToListAsync();
            }

            var item = await _context.Diseases.Include(a => a.DiseaseType)
                    .Include(a => a.Symptoms).ThenInclude(a => a.SubSymptoms).
                    SingleOrDefaultAsync(a => a.Id == id);

            if (item == null)
                throw new Exception("Item not found with provided Id");
            return item;
        }

        public async Task<ServiceResult> UpdateListingOrder(List<Disease> diseases, CancellationToken ct = default)
        {
            foreach (var item in diseases)
            {
                var _disease = await _context.Diseases.SingleOrDefaultAsync(a => a.Id == item.Id);
                _disease.ListingOrder = item.ListingOrder;
            }
            await _context.SaveChangesAsync(ct);
            return new ServiceResult("Saved");
        }

        public async Task<List<DiseaseType>> GetCategory(CancellationToken ct = default)
        {
            return await _context.DiseaseType.ToListAsync();
        }

        public async Task<List<Disease>> GetDiseasesByCategory(int? categoryId, CancellationToken ct = default)
        {
            if (categoryId == null)
                return null;
            return await _context.Diseases.Include(a => a.DiseaseType)
                .Include(a => a.Symptoms)
                .ThenInclude(a => a.SubSymptoms)
                .Where(a => a.DiseaseType.Id == categoryId)
                .OrderBy(a => a.ListingOrder)
                .ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}