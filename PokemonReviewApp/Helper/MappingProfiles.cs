using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        // Either Rever Map like Catergory & Country by writing directly
        // OR We can use ReverseMap function.
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>().ReverseMap();
            CreateMap<Pokemon, PokemonListResponseBo>()
                .IncludeBase<Pokemon, PokemonDto>()
                .ForMember(dest => dest.OwnerFullName, opt => opt.MapFrom(src => src.PokemonOwners
                    .Select(po => po.Owner.FirstName + " " + po.Owner.LastName)
                    .FirstOrDefault()
                ))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.PokemonCategories
                    .Select(c => c.Category.Name)
                    .FirstOrDefault()
                ));

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();

            CreateMap<Owner, OwnerDto>().ReverseMap();

            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
        }
    }
}
