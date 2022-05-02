using DiseaseMIS.BAL.Configurations;
using DiseaseMIS.BAL.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiseaseMIS.BAL.Services
{
    sealed class InstitutesService : IInstitutesService, IDisposable
    {
        readonly ApplicationDbContext _context;
        readonly ILogger<InchargeService> _logger;
        public InstitutesService(ApplicationDbContext db, ILogger<InchargeService> logger)
        {
            _context = db;
            _logger = logger;
        }

        public async Task<bool> Create(List<Institutes> institutes, CancellationToken ct = default)
        {
            if (institutes.Count <= 0)
                throw new Exception("Invalid Data");

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var item in institutes)
                {
                    item.District = await _context.Districts.FindAsync(item.District.Id);
                }
                MetaDataHelper.SetBaseData(institutes);
                await _context.Institutes.AddRangeAsync(institutes);
                await _context.SaveChangesAsync();
                transaction.Commit();
                _logger.LogInformation($"{institutes.Count} created");
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError("Error saving object => ", ex);
                return false;
            }
        }

        public async Task<bool> Edit(Guid? id, Institutes institute, CancellationToken ct = default)
        {
            Institutes _data = (Institutes)await Get(id);
            if (_data == null)
                throw new Exception("Institutes not found");
            _data.Name = institute.Name;
            _data.District = (Districts)await _context.Districts.FindAsync(institute.District.Id);
            MetaDataHelper.UpdateBaseData(institute);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<object> Get(Guid? id, CancellationToken ct = default)
        {
            if (id == null)
            {
                return await _context.Institutes.Include(a => a.District).ToListAsync();
            }

            if (await _context.Institutes.AnyAsync(a => a.Id == id.Value))
            {
                return await _context.Institutes.Include(a => a.District).SingleOrDefaultAsync(a => a.Id == id.Value);
            }
            return null;
        }

        public async Task<ServiceResult> Delete(Guid? id, CancellationToken ct = default)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var institute = await _context.Institutes.Include(a => a.Incharges).SingleOrDefaultAsync(a => a.Id == id);
            if (institute.Incharges.Count > 0)
            {
                return new ServiceResult
                {
                    Errors = new List<string>
                    {
                        $"{institute.Incharges.Count} Incharges are dependent on this Institute. Delete them first"
                    }
                };
            }
            _context.Institutes.Remove(institute);
            return new ServiceResult
            {
                Errors = await _context.SaveChangesAsync() > 0 ? null : new List<string>
                {
                    $"Error deleting District: {institute.Name}. Try again later."
                }
            };
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
