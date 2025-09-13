using System.Linq.Expressions;
using Ardalis.Specification;

namespace WeChooz.TechAssessment.Application.Interfaces.Repositories;

public interface IGenericRepositoryAsync<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id, bool trackChanges = false);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
    void DeleteRangeAsync(IEnumerable<TEntity> entities);
    void DetachEntity(TEntity entity);
    Task<List<TEntity>> GetBySpecification(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}