using PokemonReview.Core.Interfaces;
using PokemonReview.Core.Models.Entities;
using PokemonReview.EF.Data;

namespace PokemonReview.EF.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository categories { get; private set; }
        public IGenericRepository<Country> countries { get; private set; }
        public IGenericRepository<Owner> owners { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            categories = new CategoryRepository(_context);
            countries = new GenericRepository<Country>(_context);
            owners = new GenericRepository<Owner>(_context);
        }
        public int Complete() =>
            _context.SaveChanges();
        public void Dispose() =>
            _context?.Dispose();

    }
}
