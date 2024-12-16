using AutoMapper;
using BankAccounstMicroservice.Models.Dtos;
using BankAccountsMicroservice.Models;

namespace BankAccounstMicroservice.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountRequestDto, Account>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<MovementRequestDto, Movement>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
