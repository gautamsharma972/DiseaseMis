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
    class InchargeService : IInchargesService, IDisposable
    {
        readonly ApplicationDbContext _context;
        readonly ILogger<InchargeService> _logger;

        public InchargeService(ApplicationDbContext db,
            ILogger<InchargeService> logger)
        {
            _context = db;
            _logger = logger;
        }

        public async Task<bool> Create(List<Incharges> incharges, CancellationToken ct = default)
        {
            if (incharges.Count <= 0)
                throw new Exception("Invalid Data");

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var item in incharges)
                {
                    item.Institute = await _context.Institutes.FindAsync(item.Institute.Id);
                }
                MetaDataHelper.SetBaseData(incharges);
                await _context.Incharges.AddRangeAsync(incharges);
                await _context.SaveChangesAsync();
                transaction.Commit();
                _logger.LogInformation($"{incharges.Count}  created");
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError("Error saving object => ", ex);
                return false;
            }
        }

        public async Task<bool> Delete(Guid? id, CancellationToken ct = default)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var incharges = await _context.Incharges.FindAsync(id);

            incharges.Status = incharges.Status == Enums.Status.Archived
                ? Enums.Status.Active
                : Enums.Status.Archived;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Edit(Guid? id, Incharges incharge, CancellationToken ct = default)
        {
            Incharges _data = (Incharges)await Get(id);
            if (_data == null)
                throw new Exception("Incharges not found");

            _data.Name = incharge.Name;
            _data.Designation = incharge.Designation;
            _data.Phone = incharge.Phone;
            _data.Institute = (Institutes)await _context.Institutes.FindAsync(incharge.Institute.Id);
            MetaDataHelper.UpdateBaseData(incharge);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<object> Get(Guid? id, CancellationToken ct = default)
        {
            if (id == null)
            {
                return await _context.Incharges.Include(a => a.Institute).ToListAsync();
            }

            if (await _context.Incharges.AnyAsync(a => a.Id == id.Value))
            {
                return await _context.Incharges.Include(a => a.Institute).SingleOrDefaultAsync(a => a.Id == id.Value);
            }
            return null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
