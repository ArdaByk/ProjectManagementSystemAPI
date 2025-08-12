using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectTasks.Queries.GetProjectTasksByProjectId;

public class ProjectTaskUserDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}