using System.Security.Claims;
using ARPCE.Administration.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ARPCE.Administration.WebUI.Controllers;
public record UserRoleAsign
{
    public string username { get; set; }
    public string name { get; set; }
}
public record AuthorizeDto
{
    public string userId { get; set; }
    public string policyName { get; set; }
}
[Route("api/[controller]")]
[ApiController]

public class RoleController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ARPCEUser> _userManager;
    
    public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ARPCEUser> userManager) { 
        _roleManager = roleManager;
        _userManager = userManager;
    }

    // GET: RoleController/GetAll
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok( await _roleManager.Roles.ToListAsync());
    }
    [HttpPost]
    public async Task<ActionResult> Create(string name)
    {
        bool IsRoleExist = await _roleManager.RoleExistsAsync(name);
        if (IsRoleExist) {
            return BadRequest("Le role exist deja");
        }else
        {
            IdentityRole role = new IdentityRole { Name = name };
            IdentityResult result = await _roleManager.CreateAsync(role);
            if (result.Succeeded) {
                return Ok(result);
            } 
           
            return BadRequest(result);
        }
    }
    [HttpPost]
    [Route("AddUserToRole")]
    public async Task<IActionResult> AddUserToRole([FromBody] UserRoleAsign userRoleAsign)
    {
        var user = await _userManager.FindByNameAsync(userRoleAsign.username);
        if(user !=null)
        {
            var role = await _roleManager.FindByNameAsync(userRoleAsign.name);
            if (role != null)
            {
                await _userManager.AddToRoleAsync(user, role.Name);
                return Ok();
            }else
            {
                return BadRequest("Le Role n'exist pas");

            }
        }else
        {
            return BadRequest("L'utilisateur avec cette nom n'exist pas");
        }
        
      //  _userManager.AddToRoleAsync();
    }
    [HttpPost]
    [Route("Authorize")]
    public async Task<IActionResult> Authorize([FromBody] AuthorizeDto dto)
    {
        Claim claim = new Claim("Role", dto.policyName);
        var user = await _userManager.FindByNameAsync(dto.userId);
        var result = _userManager.AddClaimAsync(user,claim);
        
        return Ok(result);
    }
}