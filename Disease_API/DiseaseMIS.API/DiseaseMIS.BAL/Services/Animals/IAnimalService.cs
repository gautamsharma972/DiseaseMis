using DiseaseMIS.BAL.Core;
using DiseaseMIS.BAL.Core.MIS;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiseaseMIS.BAL.Services
{
    public interface IAnimalService
    {
        #region Animals
        Task<object> Get(Guid? id, CancellationToken ct = default);
        Task<bool> Edit(Guid id, Animals animals, CancellationToken ct = default);
        Task<ServiceResult> Delete(Guid id, CancellationToken ct = default);
        Task<bool> Create(List<Animals> animals, CancellationToken ct = default);
        Task<ServiceResult> UpdateListingOrders(List<Animals> animals, CancellationToken ct = default);
        #endregion

        #region Category
        Task<object> GetCategory(Guid? id, CancellationToken ct = default);
        Task<bool> EditCategory(Guid id, AnimalCategory category, CancellationToken ct = default);
        Task<ServiceResult> DeleteCategory(Guid id, CancellationToken ct = default);
        Task<bool> CreateCategory(List<AnimalCategory> categories, CancellationToken ct = default);
        #endregion
    }
}
