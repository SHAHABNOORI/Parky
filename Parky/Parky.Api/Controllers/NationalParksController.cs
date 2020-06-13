using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(allNationalParks);
        }

    }
}
