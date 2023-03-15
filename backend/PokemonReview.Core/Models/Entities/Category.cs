namespace PokemonReview.Core.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PokemonCategory> PokemonCategories { get; set; }
        public Category(string name)
        {
            PokemonCategories = new List<PokemonCategory>();
            Name = name;
        }
    }
}
