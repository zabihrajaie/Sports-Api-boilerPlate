using ApiSportsBoilerPlate.Data.Entity;
using ApiSportsBoilerPlate.DTO;
using ApiSportsBoilerPlate.DTO.Request;
using ApiSportsBoilerPlate.DTO.Response;
using AutoMapper;

namespace ApiSportsBoilerPlate.Infrastructure.Configs
{
    public class MappingProfileConfiguration : Profile
    {
        public MappingProfileConfiguration()
        {
            CreateMap<Person, CreatePersonRequest>().ReverseMap();
            CreateMap<Person, UpdatePersonRequest>().ReverseMap();
            CreateMap<Person, PersonQueryResponse>().ReverseMap();
        }
    }
}
