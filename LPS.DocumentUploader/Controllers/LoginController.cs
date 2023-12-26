using AutoMapper;
using LPS.DocumentUploader.Application.Services.Logins;
using LPS.DocumentUploader.Application.Services.Logins.Dto;
using LPS.DocumentUploader.Application.Services.Users.Dto;
using LPS.DocumentUploader.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LPS.DocumentUploader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly ILoginAppService _loginAppService;
        private IMapper? _mapper;

        public LoginController(IConfiguration configuration, ILoginAppService loginAppService, IMapper mapper)
        {
            _configuration = configuration;
            _loginAppService = loginAppService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = await AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(UserModel userModel)
        {
            var secruityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(secruityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(1200),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UserModel?> AuthenticateUser(LoginModel userModel)
        {
            try
            {
                var userLogin = _mapper.Map<LoginDto>(userModel);
                var dataLogin = await _loginAppService.Login(userLogin);
                var login = _mapper.Map<UserModel>(dataLogin);

                return login;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
