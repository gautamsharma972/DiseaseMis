using DiseaseMIS.BAL.Core.MIS;
using DiseaseMIS.BAL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DiseaseMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        readonly IFormsService _formsService;
        public FormsController(IFormsService formsService)
        {
            _formsService = formsService;
        }

        [HttpPost("get")]
        public async Task<IActionResult> Get(FormFilterInput filter)
        {
            if (filter == null)
                return BadRequest("Provide form parameters");
            var _form = await _formsService.Get(filter);
            return Ok(_form);
        }

        [HttpPost]
        public async Task<IActionResult> Save(FormsInput formsInput)
        {
            return Ok(await _formsService.Create(formsInput));
        }

        [HttpPost("laboratory")]
        public async Task<IActionResult> PostLaboratory(LaboratoryFormsInput laboratoryFormsInput)
        {
            return Ok(await _formsService.Create(laboratoryFormsInput));
        }

        [HttpPost("laboratory/get")]
        public async Task<IActionResult> GetLabForm(FormFilterInput filter)
        {
            if (filter == null)
                return BadRequest("Provide form parameters");
            var _form = await _formsService.GetLabForms(filter);
            return Ok(_form);
        }

        [HttpPut("{id}/ToggleDiseaseFormLock")]
        public async Task<IActionResult> ToggleDiseaseFormLock(Guid id)
        {
            return Ok(await _formsService.ToggleDiseaseFormLock(id));
        }

        [HttpPut("{id}/ToggleLabFormLock")]
        public async Task<IActionResult> ToggleLabFormLock(Guid id)
        {
            return Ok(await _formsService.ToggleLabFormLock(id));
        }
    }
}
