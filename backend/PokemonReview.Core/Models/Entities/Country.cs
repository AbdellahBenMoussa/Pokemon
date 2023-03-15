using System.Text.Json.Serialization;

namespace PokemonReview.Core.Models.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Owner> Owners { get; set; }
        public Country(string name)
        {
            Owners = new List<Owner>();
            Name = name;
        }
    }
}
