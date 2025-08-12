using PMS.Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Security.JWT;

public interface ITokenHelper
{
    public AccessToken CreateToken(User user, IList<string> operationClaims);

}
