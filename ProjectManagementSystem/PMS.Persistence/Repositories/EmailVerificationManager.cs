using PMS.Application.Abstractions.Services;
using PMS.Persistence.Contexts;
using PMS.Core.Persistence.Repositories;
using PMS.Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Persistence.Repositories;

public class EmailVerificationManager : EfRepositoryBase<EmailVerificationToken, Guid, BaseDbContext>, IEmailVerificationTokenService
{
    public EmailVerificationManager(BaseDbContext dbContext) : base(dbContext)
    {
    }
}
