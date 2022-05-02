using DiseaseMIS.BAL.Core;
using DiseaseMIS.BAL.Core.MIS;
using DiseaseMIS.BAL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiseaseMIS.API.Controllers
{
    [Route("api/master")]
    [ApiController]
    public class MasterContoller : ControllerBase
    {
        readonly IDistrictService _districtService;
        readonly ILaboratoryService _laboratoryService;
        readonly IInchargesService _inchargesService;
        readonly IInstitutesService _instituteService;
        readonly IAnimalService _animalService;
        public MasterContoller(IDistrictService districtService,
            ILaboratoryService laboratoryService,
            IInchargesService inchargesService,
            IAnimalService animalService,
            IInstitutesService institutesService)
        {
            _districtService = districtService;
            _laboratoryService = laboratoryService;
            _inchargesService = inchargesService;
            _animalService = animalService;
            _instituteService = institutesService;
        }

        #region Districts

        [HttpGet("districts")]
        public async Task<IActionResult> GetDistricts()
        {
            return Ok(new
            {
                data = await _districtService.Get(null)
            });
        }

        [HttpGet("districts/{id?}")]
        public async Task<IActionResult> GetDistricts(Guid? id)
        {
            return Ok(new
            {
                data = await _districtService.Get(id)
            });
        }

        [HttpPut("districts/{id?}")]
        public async Task<IActionResult> PutDistrict(Guid? id, Districts district)
        {
            return Ok(await _districtService.Edit(id, district));
        }

        [HttpPost("districts")]
        public async Task<IActionResult> PostDistrict(List<Districts> districts)
        {
            return Ok(await _districtService.Create(districts));

        }

        [HttpDelete("districts/{id?}")]
        public async Task<IActionResult> DeleteDistrict(Guid? id)
        {
            var result = await _districtService.Delete(id);
            return Ok(result);
        }

        #endregion Districts

        #region Incharges

        [HttpGet("incharges/{id?}")]
        public async Task<IActionResult> GetIncharges(Guid? id)
        {
            return Ok(new
            {
                data = await _inchargesService.Get(id)
            });
        }

        [HttpGet("incharges")]
        public async Task<IActionResult> GetIncharges()
        {
            return Ok(new
            {
                data = await _inchargesService.Get(null)
            });
        }

        [HttpPost("incharges")]
        public async Task<IActionResult> PostIncharges(List<Incharges> incharges)
        {
            var isSaved = await _inchargesService.Create(incharges);
            if (!isSaved)
                throw new Exception("Could not save incharges");
            return Ok();

        }

        [HttpPut("incharges/{id?}")]
        public async Task<IActionResult> PutIncharge(Guid? id, Incharges incharge)
        {
            var isSaved = await _inchargesService.Edit(id, incharge);
            if (!isSaved)
                throw new Exception("Could not save incharges");
            return Ok();
        }

        [HttpDelete("incharges/{id?}")]
        public async Task<IActionResult> DeleteIncharge(Guid? id)
        {
            var isSaved = await _inchargesService.Delete(id);
            if (!isSaved)
                throw new Exception("Could not delete incharge");
            return Ok();
        }
        #endregion Incharges

        #region Institutes

        [HttpGet("Institutes")]
        public async Task<IActionResult> GetInstitutes()
        {
            return Ok(new
            {
                data = await _instituteService.Get(null)
            });
        }

        [HttpGet("Institutes/{id?}")]
        public async Task<IActionResult> GetInstitutes(Guid? id)
        {
            return Ok(new
            {
                data = await _instituteService.Get(id),
            });
        }

        [HttpPut("Institutes/{id?}")]
        public async Task<IActionResult> PutInstitutes(Guid? id, Institutes Institutes)
        {
            var isSaved = await _instituteService.Edit(id, Institutes);
            if (!isSaved)
                throw new Exception("Error occurred saving Insitutes. Try again later");
            return Ok();
        }

        [HttpPost("Institutes")]
        public async Task<IActionResult> PostInstitutes(List<Institutes> Institutes)
        {
            var isSaved = await _instituteService.Create(Institutes);
            if (!isSaved)
                throw new Exception("Error occurred saving Institutes. Try again later");
            return Ok();
        }

        [HttpDelete("Institutes/{id?}")]
        public async Task<IActionResult> DeleteInstitutes(Guid? id)
        {
            var result = await _instituteService.Delete(id);
            return Ok(result);
        }

        #endregion

        #region Laboratory

        [HttpGet("Laboratories")]
        public async Task<IActionResult> GetLaboratories()
        {
            return Ok(new
            {
                data = await _laboratoryService.Get(null)
            });
        }

        [HttpGet("Laboratories/{id?}")]
        public async Task<IActionResult> GetLaboratories(Guid? id)
        {
            return Ok(new
            {
                data = await _laboratoryService.Get(id)
            });
        }

        [HttpPut("Laboratories/{id?}")]
        public async Task<IActionResult> PutLaboratories(Guid? id, Laboratory Laboratory)
        {
            var isSaved = await _laboratoryService.Edit(id, Laboratory);
            return isSaved ? Ok() : throw new Exception("Could not save Laboratory");
        }

        [HttpPost("Laboratories")]
        public async Task<IActionResult> PostLaboratories(List<Laboratory> laboratory)
        {
            var isSaved = await _laboratoryService.Create(laboratory);
            return isSaved ? Ok() : throw new Exception("Could not save Laboratory");
        }

        [HttpDelete("Laboratories/{id?}")]
        public async Task<IActionResult> DeleteLaboratories(Guid? id)
        {
            var isSaved = await _laboratoryService.Delete(id);
            return isSaved ? Ok() : throw new Exception("Could not delete Laboratory");
        }

        #endregion Districts

        #region Animals

        [HttpGet("animals")]
        public async Task<IActionResult> GetAnimals()
        {
            return Ok(await _animalService.Get(null));
        }

        [HttpGet("animals/{id?}")]
        public async Task<IActionResult> GetAnimals(Guid? id)
        {
            return Ok(await _animalService.Get(id));
        }

        [HttpPut("animals/{id?}")]
        public async Task<IActionResult> PutAnimals(Guid? id, Animals animal)
        {
            if (!id.HasValue)
                return BadRequest(nameof(id));

            var isSaved = await _animalService.Edit(id.Value, animal);
            return isSaved ? Ok() : throw new Exception("Could not save. Try again later");
        }

        [HttpPost("animals")]
        public async Task<IActionResult> PostAnimals(List<Animals> animals)
        {
            var isSaved = await _animalService.Create(animals);
            return isSaved ? Ok() : throw new Exception("Could not save. Try again later");
        }

        [HttpDelete("animals/{id?}")]
        public async Task<IActionResult> DeleteAnimals(Guid? id)
        {
            return Ok(await _animalService.Delete(id.Value));
        }

        [HttpPost("animals/updateListingOrder")]
        public async Task<IActionResult> UpdateListingOrder(List<Animals> animals, CancellationToken ct = default)
        {
            return Ok(await _animalService.UpdateListingOrders(animals, ct));
        }
        #endregion

        #region AnimalCategory
        [HttpGet("animals/category")]
        public async Task<IActionResult> GetAnimalCategory()
        {
            return Ok(await _animalService.GetCategory(null));
        }

        [HttpGet("animals/category/{id?}")]
        public async Task<IActionResult> GetAnimalCategory(Guid? id)
        {
            return Ok(await _animalService.GetCategory(id));
        }

        [HttpPut("Animals/category/{id?}")]
        public async Task<IActionResult> PutAnimalCategory(Guid? id, AnimalCategory category)
        {
            if (!id.HasValue)
                return BadRequest(nameof(id));

            var isSaved = await _animalService.EditCategory(id.Value, category);
            return isSaved ? Ok() : throw new Exception("Could not save. Try again later");
        }

        [HttpPost("Animals/category")]
        public async Task<IActionResult> PostAnimalCategory(List<AnimalCategory> categories)
        {
            var isSaved = await _animalService.CreateCategory(categories);
            if (isSaved)
                return Ok();
            throw new Exception("Error saving Category");
        }

        [HttpDelete("Animals/category/{id?}")]
        public async Task<IActionResult> DeleteAnimalCategory(Guid? id)
        {
            return Ok(await _animalService.DeleteCategory(id.Value));
        }
        #endregion
    }
}
