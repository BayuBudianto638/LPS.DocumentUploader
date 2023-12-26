using LPS.DocumentUploader.Application.Services.Logins.Dto;
using LPS.DocumentUploader.Application.Services.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS.DocumentUploader.Application.Services.Logins
{
    public interface ILoginAppService
    {
        Task<LoginDto> Login(LoginDto model);
    }
}
