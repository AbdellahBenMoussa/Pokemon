using PokemonReview.Core.Interfaces;
using PokemonReview.Core.Models.Entities;
using PokemonReview.EF.Data;

namespace PokemonReview.EF.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context) { }
        public Category unsharedcomportement(int id)
        {
            throw new NotImplementedException();
        }
    }
}
