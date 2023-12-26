using AutoMapper;
using LPS.DocumentUploader.Application.CustomExceptions;
using LPS.DocumentUploader.Application.Helpers;
using LPS.DocumentUploader.Application.Services.Logins.Dto;
using LPS.DocumentUploader.Application.Services.Users.Dto;
using LPS.DocumentUploader.Database.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS.DocumentUploader.Application.Services.Logins
{
    public class LoginAppService : ILoginAppService, IDisposable
    {
        private readonly LPSDBContext _databaseContext;
        private IMapper? _mapper;

        public LoginAppService(LPSDBContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<LoginDto> Login(LoginDto model)
        {
            try
            {
                var user = _databaseContext.Users.FirstOrDefault(w => w.UserName == model.UserName);

                if (user != null)
                {
                    string Password = CryptographyHelper.GenerateHashWithSalt(model.Password, user.PasswordSalt);
                    user = _databaseContext.Users.FirstOrDefault(w => w.UserName == model.UserName && w.Password.Equals(Password));

                    return await Task.Run(() => user != null
                            ? _mapper.Map<LoginDto>(user)
                            : new LoginDto());
                }
                else
                {
                    return await Task.Run(() => new LoginDto());
                }
            }
            catch (UserException ex)
            {
                throw new UserException(ex.Message);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _databaseContext?.Dispose();
                _mapper = null;
            }
        }
    }
}
