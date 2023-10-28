using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPCE.Administration.Application.Common.Models;
public class AuthorizationToken
{
    public string Token { get; set; }

    public string UserId { get; set; }
    public string Email { get; set; } 
    public string Message { get; set; } = string.Empty;
   
   
}
