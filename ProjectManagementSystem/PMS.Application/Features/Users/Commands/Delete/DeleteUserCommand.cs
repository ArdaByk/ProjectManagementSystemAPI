using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.Users.Rules;
using PMS.Core.Security.Entities;
using PMS.Core.Security.Extensions;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Users.Commands.Delete;

public class DeleteUserCommand : IRequest<DeleteUserResponse>
{

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IProjectUserService _projectUserService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserBusinessRules _userBusinessRules;

        public DeleteUserCommandHandler(IUserService userService, IMapper mapper, UserBusinessRules userBusinessRules, IProjectUserService projectUserService ,IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
            _projectUserService = projectUserService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            Guid userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.GetIdClaim());

            await _userBusinessRules.UserShouldExistsWhenSelected(userId);

            User user = await _userService.GetAsync(x => x.Id == userId, enableTracking: false, cancellationToken: cancellationToken);

            ICollection<ProjectUser> projectUsers = await _projectUserService.GetAllAsync(predicate: x => x.UserId == userId, enableTraking: false, cancellationToken: cancellationToken);

            await _projectUserService.DeleteRangeAsync(projectUsers, permanent: true);

            user = await _userService.DeleteAsync(user, permanent: true);

            return _mapper.Map<DeleteUserResponse>(user);
        }
    }
}
