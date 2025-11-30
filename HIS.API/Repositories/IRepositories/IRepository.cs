using System.Linq.Expressions;

namespace HIS.API.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        void Update(T entity);

        void Delete(T entity);


        Task<IEnumerable<T?>> GetAsync(
          Expression<Func<T, bool>>? expression = null,
          Expression<Func<T, Object>>[]? includes = null,
          bool tracked = true,
          CancellationToken cancellationToken = default);

        Task<T?> GetOneAsync(
           Expression<Func<T, bool>>? expression = null,
           Expression<Func<T, Object>>[]? includes = null,
           bool tracked = true,
           CancellationToken cancellationToken = default);

        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
