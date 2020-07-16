using AutoMapper;
using Pulsar.Customers.Api.Models.Customers;

namespace Pulsar.Customers.Api.Models.AutoMapperProfiles
{
    /// <summary>
    ///     Add Automapper Mappings that will auto load during startup
    /// </summary>
    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>().ReverseMap();

        }
    }
}