using AutoMapper;
using PMS.Application.Features.ProjectInvitations.Commands.Send;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectInvitations.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<SendProjectInvitationCommand, ProjectInvitation>().ReverseMap();
        CreateMap<SendProjectInvitationResponse,  ProjectInvitation>().ReverseMap();
    }
}
