using createWebApi_DominModels.Models.DTO;
using createWebApi_DominModels.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace createWebApi_DominModels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }


        /// <summary>
        /// 註冊新用戶
        /// POST: /api/Auth/Register
        /// </summary>
        /// <param name="registerRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if(identityResult.Succeeded)
            {
                // 新增用戶的腳色權限
                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                
                    if(identityResult.Succeeded)
                    {
                        return Ok("註冊成功");
                    }
                }
            }

            return BadRequest("帳號或密碼已被使用");
        }


        /// <summary>
        /// 登入
        /// POST: /api/Auth/Login
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                //確認帳號、密碼正確後
                if (checkPasswordResult)
                {
                    //取得使用者的角色權限
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        //建立Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }
                }
            }

            return BadRequest("帳號或密碼錯誤");
        }


    }
}
