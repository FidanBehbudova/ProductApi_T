using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Entities.Auth;
using ProductApi.Entities.Dtos.Auth;


namespace ProductApi.Controllers.Auth
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;


        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = _mapper.Map<AppUser>(registerDto);
            var addeduser = await _userManager.CreateAsync(user, registerDto.Password);

            if (!addeduser.Succeeded)
            {
                return BadRequest(new
                {
                    Error = addeduser.Errors,
                    Code = 400
                });
            }


            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            await _userManager.AddToRoleAsync(user, "User");
            return Ok("user yaradildi");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                return BadRequest(new
                {
                    Error = "user tapilmadi",
                    Code = 400

                });
            }

            var password = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!password)
            {
                return BadRequest(new
                {
                    Error = "pass sehvdi",
                    Code = 400

                });
            }
            return Ok("login oldu");
        }

    }
}
