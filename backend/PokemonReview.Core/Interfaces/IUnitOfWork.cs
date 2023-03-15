using PokemonReview.Core.Models.Entities;

namespace PokemonReview.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository categories { get; }
        IGenericRepository<Country> countries { get; }
        IGenericRepository<Owner> owners { get; }
        int Complete();

    }
}
