using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductApi.Entities.Auth;
using ProductApi.Entities.Dtos.Auth;
using ProductApi.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ProductApi.Controllers.Auth
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly TokenOption _tokenOptions;


        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _config = config;
            _tokenOptions = _config.GetSection("TokenOptions").Get<TokenOption>();
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
                return NotFound();
            }

            var password = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!password)
            {
                return Unauthorized();
            }

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            JwtHeader header = new JwtHeader(signingCredentials);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(CustomClaimTypes.FullName,user.Email),
            };

            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }
            JwtPayload payload = new JwtPayload(issuer:_tokenOptions.Issuer,audience:_tokenOptions.Audience,claims:claims,notBefore:DateTime.Now,expires:DateTime.Now.AddHours(_tokenOptions.AccesTokenExpiration));
            JwtSecurityToken securityToken = new JwtSecurityToken(header,payload);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string jwt=handler.WriteToken(securityToken);
            return Ok(new
            {
                jwt = jwt,
                Expires= DateTime.Now.AddHours(_tokenOptions.AccesTokenExpiration)
            });
        }

    }
}
