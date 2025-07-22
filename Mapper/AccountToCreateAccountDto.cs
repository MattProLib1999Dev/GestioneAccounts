using AutoMapper;
using GestioneAccounts.BE.Domain.Models;

public class CreateAccountToCreateAccountDto : Profile
{
    public CreateAccountToCreateAccountDto()
    {
        CreateMap<Account, CreateAccountDto>()
          .ForMember(dest => dest.LockoutEnd, opt => opt.MapFrom(src => src.LockoutEnd.HasValue ? src.LockoutEnd.Value.DateTime : (DateTime?)null));

    }
}
