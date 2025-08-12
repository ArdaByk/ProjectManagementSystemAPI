using AutoMapper;
using PMS.Application.Features.ProjectTasks.Commands.Create;
using PMS.Application.Features.ProjectTasks.Commands.Delete;
using PMS.Application.Features.ProjectTasks.Commands.Update;
using PMS.Application.Features.ProjectTasks.Queries.GetProjectTasksByUserId;
using PMS.Application.Features.ProjectTasks.Queries.GetAProjectsTasksByUserId;
using PMS.Application.Features.ProjectTasks.Queries.GetProjectTasksWithUsersByProjectId;
using PMS.Application.Features.ProjectTasks.Queries.GetProjectTaskById;
using PMS.Application.Features.ProjectTasks.Queries.GetProjectTasksByProjectId;
using PMS.Core.Application.Responses;
using PMS.Core.Persistence.Paging;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Core.Security.Entities;

namespace PMS.Application.Features.ProjectTasks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateProjectTaskCommand, ProjectTask>().ReverseMap();
        CreateMap<CreateProjectTaskResponse, ProjectTask>().ReverseMap();
        CreateMap<DeleteProjectTaskCommand, ProjectTask>().ReverseMap();
        CreateMap<DeleteProjectTaskResponse, ProjectTask>().ReverseMap();
        CreateMap<UpdateProjectTaskCommand, ProjectTask>().ReverseMap();
        CreateMap<UpdateProjectTaskResponse, ProjectTask>().ReverseMap();
        CreateMap<GetProjectTasksByProjectIdResponse, ProjectTask>().ReverseMap();
        CreateMap<GetProjectTaskByIdResponse, ProjectTask>().ReverseMap();
        CreateMap<GetListResponse<GetProjectTasksByProjectIdResponse>, Paginate<ProjectTask>>().ReverseMap();
        CreateMap<GetListResponse<GetProjectTasksByUserIdItemDto>, Paginate<ProjectTask>>().ReverseMap();
        CreateMap<GetAProjectsTasksByUserIdItemDto, ProjectTask>().ReverseMap();
        CreateMap<GetProjectTasksByUserIdItemDto, ProjectTask>().ReverseMap();
        CreateMap<GetListResponse<GetAProjectsTasksByUserIdItemDto>, Paginate<ProjectTask>>().ReverseMap();
        CreateMap<User, UserItemDto>().ReverseMap();
        CreateMap<ProjectTaskUser, UserItemDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ReverseMap()
            .ForPath(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForPath(dest => dest.User.Username, opt => opt.MapFrom(src => src.Username))
            .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.Email))
            .ForPath(dest => dest.UserId, opt => opt.Ignore());
        CreateMap<ProjectTask, GetProjectTasksWithUsersByProjectIdItemDto>()
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
            .ReverseMap();
        CreateMap<Paginate<ProjectTask>, GetListResponse<GetProjectTasksWithUsersByProjectIdItemDto>>().ReverseMap();

        CreateMap<ProjectTask, GetProjectTasksByProjectIdResponse>()
         .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users));

        CreateMap<ProjectTaskUser, ProjectTaskUserDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));

    }
}
