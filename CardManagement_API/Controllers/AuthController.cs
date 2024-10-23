using CardManagement.Application.Dtos.SysUser;
using CardManagement.Application.Service.Interface;
using CardManagement.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardManagement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISysUserService _sysUserService;

        public AuthController(ISysUserService sysUserService) =>
           _sysUserService = sysUserService ?? throw new ArgumentNullException(nameof(sysUserService));


        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            try
            {
                var result = await _sysUserService.CreateUserAsync(request);

                if (result.Succeeded)
                {
                    return Ok(new GeneralResponse<object>("200", result));
                }

                return BadRequest(new GeneralResponse<object>("404", null));
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse<bool>("Error Exception : 500"));
            }
        }

        [AllowAnonymous]
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn([FromBody] LogInRequest request)
        {
            try
            {
                var result = await _sysUserService.GetAuthenticateUser(request);

                if (!string.IsNullOrEmpty(result.ID))
                {
                    return Ok(new GeneralResponse<object>("200", result));
                }

                return BadRequest(new GeneralResponse<object>("UnAuthorized  : 401", null));
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse<bool>("Error Exception : 500"));
            }
        }

    }
}
