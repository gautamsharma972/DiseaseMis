using DiseaseMIS.BAL.Core.MIS;
using DiseaseMIS.BAL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiseaseMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesController : ControllerBase
    {
        readonly ISamplesService _sampleService;
        public SamplesController(ISamplesService samplesService)
        {
            _sampleService = samplesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _sampleService.Get(null));
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get(Guid? id)
        {
            if (id == null)
                return BadRequest(nameof(id));
            var result = await _sampleService.Get(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(List<Samples> samples)
        {
            var result = await _sampleService.Create(samples);
            return Ok(result);
        }


        [HttpPut("{id?}")]
        public async Task<IActionResult> Put(Guid? id, Samples samples)
        {
            var isSaved = await _sampleService.Edit(id, samples);
            if (!isSaved)
                throw new Exception("Error saving samples");
            return Ok();
        }

        [HttpDelete("{id?}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            return Ok(await _sampleService.Delete(id));
        }

        [HttpPost("updateListingOrder")]
        public async Task<IActionResult> UpdateListingOrder(List<Samples> sample, CancellationToken ct = default)
        {
            return Ok(await _sampleService.UpdateListingOrder(sample, ct));
        }
    }
}
