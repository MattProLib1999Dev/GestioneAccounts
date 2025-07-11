using AutoMapper;
using GestioneAccounts.BE.Domain.Models;

public class CreateAccountToCreateAccountDto : Profile
{
    public CreateAccountToCreateAccountDto()
    {
        CreateMap<Account, CreateAccountDto>();
    }
}
