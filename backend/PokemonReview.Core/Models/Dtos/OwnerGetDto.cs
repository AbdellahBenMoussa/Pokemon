using PokemonReview.Core.Models.Entities;

namespace PokemonReview.Core.Models.Dtos
{
    public class OwnerGetDto : OwnerDto
    {
        public Country Country { get; set; }

    }
}
