using AutoMapper;
using LPS.DocumentUploader.Application.Services.Documents.Dto;
using LPS.DocumentUploader.Application.Services.Users.Dto;
using LPS.DocumentUploader.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS.DocumentUploader.Application.ConfigProfiles
{
    public class ConfigurationProfiles: Profile
    {
        public ConfigurationProfiles()
        {
            CreateMap<MstDocument, DocumentDto>();

            CreateMap<MstUser, CreateUserDto>();
            CreateMap<CreateUserDto, MstUser>();

            CreateMap<MstUser, UpdateUserDto>();
            CreateMap<UpdateUserDto, MstUser>();

            CreateMap<MstUser, UserDto>();
            CreateMap<UserDto, MstUser>();

            CreateMap<MstDocument, DocumentDto>();
            CreateMap<DocumentDto, MstDocument>();
        }
    }
}
