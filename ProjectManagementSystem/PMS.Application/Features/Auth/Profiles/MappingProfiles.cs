using AutoMapper;
using PMS.Application.Features.Auth.Queries.VerifyEmail;
using PMS.Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Auth.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<VerifyEmailQuery, User>().ReverseMap();
    }
}
