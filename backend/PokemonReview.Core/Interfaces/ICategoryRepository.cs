using PokemonReview.Core.Models.Entities;

namespace PokemonReview.Core.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Category unsharedcomportement(int id);
    }
}
