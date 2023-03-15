namespace PokemonReview.Core.Models.Dtos
{
    public class OwnerUpdateDto : OwnerDto
    {
        public int Id { get; set; }
        public int countryId { get; set; }

    }
}
