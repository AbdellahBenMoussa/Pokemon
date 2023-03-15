using Microsoft.EntityFrameworkCore;
using PokemonReview.Core.Consts;
using PokemonReview.Core.Interfaces;
using PokemonReview.EF.Data;
using System.Linq.Expressions;

namespace PokemonReview.EF.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddTAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddTiesRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public T UpdateT(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void DeleteT(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteTiesRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void AttachT(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public void AttachTiesRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AttachRange(entities);
        }

        public async Task<int> CountTiesAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountTiesAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().CountAsync(criteria);
        }

        public async Task<IEnumerable<T>> FindAllTiesByFilterAsync(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query.Include(item);
                }
            }
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllTiesByFilterAsync(Expression<Func<T, bool>> predicate, int skip, int take) =>
            await _context.Set<T>()
                            .Where(predicate)
                            .Skip(skip)
                            .Take(take)
                            .ToListAsync();

        public async Task<IEnumerable<T>> FindAllTiesByFilterAsync(Expression<Func<T, bool>> predicate, int? skip, int? take, Expression<Func<T, object>> orderbyobject, string orderdirection = "ASC")
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderbyobject != null)
            {
                if (orderdirection == OrderDirection.Ascending)
                    query = query.OrderBy(orderbyobject);
                else
                    query = query.OrderByDescending(orderbyobject);
            }

            return await query.ToListAsync();
        }
        public async Task<T> FindTByFilterAsync(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return query.SingleOrDefault(predicate);
        }

        public async Task<IEnumerable<T>> GetAllTiesAsync() =>
            await _context.Set<T>().ToListAsync();

        public async Task<T> GetTByIdAsync(int id) =>
            await _context.Set<T>().FindAsync(id);

        public async Task<bool> IsTExistAsync(Expression<Func<T, bool>> predicate) =>
            await _context.Set<T>().AnyAsync(predicate);
    }
}
