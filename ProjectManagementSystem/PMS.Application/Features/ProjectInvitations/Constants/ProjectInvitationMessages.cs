using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectInvitations.Constants;

public static class ProjectInvitationMessages
{
    public static string InvitationDoesntExist = "Noc such invitation found.";
    public static string UserDoesntExist = "No such user found.";
    public static string ProjectDoesntExist = "No such project found.";
    public static string UserAlreadyExists = "This user is already a member of the project.";
    public static string InvitationExpired = "Invitation expired.";
}
