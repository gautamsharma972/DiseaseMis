using DiseaseMIS.BAL.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiseaseMIS.BAL.Services
{
    public interface IDistrictService
    {
        Task<ServiceResult> Create(List<Districts> districts, CancellationToken ct = default);
        Task<ServiceResult> Delete(Guid? id, CancellationToken ct = default);
        Task<ServiceResult> Edit(Guid? id, Districts district, CancellationToken ct = default);
        Task<object> Get(Guid? id, CancellationToken ct = default);
    }
}
