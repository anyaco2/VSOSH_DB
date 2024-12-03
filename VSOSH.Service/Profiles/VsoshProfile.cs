using AutoMapper;
using VSOSH.Contracts;

namespace VSOSH.Service.Profiles;

public class VsoshProfile : Profile
{
    public VsoshProfile()
    {
        CreateMap<Subject, Domain.Subject>();
    }
}