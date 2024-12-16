using AutoMapper;
using BankClientsMicroservice.Models;
using BankClientstMicroservice.Models.Dtos;

namespace BankAccounstMicroservice.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientResponseDto>();
            CreateMap<ClientRequestDto, Client>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
