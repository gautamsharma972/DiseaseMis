using DiseaseMIS.BAL.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiseaseMIS.BAL.Services
{
    public interface IInchargesService
    {
        Task<bool> Create(List<Incharges> incharges, CancellationToken ct = default);
        Task<bool> Delete(Guid? id, CancellationToken ct = default);
        Task<bool> Edit(Guid? id, Incharges incharge, CancellationToken ct = default);
        Task<object> Get(Guid? id, CancellationToken ct = default);
    }
}
