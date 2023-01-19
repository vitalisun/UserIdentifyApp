using AutoMapper;
using IndentifyApp.BL.Models;
using IndentifyApp.WebApp.Models;

namespace IndentifyApp.WebApp.Infrastructure;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<LoginDto, LoginViewModel>().ReverseMap();
        CreateMap<RegisterDto, RegisterViewModel>().ReverseMap();
        CreateMap<LoginDto, RegisterViewModel>().ReverseMap();
    }
}