using DiseaseMIS.BAL.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiseaseMIS.BAL.Services
{
    public interface IInstitutesService
    {
        Task<bool> Create(List<Institutes> incharges, CancellationToken ct = default);
        Task<ServiceResult> Delete(Guid? id, CancellationToken ct = default);
        Task<bool> Edit(Guid? id, Institutes incharge, CancellationToken ct = default);
        Task<object> Get(Guid? id, CancellationToken ct = default);
    }
}
