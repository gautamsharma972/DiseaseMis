using DiseaseMIS.BAL.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiseaseMIS.BAL.Services
{
    public interface IDiseaseService
    {
        Task<bool> Create(List<Disease> diseases, CancellationToken ct = default);
        Task<bool> Edit(Guid? id, Disease disease, CancellationToken ct = default);
        Task<ServiceResult> Delete(Guid? id, CancellationToken ct = default);
        Task<object> Get(Guid? id, CancellationToken ct = default);
        Task<List<DiseaseType>> GetCategory(CancellationToken ct = default);
        Task<List<Disease>> GetDiseasesByCategory(int? categoryId, CancellationToken ct = default);
        Task<ServiceResult> UpdateListingOrder(List<Disease> diseases, CancellationToken ct = default);
    }
}
