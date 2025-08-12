using Application.Features.Auth.Constants;
using PMS.Application.Abstractions.Services;
using PMS.Core.Application.Rules;
using PMS.Core.CrossCuttingConcerns.Exceptions.Types;

namespace PMS.Application.Features.Users.Rules;

public class UserBusinessRules: BaseBusinessRule
{
    private readonly IUserService _userService;

    public UserBusinessRules(IUserService userService)
    {
        _userService = userService;
    }

    public async Task UserEmailShouldNotExistsWhenInsert(string email)
    {
        bool result = await _userService.AnyAsync(predicate: u => u.Email == email, enableTraking: false);
        if (result)
            throw new BusinessException(AuthMessages.UserMailAlreadyExists);
    }
    public async Task UserShouldExistsWhenSelected(Guid id)
    {
        bool result = await _userService.AnyAsync(predicate: u => u.Id == id, enableTraking: false);
        if (!result)
            throw new BusinessException(AuthMessages.UserDontExists);
    }
}