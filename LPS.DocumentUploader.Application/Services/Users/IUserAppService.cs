using LPS.DocumentUploader.Application.Services.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS.DocumentUploader.Application.Services.Users
{
    public interface IUserAppService
    {
        Task<(bool, string)> Create(CreateUserDto model);
        Task<(bool, string)> Update(UpdateUserDto model);
        Task<(bool, string)> Delete(int id);
    }
}
