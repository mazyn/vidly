using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Adapters
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Customer model mappings
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id,
                    x => x.Ignore());

            // Movie model mappings
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>()
                .ForMember(m => m.Id,
                    x => x.Ignore());
        }
    }
}