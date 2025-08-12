using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Enums;

public static class OperationClaims
{
    public const string Admin = "Admin";
    public const string ProjectOwner = "ProjectOwner";
    public const string ProjectMember = "ProjectMember";
    public const string User = "User";
}
