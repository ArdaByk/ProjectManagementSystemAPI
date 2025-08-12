using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Projects.Queries.GetProjectWithProjectTasksAndUsers;

public class ProjectUserListItemDto
{
    public string OperationClaim { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}
