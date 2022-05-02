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
    public class DistrictsService : IDistrictService, IDisposable
    {
        readonly ApplicationDbContext _context;
        readonly ILogger<DistrictsService> _logger;
        public DistrictsService(ApplicationDbContext db,
            ILogger<DistrictsService> logger)
        {
            _context = db;
            _logger = logger;
        }

        public async Task<ServiceResult> Create(List<Districts> districts, CancellationToken ct = default)
        {
            if (districts.Count <= 0)
                throw new Exception("Invalid Data");

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                MetaDataHelper.SetBaseData(districts);
                await _context.Districts.AddRangeAsync(districts);
                await _context.SaveChangesAsync();
                transaction.Commit();
                _logger.LogInformation($"{districts.Count} created");
                return new ServiceResult("Saved");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError("Error saving object => ", ex);

                var _errors = new List<string>();
                foreach (object item in ex.InnerException.Data.Values)
                {
                    _errors.Add(item.ToString());
                }

                return new ServiceResult
                {
                    Errors = _errors
                };
            }
        }

        public async Task<ServiceResult> Edit(Guid? id, Districts district, CancellationToken ct = default)
        {
            Districts data = (Districts)await Get(id);
            if (data == null)
                throw new Exception("District not found");

            data.Name = district.Name;
            MetaDataHelper.UpdateBaseData(data);
            try
            {
                return new ServiceResult
                {
                    Errors = await _context.SaveChangesAsync() > 0 ? null : new List<string>
                    {
                        "Error saving Districts. Try again later."
                    }
                };
            }
            catch (Exception ex)
            {
                var _errors = new List<string>();
                foreach (object item in ex.InnerException.Data.Values)
                {
                    _errors.Add(item.ToString());
                }

                return new ServiceResult
                {
                    Errors = _errors
                };
                throw;
            }

        }

        public async Task<object> Get(Guid? id, CancellationToken ct = default)
        {
            if (id == null)
            {
                return await _context.Districts
                    .Include(a => a.Institutes)
                    .ThenInclude(a => a.Incharges)
                    .ToListAsync();
            }

            var item = await _context.Districts
                .Include(a => a.Institutes)
                .ThenInclude(a => a.Incharges)
                .SingleOrDefaultAsync(a => a.Id == id);
            if (item == null)
                throw new Exception("Item not found with provided Id");
            return item;
        }

        public async Task<ServiceResult> Delete(Guid? id, CancellationToken ct = default)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var district = await _context.Districts.
                Include(a => a.Institutes).SingleOrDefaultAsync(a => a.Id == id);

            if (district.Institutes.Count > 0)
            {
                return new ServiceResult
                {
                    Errors = new List<string>
                    {
                        $"{district.Institutes.Count} Institutes are dependent on this District. Delete them first"
                    }
                };
            }
            _context.Districts.Remove(district);
            return new ServiceResult
            {
                Errors = await _context.SaveChangesAsync() > 0 ? null : new List<string>
                {
                    $"Error deleting District: {district.Name}. Try again later."
                }
            };
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
