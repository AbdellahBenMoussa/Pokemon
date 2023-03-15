namespace PokemonReview.Core.Models.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gym { get; set; }

        public Country Country { get; set; }
        public int CountryId { get; set; }
        public ICollection<PokemonOwner> PokemonOwners { get; set; }
        public Owner()
        {
            PokemonOwners = new List<PokemonOwner>();
        }
    }
}
