using MSD.Product.Domain.Dtos.Common;
using MSD.Product.Domain.Models.Common;
using MSD.Product.Infra;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MSD.Product.Domain.Interfaces.Repositories.Common
{
    public interface ICrudRepositoryBase<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task SaveAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        IQueryable<TEntity> Query();
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);
        PagedResult<TEntity> Page(Expression<Func<TEntity, bool>> predicate, int page = 1, int pageSize = Constants.DefaultPageSize);
        PagedResult<T> Page<T>(IQueryable<T> query, int page = 1, int pageSize = Constants.DefaultPageSize);
    }
}
