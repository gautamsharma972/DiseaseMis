using DiseaseMIS.BAL.Core.MIS;
using System;
using System.Threading.Tasks;

namespace DiseaseMIS.BAL.Services
{
    public interface IFormsService
    {
        Task<bool> Create(FormsInput formsInput);
        Task<bool> Create(LaboratoryFormsInput formsInput);
        Task<DiseaseForms> Get(FormFilterInput filterInput);
        Task<LaboratoryForms> GetLabForms(FormFilterInput filter);
        Task<bool> ToggleDiseaseFormLock(Guid id);
        Task<bool> ToggleLabFormLock(Guid id);
    }
}
