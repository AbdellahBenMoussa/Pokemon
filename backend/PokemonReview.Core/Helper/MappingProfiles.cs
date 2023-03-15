using AutoMapper;
using PokemonReview.Core.Models.Dtos;
using PokemonReview.Core.Models.Entities;

namespace PokemonReview.Core.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryUpdateDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<Country, CountryUpdateDto>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<Owner, OwnerGetDto>();
            CreateMap<Owner, OwnerUpdateDto>();
            //CreateMap<Pokemon, PokemonDto>();
            //CreateMap<Review, ReviewDto>();
            //CreateMap<Reviewer, ReviewerDto>();
            //CreateMap<Login, LoginDto>();

            CreateMap<CategoryDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<CountryDto, Country>();
            CreateMap<CountryUpdateDto, Country>();
            CreateMap<OwnerDto, Owner>();
            CreateMap<OwnerGetDto, Owner>();
            CreateMap<OwnerUpdateDto, Owner>();
            //CreateMap<PokemonDto, Pokemon>();
            //CreateMap<ReviewDto, Review>();
            //CreateMap<ReviewerDto, Reviewer>();
            //CreateMap<LoginDto, Login>();


        }
    }
}
