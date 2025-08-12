using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Security.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static ICollection<string>? GetClaims(this ClaimsPrincipal claimsPrincipal, string claimType)
        => claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToImmutableArray();
    
    public static ICollection<string>? GetRoleClaims(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal?.GetClaims(ClaimTypes.Role);
   
    public static string? GetIdClaim(this ClaimsPrincipal claimsPrincipal)
        =>  claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
}
