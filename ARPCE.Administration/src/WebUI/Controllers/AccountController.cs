using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Application.Common.Models;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ARPCE.Administration.WebUI.Controllers;
public record Auth
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<ARPCEUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AccountController> _logger;
    public AccountController(UserManager<ARPCEUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;   
    }
    [HttpPost]
    public async Task<IActionResult> Signin([FromBody] Auth auth)
    {
       var UserEntity = await  _userManager.FindByNameAsync(auth.UserName);
        if(UserEntity != null)
        {
            IList<Claim> userClaims = new List<Claim>();
            userClaims.Add(new Claim("UserName", UserEntity.UserName));
            userClaims.Add(new Claim("EmailConfirmed", UserEntity.EmailConfirmed.ToString()));
            var roles = await _userManager.GetRolesAsync(UserEntity);
            foreach(var role in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role,role));
            }
            userClaims.Add(new Claim(ClaimTypes.Name, UserEntity.UserName));
            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, UserEntity.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddHours(2)).ToUnixTimeSeconds().ToString()));

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ThisIsMyCrazyFNPOSLittleSecretKey")),SecurityAlgorithms.HmacSha256)),
                new JwtPayload(userClaims)
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new AuthorizationToken
            {
                Token = jwtToken,
                UserId = UserEntity.Id,
                Email = UserEntity.Email,
                Message = "Ok"
            });
        }else
        {
            return BadRequest("L'utlisateur n'exist pas");
        }
    }
}
