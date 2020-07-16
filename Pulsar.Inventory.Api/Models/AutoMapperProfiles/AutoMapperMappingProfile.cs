using AutoMapper;

namespace Pulsar.Inventory.Api.Models.AutoMapperProfiles
{
    /// <summary>
    ///     Add Automapper Mappings that will auto load during startup
    /// </summary>
    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
        {
            //CreateMap<string, string>().ReverseMap();

        }
    }
}