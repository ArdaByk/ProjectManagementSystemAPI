using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Security.JWT;

public class AccessToken
{
    public AccessToken(string token, DateTime expiration)
    {
        Token = token;
        Expiration = expiration;
    }
    public AccessToken()
    {
        Token = string.Empty;
    }

    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}
