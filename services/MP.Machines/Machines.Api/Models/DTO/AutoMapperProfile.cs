using AutoMapper;
using MP.MachinesApi.Models;

namespace Machines.Api.Models.DTO
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Parameter, ParameterDTO>()
                .ForMember(dest => dest.Machines, act => {
                    act.PreCondition(src => src.Machines != null);
                    act.MapFrom(src => src.Machines);
                });
            CreateMap<Parameter, ParameterBaseDTO>()
                .ForSourceMember(src => src.Machines, opt => opt.DoNotValidate());
            
            CreateMap<Machine, MachineDTO>()
                .ForMember(dest => dest.Parameters, act => {
                    act.PreCondition(src => src.Parameters != null);
                    act.MapFrom(src => src.Parameters);
                });
            CreateMap<Machine, MachineBaseDTO>()
                .ForSourceMember(src => src.Parameters, opt => opt.DoNotValidate());
                
        }
    }
}
