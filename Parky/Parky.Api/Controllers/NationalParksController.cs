using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parky.Api.Models;
using Parky.Api.Models.Dtos;
using Parky.Api.Repository.IRepository;

namespace Parky.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParksController : Controller
    {
        private readonly INationalParkRepository _npRepo;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository npRepo, IMapper mapper)
        {
            _npRepo = npRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of national parks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var allNationalParks = _npRepo.GetNationalParks();
            var result = allNationalParks.Select(nationalPark => _mapper.Map<NationalPark>(nationalPark));
            return Ok(result);
        }

        /// <summary>
        /// Get individual national park
        /// </summary>
        /// <param name="nationalParkId">The Id of the national park</param>
        /// <returns></returns>
        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var nationalPark = _npRepo.GetNationalPark(nationalParkId);
            if (nationalPark == null)
                return NotFound();

            var result = _mapper.Map<NationalParkDto>(nationalPark);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalPark)
        {
            if (nationalPark == null)
                return BadRequest(ModelState);

            if (_npRepo.NationalParkExists(nationalPark.Name))
            {
                ModelState.AddModelError("", "National Park Exists!");
                return StatusCode(404, ModelState);
            }

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            var nationalParkObj = _mapper.Map<NationalPark>(nationalPark);

            if (!_npRepo.CreateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark", new { nationalParkId = nationalParkObj.Id },nationalParkObj);
        }

        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        public IActionResult UpdateNationalPark(int nationalParkId, [FromBody] NationalParkDto nationalPark)
        {

            if (nationalPark == null || nationalParkId != nationalPark.Id)
                return BadRequest(ModelState);

            var nationalParkObj = _mapper.Map<NationalPark>(nationalPark);

            if (!_npRepo.UpdateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when update the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{nationalParkId:int}", Name = "DeleteNationalPark")]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {

            if (! _npRepo.NationalParkExists(nationalParkId))
                return NotFound();

            var nationalPark = _npRepo.GetNationalPark(nationalParkId);

            if (!_npRepo.DeleteNationalPark(nationalPark))
            { 
                ModelState.AddModelError("", $"Something went wrong when delete the record {nationalPark.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
