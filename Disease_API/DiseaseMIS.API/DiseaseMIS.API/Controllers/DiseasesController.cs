using DiseaseMIS.BAL.Core;
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
    public class DiseasesController : ControllerBase
    {
        readonly IDiseaseService _diseaseService;
        public DiseasesController(IDiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
        }

        #region Disease
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _diseaseService.Get(null));
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get(Guid? id)
        {
            if (id == null)
                return BadRequest(nameof(id));
            var result = await _diseaseService.Get(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(List<Disease> diseases)
        {
            var result = await _diseaseService.Create(diseases);
            return Ok(result);
        }


        [HttpPut("{id?}")]
        public async Task<IActionResult> Put(Guid? id, Disease disease)
        {
            var isSaved = await _diseaseService.Edit(id, disease);
            if (!isSaved)
                throw new Exception("Error saving Disease");
            return Ok();
        }

        [HttpDelete("{id?}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            return Ok(await _diseaseService.Delete(id));
        }

        [HttpGet("byCategory/{categoryId?}")]
        public async Task<IActionResult> GetByCategory(int? categoryId)
        {
            return Ok(await _diseaseService.GetDiseasesByCategory(categoryId));
        }

        [HttpPost("updateListingOrder")]
        public async Task<IActionResult> UpdateListingOrder(List<Disease> diseases, CancellationToken ct = default)
        {
            return Ok(await _diseaseService.UpdateListingOrder(diseases, ct));
        }
        #endregion

        #region DiseaseType

        [HttpGet("category")]
        public async Task<IActionResult> GetCategory()
        {
            return Ok(await _diseaseService.GetCategory());
        }

        #endregion

    }
}
