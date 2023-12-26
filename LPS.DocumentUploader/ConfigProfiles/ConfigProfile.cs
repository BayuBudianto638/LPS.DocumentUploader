using AutoMapper;
using LPS.DocumentUploader.Application.Services.Documents.Dto;
using LPS.DocumentUploader.Application.Services.Logins.Dto;
using LPS.DocumentUploader.Application.Services.Users.Dto;
using LPS.DocumentUploader.Models;

namespace LPS.DocumentUploader.ConfigProfiles
{
    public class ConfigProfile: Profile
    {
        public ConfigProfile()
        {
            CreateMap<UserDto, UserModel>();
            CreateMap<UserModel, UserDto>();

            CreateMap<CreateUserDto, UserModel>();
            CreateMap<UserModel, CreateUserDto>();

            CreateMap<UpdateUserDto, UserModel>();
            CreateMap<UserModel, UpdateUserDto>();

            CreateMap<LoginDto, LoginModel>();
            CreateMap<LoginModel, LoginDto>();

            CreateMap<DocumentDto, DocumentViewModel>();
            CreateMap<DocumentViewModel, DocumentDto>();
        }
    }
}
