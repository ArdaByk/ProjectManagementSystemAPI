using PMS.Application.Features.ProjectUsers.Constants;
using PMS.Core.Application.Rules;
using PMS.Core.CrossCuttingConcerns.Exceptions.Types;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectUsers.Rules;

public class ProjectUserBusinessRules : BaseBusinessRule
{
    public async Task ProjectUserShouldExist(ProjectUser? projectUser)
    {
        if (projectUser == null)
            throw new BusinessException(ProjectUserMessages.ProjectUserDoesntExists);
    }
}
