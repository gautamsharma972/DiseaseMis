using DiseaseMIS.BAL.Configurations;
using DiseaseMIS.BAL.Core.MIS;
using DiseaseMIS.BAL.Services.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMIS.BAL.Services
{
    class FormsService : IFormsService, IDisposable
    {
        readonly ApplicationDbContext _context;
        readonly ILogger<FormsService> _logger;
        readonly IUserService _user;

        public FormsService(ApplicationDbContext context,
            IUserService user,
            ILogger<FormsService> logger)
        {
            _context = context;
            _logger = logger;
            _user = user;
        }

        public async Task<DiseaseForms> Get(FormFilterInput filterInput)
        {
            filterInput.CreatedDate ??= DateTime.Now;

            var _form = await _context.DiseaseForms
                .Include(a => a.District)
                .Include(a => a.Institute)
                .Include(a => a.Incharge)
                .SingleOrDefaultAsync(a => a.CreatedDate.Month == filterInput.CreatedDate.Value.Month &&
                a.CreatedDate.Year == filterInput.CreatedDate.Value.Year
                && a.FormName.ToLower().Trim() == filterInput.FormName.ToLower().Trim()
                && a.District.Id == filterInput.DistrictId);

            if (_form == null)
                return null;
            _form.FormDiseaseValues = await _context.FormDiseaseValues
               .Include(a => a.Animal)
               .Include(a => a.Disease)
               .Include(a => a.Symptom)
               .ThenInclude(a => a.Disease)
               .Include(a => a.DiseaseForms)
               .Where(a => a.DiseaseForms.Id == _form.Id && a.ValueOfType == filterInput.FormType)
               .ToListAsync();

            return _form;
        }

        public async Task<LaboratoryForms> GetLabForms(FormFilterInput filterInput)
        {
            filterInput.CreatedDate ??= DateTime.Now;

            var _form = await _context.LaboratoryForms
                .Include(a => a.District)
                .SingleOrDefaultAsync(a => a.CreatedDate.Month == filterInput.CreatedDate.Value.Month &&
                a.CreatedDate.Year == filterInput.CreatedDate.Value.Year
                 && a.District.Id == filterInput.DistrictId);

            if (_form == null)
                return null;
            _form.LabFormValues = await _context.LabFormValues
               .Include(a => a.AnimalCategory).ThenInclude(a => a.Animals)
               .Include(a => a.Samples)
               .ThenInclude(a => a.TestTypes)
               .ThenInclude(a => a.SubTests)
               .Include(a => a.LaboratoryForms)
               .Where(a => a.LaboratoryForms.Id == _form.Id)
               .ToListAsync();

            return _form;
        }

        public async Task<bool> Create(FormsInput formsInput)
        {
            bool hasData = false;
            var currentMonthForm = await _context.DiseaseForms
                  .SingleOrDefaultAsync(a => a.CreatedDate.Month == formsInput.CreatedDate.Month &&
                  a.CreatedDate.Year == formsInput.CreatedDate.Year && a.FormName.ToLower().Trim()
                  == formsInput.Name.ToLower().Trim());

            hasData = currentMonthForm != null;

            currentMonthForm = await SetFormValues(formsInput, currentMonthForm);

            await SetFormDiseaseValues(formsInput, currentMonthForm);

            try
            {
                if (hasData)
                {
                    currentMonthForm.CurrentStep = formsInput.CurrentStep;
                    currentMonthForm.Remarks = formsInput.Remarks;
                    currentMonthForm.IsLocked = formsInput.CurrentStep == "remarks";
                    await _context.SaveChangesAsync();
                    return true;
                }

                await _context.DiseaseForms.AddAsync(currentMonthForm);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> Create(LaboratoryFormsInput formsInput)
        {
            bool hasData = false;
            var currentMonthForm = await _context.LaboratoryForms.Include(a => a.LabFormValues)
                  .SingleOrDefaultAsync(a => a.CreatedDate.Month == formsInput.CreatedDate.Month &&
                  a.CreatedDate.Year == formsInput.CreatedDate.Year);

            hasData = currentMonthForm != null;
            currentMonthForm = await SetLabFormValues(formsInput, currentMonthForm);
            await SetLabFormTestValues(formsInput, currentMonthForm);

            if (!hasData)
            {
                await _context.LaboratoryForms.AddAsync(currentMonthForm);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task SetLabFormTestValues(LaboratoryFormsInput formsInput, LaboratoryForms currentMonthForm)
        {
            foreach (var (category, test) in from category in formsInput.Categories
                                             from test in category.Tests
                                             select (category, test))
            {
                currentMonthForm.LabFormValues.Add(new LabFormValues
                {
                    AnimalCategory = await _context.AnimalCategories.FindAsync(category.Id),
                    NoOfPositiveCases = test.NoOfPositiveCases,
                    TotalValue = test.TotalValue,
                    TestTypes = await _context.TestTypes.FindAsync(test.TestType.Id),
                    Samples = await _context.Samples.FindAsync(test.TestType.Sample.Id),
                });
            }
        }

        public async Task<bool> ToggleDiseaseFormLock(Guid id)
        {
            var _form = await _context.DiseaseForms.FindAsync(id);
            if (_form == null)
                throw new Exception("No form found");
            _form.IsLocked = false;
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> ToggleLabFormLock(Guid id)
        {
            var _form = await _context.LaboratoryForms.FindAsync(id);
            if (_form == null)
                throw new Exception("No form found");
            _form.IsLocked = false;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<LaboratoryForms> SetLabFormValues(LaboratoryFormsInput formsInput, LaboratoryForms currentMonthForm)
        {
            if (currentMonthForm == null)
            {
                currentMonthForm = new LaboratoryForms
                {
                    CreatedDate = formsInput.CreatedDate,
                    CreatedOn = DateTime.Now,
                    District = await _context.Districts.FindAsync(formsInput.District.Id),
                    LastUpdatedOn = DateTime.Now,
                    IsLocked = true,
                    User = _user.User.Id,
                    Remarks = formsInput.Remarks
                };
            }
            else
            {
                if (currentMonthForm.LabFormValues.Count > 0)
                {
                    _context.LabFormValues.RemoveRange(currentMonthForm.LabFormValues);
                    await _context.SaveChangesAsync();
                }
            }

            return currentMonthForm;
        }

        private async Task<DiseaseForms> SetFormValues(FormsInput formsInput, DiseaseForms currentMonthForm)
        {
            if (currentMonthForm == null)
            {
                currentMonthForm = new DiseaseForms
                {
                    CreatedDate = formsInput.CreatedDate,
                    CreatedOn = DateTime.Now,
                    District = await _context.Districts.FindAsync(formsInput.District.Id),
                    Incharge = await _context.Incharges.FindAsync(formsInput.Incharge.Id),
                    Institute = await _context.Institutes.FindAsync(formsInput.Institute.Id),
                    LastUpdatedOn = DateTime.Now,
                    IsLocked = formsInput.CurrentStep == "remarks",
                    User = _user.User.Id,
                    FormName = formsInput.Name,
                    CurrentStep = formsInput.CurrentStep ?? "incidence",
                    Remarks = formsInput.Remarks
                };
            }

            return currentMonthForm;
        }

        private async Task SetFormDiseaseValues(FormsInput formsInput, DiseaseForms currentMonthForm)
        {
            if (formsInput.CurrentStep != "remarks")
            {
                foreach (var (animal, symptom) in from animal in formsInput.Animals
                                                  from symptom in animal.Symptoms
                                                  select (animal, symptom))
                {
                    currentMonthForm.FormDiseaseValues.Add(new FormDiseaseValues
                    {
                        Animal = await _context.Animals.FindAsync(animal.Id),
                        Disease = await _context.Diseases.FindAsync(symptom.Symptom.Disease.Id),
                        Symptom = await _context.Symptoms.FindAsync(symptom.Symptom.Id),
                        Value = symptom.Value,
                        ValueOfType = formsInput.FormType
                    });
                }
            }
            else
            {
                currentMonthForm.CurrentStep = "completed";
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}