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

        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var allNationalParks = _npRepo.GetNationalParks();
            var result = allNationalParks.Select(nationalPark => _mapper.Map<NationalPark>(nationalPark));
            return Ok(result);
        } 

        [HttpGet("{nationalParkId:int}")]

        public IActionResult GetNationalPark(int nationalParkId)
        {
            var nationalPark = _npRepo.GetNationalPark(nationalParkId);
            if (nationalPark == null)
                return NotFound();

            var result = _mapper.Map<NationalParkDto>(nationalPark);
            return Ok(result);
        }

    }
}
