using AutoMapper;
using LPS.DocumentUploader.Application.Helpers;
using LPS.DocumentUploader.Application.Models;
using LPS.DocumentUploader.Application.Services.Users.Dto;
using LPS.DocumentUploader.Application.Services.Users;
using LPS.DocumentUploader.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LPS.DocumentUploader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        private IMapper? _mapper;

        public UserController(IUserAppService userAppService, IMapper mapper)
        {
            _userAppService = userAppService;
            _mapper = mapper;
        }

        [HttpDelete("DeleteUser")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            try
            {
                var (isDeleted, isMessage) = await _userAppService.Delete(Id);
                if (!isDeleted)
                {
                    return Requests.Response(this, new ApiStatus(406), "Error", "Error");
                }

                return Requests.Response(this, new ApiStatus(200), "Success", "Success");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpPost("SaveUser")]
        [AllowAnonymous]
        public async Task<IActionResult> SaveUser([FromBody] UserModel model)
        {
            try
            {
                var userModel = _mapper.Map<CreateUserDto>(model);

                var (isAdded, isMessage) = await _userAppService.Create(userModel);
                if (!isAdded)
                {
                    return Requests.Response(this, new ApiStatus(406), "Error", "Error");
                }

                return Requests.Response(this, new ApiStatus(200), "Success", "Success");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpPatch("UpdateUser")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel model)
        {
            try
            {
                var userModel = _mapper.Map<UpdateUserDto>(model);
                var (isUpdated, isMessage) = await _userAppService.Update(userModel);
                if (!isUpdated)
                {
                    return Requests.Response(this, new ApiStatus(406), "Error", "Error");
                }

                return Requests.Response(this, new ApiStatus(200), "Success", "Success");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }
    }
}
