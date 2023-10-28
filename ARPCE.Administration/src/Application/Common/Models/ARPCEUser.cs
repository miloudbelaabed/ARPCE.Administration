using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPCE.Administration.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace ARPCE.Administration.Application.Common.Models;
public class ARPCEUser : IdentityUser
{
    public bool? IsActive { get; set; } = false;
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public DateTime Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}
