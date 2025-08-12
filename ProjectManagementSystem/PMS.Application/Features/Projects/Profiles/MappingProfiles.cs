using AutoMapper;
using PMS.Application.Features.Projects.Commands.Create;
using PMS.Application.Features.Projects.Commands.Delete;
using PMS.Application.Features.Projects.Commands.Update;
using PMS.Application.Features.Projects.Queries.GetOwnedProjectsByUserId;
using PMS.Application.Features.Projects.Queries.GetProjectById;
using PMS.Application.Features.Projects.Queries.GetProjectsByUserId;
using PMS.Application.Features.Projects.Queries.GetProjectWithProjectTasksAndUsers;
using PMS.Application.Features.Projects.Queries.GetProjectWithProjectTasksAndUsersQuery;
using PMS.Core.Application.Responses;
using PMS.Core.Persistence.Paging;
using PMS.Core.Security.Entities;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Projects.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
            CreateMap<CreateProjectCommand, Project>().ReverseMap();
            CreateMap<CreateProjectResponse, Project>().ReverseMap();
            CreateMap<DeleteProjectCommand, Project>().ReverseMap();
            CreateMap<DeleteProjectResponse, Project>().ReverseMap();
            CreateMap<UpdateProjectCommand, Project>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => src != null));
            CreateMap<UpdateProjectResponse, Project>().ReverseMap();
            CreateMap<GetProjectByIdResponse, Project>().ReverseMap();
            CreateMap<GetProjectsByUserIdItemDto, Project>().ReverseMap();
            CreateMap<GetOwnedProjectsByUserIdResponse, Project>().ReverseMap();
            CreateMap<GetListResponse<GetOwnedProjectsByUserIdResponse>, Paginate<Project>>().ReverseMap();
            CreateMap<GetListResponse<GetProjectsByUserIdItemDto>, Paginate<Project>>().ReverseMap();

            CreateMap<Project, GetProjectWithProjectTasksAndUsersResponse>()
                .ForMember(dest => dest.ProjectTasks, opt => opt.MapFrom(src => src.ProjectTasks.Select(pt => pt.ProjectTask)))
                .ForMember(dest => dest.ProjectUsers, opt => opt.MapFrom(src => src.ProjectUsers))
                .ReverseMap();

            CreateMap<ProjectTask, ProjectTaskItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users.Select(ptu => ptu.User)));

            CreateMap<User, ProjectTaskUserItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));

            CreateMap<ProjectUser, ProjectUserListItemDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
    }
}
