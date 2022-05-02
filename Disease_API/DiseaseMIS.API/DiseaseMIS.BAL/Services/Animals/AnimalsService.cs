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
    sealed class AnimalsService : IAnimalService, IDisposable
    {
        readonly ApplicationDbContext _context;
        readonly ILogger<AnimalsService> _logger;

        public AnimalsService(ApplicationDbContext context,
            ILogger<AnimalsService> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<bool> Create(List<Animals> animals, CancellationToken ct = default)
        {
            if (animals.Count <= 0)
                throw new Exception("Invalid Data");

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                MetaDataHelper.SetBaseData(animals);
                foreach (var animal in animals)
                {
                    animal.ListingOrder = await _context.Animals.CountAsync() + 1;
                    animal.Category = await _context.AnimalCategories.FindAsync(animal.Category.Id);
                }
                await _context.Animals.AddRangeAsync(animals);
                await _context.SaveChangesAsync();
                transaction.Commit();
                _logger.LogInformation($"{animals.Count} created");
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError("Error saving object => ", ex);
                return false;
            }
        }

        public async Task<ServiceResult> Delete(Guid id, CancellationToken ct = default)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var animal = await _context.Animals.FindAsync(id);

            _context.Animals.Remove(animal);
            return new ServiceResult
            {
                Errors = await _context.SaveChangesAsync() > 0 ? null : new List<string>
                {
                    $"Error deleting District: {animal.Name}. Try again later."
                }
            };
        }

        public async Task<bool> Edit(Guid id, Animals animals, CancellationToken ct = default)
        {
            Animals data = (Animals)await Get(id);
            if (data == null)
                throw new Exception("District not found");

            data.Name = animals.Name;
            data.Category = await _context.AnimalCategories.FindAsync(animals.Category.Id);

            MetaDataHelper.UpdateBaseData(data);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<object> Get(Guid? id, CancellationToken ct = default)
        {
            if (id == null)
            {
                return await _context.Animals
                    .Include(a => a.Category)
                    .OrderBy(a => a.ListingOrder)
                    .ToListAsync();
            }

            var item = await _context.Animals.Include(a => a.Category).SingleOrDefaultAsync(a => a.Id == id);
            if (item == null)
                throw new Exception("Item not found with provided Id");
            return item;
        }


        #region Category
        public async Task<bool> CreateCategory(List<AnimalCategory> categories, CancellationToken ct = default)
        {
            if (categories.Count <= 0)
                throw new Exception("Invalid Data");

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                await _context.AnimalCategories.AddRangeAsync(categories);
                await _context.SaveChangesAsync();
                transaction.Commit();
                _logger.LogInformation($"{categories.Count} created");
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError("Error saving object => ", ex);
                return false;
            }
        }

        public async Task<ServiceResult> UpdateListingOrders(List<Animals> animals, CancellationToken ct = default)
        {
            foreach (var item in animals)
            {
                var _animal = await _context.Animals.SingleOrDefaultAsync(a => a.Id == item.Id);
                _animal.Category = await _context.AnimalCategories.SingleOrDefaultAsync(a => a.Id == item.Category.Id);
                _animal.ListingOrder = item.ListingOrder;
            }
            await _context.SaveChangesAsync();
            return new ServiceResult("saved");
        }

        public async Task<ServiceResult> DeleteCategory(Guid id, CancellationToken ct = default)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var category = await _context.AnimalCategories.Include(a => a.Animals).
                SingleOrDefaultAsync(a => a.Id == id);

            if (category.Animals.Count > 0)
                _context.Animals.RemoveRange(category.Animals);

            _context.AnimalCategories.Remove(category);
            return new ServiceResult
            {
                Errors = await _context.SaveChangesAsync() > 0 ? null : new List<string>
                {
                    $"Error deleting Category: {category.Name}. Try again later."
                }
            };
        }

        public async Task<bool> EditCategory(Guid id, AnimalCategory category, CancellationToken ct = default)
        {
            AnimalCategory data = (AnimalCategory)await GetCategory(id);
            if (data == null)
                throw new Exception("Animal Category not found");

            data.Name = category.Name;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<object> GetCategory(Guid? id, CancellationToken ct = default)
        {
            if (id == null)
            {
                return await _context.AnimalCategories.Include(a => a.Animals).ToListAsync();
            }

            var item = await _context.AnimalCategories.Include(a => a.Animals).SingleOrDefaultAsync(a => a.Id == id);
            if (item == null)
                throw new Exception("Item not found with provided Id");
            return item;
        }
        #endregion 
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
