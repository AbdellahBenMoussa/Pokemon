namespace PokemonReview.Core.Models.Entities
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<PokemonCategory> PokemonCategories { get; set; }
        public ICollection<PokemonOwner> PokemonOwners { get; set; }
        public Pokemon()
        {
            Reviews = new List<Review>();
            PokemonCategories = new List<PokemonCategory>();
            PokemonOwners = new List<PokemonOwner>();

        }
    }
}
