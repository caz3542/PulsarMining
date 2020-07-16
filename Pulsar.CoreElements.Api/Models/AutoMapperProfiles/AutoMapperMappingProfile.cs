using AutoMapper;
using Pulsar.CoreElements.Api.Models.CoreElements;

namespace Pulsar.CoreElements.Api.Models.AutoMapperProfiles
{
    /// <summary>
    ///     Add Automapper Mappings that will auto load during startup
    /// </summary>
    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
        {
            CreateMap<CoreElement, CoreElementViewModel>().ReverseMap();

        }
    }
}