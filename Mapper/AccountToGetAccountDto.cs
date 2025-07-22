using AutoMapper;
using GestioneAccounts.BE.Domain.Models;

public class AccountToGetAccountDto : Profile
{
    public AccountToGetAccountDto()
    {

    CreateMap<GetAccountDto, Account>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
        .ForMember(dest => dest.Role, opt => opt.Ignore());


    }
}
