using AutoMapper;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<Role, RoleDto>()
        .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId));
  }

}
