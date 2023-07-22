using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace ARPCE.Administration.WebUI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    public record UserAccount {
        public string username { get; set; }
        public string password { get; set; }
    }
    private readonly IIdentityService _identityService;
    public UserController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost]
    /*public async Task<(Result result, string userId)> Create([FromBody] UserAccount userAccount)
    {
        var result = await _identityService.CreateUserAsync(userAccount.username, userAccount.password);
        var res  = result.Result;
        return (res, "23232");
       }*/
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserAccount userAccount)
    {
        var result = await _identityService.CreateUserAsync(userAccount.username, userAccount.password);
        if(result.Result.Succeeded)
        {
            return Ok(result.UserId);
        }else
        {
          return  BadRequest(result.Result);
        }
        
        

    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _identityService.GetAll();
        return Ok(result);
        
    }
}
