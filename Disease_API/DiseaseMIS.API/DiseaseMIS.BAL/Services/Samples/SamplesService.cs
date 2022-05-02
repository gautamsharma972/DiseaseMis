using DiseaseMIS.BAL.Configurations;
using DiseaseMIS.BAL.Core;
using DiseaseMIS.BAL.Core.MIS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiseaseMIS.BAL.Services
{
    class SamplesService : ISamplesService
    {
        readonly ApplicationDbContext _context;
        readonly ILogger<SamplesService> _logger;

        public SamplesService(ApplicationDbContext context,
            ILogger<SamplesService> logger)
        {
            _context = context;
            _logger = logger;

        }

        public async Task<bool> Create(List<Samples> samples, CancellationToken ct = default)
        {
            if (samples.Count <= 0)
                throw new Exception("Invalid Data");

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                MetaDataHelper.SetBaseData(samples);
                foreach (var (sample, testType) in
                    from sample in samples
                    from testType in sample.TestTypes
                    select (sample, testType))
                {
                    testType.Sample = sample;
                }

                await _context.Samples.AddRangeAsync(samples);
                await _context.SaveChangesAsync();
                transaction.Commit();
                _logger.LogInformation($"{samples.Count} created");
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

            var sample = await _context.Samples.Include(a => a.TestTypes)
                .ThenInclude(a => a.SubTests)
              .SingleOrDefaultAsync(a => a.Id == id);

            var _checkForms = await _context.LabFormValues
                .Include(a => a.Samples)
                .Include(a => a.TestTypes)
                .AnyAsync(a => a.Samples.Id == sample.Id);

            if (_checkForms)
            {
                return new ServiceResult
                {
                    Errors = new List<string>
                    {
                        $"Could not delete {sample.Name} since multiple forms are dependent on this sample"
                    }
                };
            }

            if (sample.TestTypes.Count > 0)
            {
                _context.TestTypes.RemoveRange(sample.TestTypes);
            }

            _context.Samples.Remove(sample);
            return new ServiceResult
            {
                Errors = await _context.SaveChangesAsync() > 0 ? null : new List<string>
                {
                    $"Error deleting Sample: {sample.Name}. Try again later."
                }
            };
        }

        public async Task<bool> Edit(Guid? id, Samples sample, CancellationToken ct = default)
        {
            Samples data = (Samples)await Get(id);
            if (data == null)
                throw new Exception("Sample not found");

            data.Name = sample.Name;

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var valueChanges = data.TestTypes.
                    Join(sample.TestTypes, r => r.Id, p => p.Id, (r, p) => p).ToList();

                _context.DetachLocal(data, data.Id.ToString());
                _context.TestTypes.RemoveRange(data.TestTypes);
                await _context.SaveChangesAsync();

                if (valueChanges.Count == 0)
                {
                    data.TestTypes = sample.TestTypes;
                }
                else
                {
                    foreach (var item in valueChanges)
                    {
                        item.Sample = data;
                        _context.TestTypes.Add(item);
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
                return await _context.Samples
                    .Include(a => a.TestTypes).ThenInclude(a => a.SubTests).
                    OrderBy(a => a.ListingOrder).ToListAsync();
            }

            var item = await _context.Samples
                    .Include(a => a.TestTypes).ThenInclude(a => a.SubTests).
                    SingleOrDefaultAsync(a => a.Id == id);

            if (item == null)
                throw new Exception("Item not found with provided Id");
            return item;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<ServiceResult> UpdateListingOrder(List<Samples> samples, CancellationToken ct = default)
        {
            foreach (var item in samples)
            {
                var _sample = await _context.Samples.SingleOrDefaultAsync(a => a.Id == item.Id);
                _sample.ListingOrder = item.ListingOrder;
            }
            await _context.SaveChangesAsync();
            return new ServiceResult("saved");
        }
    }
}