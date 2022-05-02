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
    class LaboratoryService : ILaboratoryService, IDisposable
    {
        readonly ApplicationDbContext _context;
        readonly ILogger<InchargeService> _logger;
        public LaboratoryService(ApplicationDbContext db, ILogger<InchargeService> logger)
        {
            _context = db;
            _logger = logger;
        }

        public async Task<bool> Create(List<Laboratory> laboratories, CancellationToken ct = default)
        {
            if (laboratories.Count <= 0)
                throw new Exception("Invalid Data");

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                laboratories.ForEach(async a => a.Districts = await _context.Districts.FindAsync(a.Districts.Id));
                await _context.Laboratories.AddRangeAsync(laboratories);
                MetaDataHelper.SetBaseData(laboratories);
                await _context.SaveChangesAsync();
                transaction.Commit();
                _logger.LogInformation($"{laboratories.Count}  created");
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError("Error saving object => ", ex);
                return false;
            }
        }

        public async Task<bool> Edit(Guid? id, Laboratory laboratory, CancellationToken ct = default)
        {
            Laboratory _data = (Laboratory)await Get(id);
            if (_data == null)
                throw new Exception("Laboratory not found");

            _data.Location = laboratory.Location;
            _data.Districts = await _context.Districts.FindAsync(laboratory.Districts.Id);
            MetaDataHelper.UpdateBaseData(laboratory);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<object> Get(Guid? id, CancellationToken ct = default)
        {
            if (id == null)
            {
                return await _context.Laboratories.Include(a => a.Districts).ToListAsync();
            }

            if (await _context.Laboratories.AnyAsync(a => a.Id == id.Value))
            {
                return await _context.Laboratories.Include(a => a.Districts).SingleOrDefaultAsync(a => a.Id == id.Value);
            }
            return null;
        }

        public async Task<bool> Delete(Guid? id, CancellationToken ct = default)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var labs = await _context.Laboratories.FindAsync(id);
            _context.Laboratories.Remove(labs);
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
