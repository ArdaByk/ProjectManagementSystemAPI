using AutoMapper;
using PMS.Application.Features.Users.Commands.Create;
using PMS.Application.Features.Users.Commands.Delete;
using PMS.Application.Features.Users.Commands.Update;
using PMS.Application.Features.Users.Queries.GetById;
using PMS.Application.Features.Users.Queries.GetList;
using PMS.Core.Application.Responses;
using PMS.Core.Persistence.Paging;
using PMS.Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateUserCommand, User>().ReverseMap();
        CreateMap<CreateUserResponse, User>().ReverseMap();
        CreateMap<DeleteUserCommand, User>().ReverseMap();
        CreateMap<DeleteUserResponse, User>().ReverseMap();
        CreateMap<UpdateUserCommand, User>().ReverseMap();
        CreateMap<UpdateUserCommandResponse, User>().ReverseMap();
        CreateMap<GetByIdUserResponse,  User>().ReverseMap();
        CreateMap<GetListResponse<GetListUserQueryItemDto>, Paginate<User>>().ReverseMap();
        CreateMap<GetListUserQueryItemDto, User>().ReverseMap();
    }
}
