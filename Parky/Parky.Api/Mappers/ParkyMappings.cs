using AutoMapper;
using Parky.Api.Models;
using Parky.Api.Models.Dtos;

namespace Parky.Api.Mappers
{
    public class ParkyMappings : Profile
    {
        public ParkyMappings()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
        }
    }
}