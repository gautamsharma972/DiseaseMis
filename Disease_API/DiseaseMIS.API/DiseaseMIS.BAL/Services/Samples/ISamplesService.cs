using DiseaseMIS.BAL.Core;
using DiseaseMIS.BAL.Core.MIS;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiseaseMIS.BAL.Services
{
    public interface ISamplesService
    {
        Task<bool> Create(List<Samples> samples, CancellationToken ct = default);
        Task<bool> Edit(Guid? id, Samples samples, CancellationToken ct = default);
        Task<ServiceResult> Delete(Guid? id, CancellationToken ct = default);
        Task<object> Get(Guid? id, CancellationToken ct = default);
        Task<ServiceResult> UpdateListingOrder(List<Samples> sample, CancellationToken ct = default);
    }
}
