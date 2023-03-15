using PokemonReview.Core.Consts;
using System.Linq.Expressions;

namespace PokemonReview.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetTByIdAsync(int id);
        Task<IEnumerable<T>> GetAllTiesAsync();
        Task<T> FindTByFilterAsync(Expression<Func<T, bool>> predicate, string[] includes = null);
        Task<IEnumerable<T>> FindAllTiesByFilterAsync(Expression<Func<T, bool>> predicate, string[] includes = null);
        Task<IEnumerable<T>> FindAllTiesByFilterAsync(Expression<Func<T, bool>> predicate, int skip, int take);
        Task<IEnumerable<T>> FindAllTiesByFilterAsync(Expression<Func<T, bool>> predicate,
            int? skip,
            int? take,
            Expression<Func<T, object>> orderbyobject,
            string orderdirection = OrderDirection.Ascending);
        Task<bool> IsTExistAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddTAsync(T entity);
        Task<IEnumerable<T>> AddTiesRangeAsync(IEnumerable<T> entities);
        T UpdateT(T entity);
        void DeleteT(T entity);
        void DeleteTiesRange(IEnumerable<T> entities);
        void AttachT(T entity);
        void AttachTiesRange(IEnumerable<T> entities);
        Task<int> CountTiesAsync();
        Task<int> CountTiesAsync(Expression<Func<T, bool>> criteria);
    }
}
