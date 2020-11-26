using Microsoft.EntityFrameworkCore;
using MSD.Product.Domain.Dtos.Common;
using MSD.Product.Domain.Infra;
using MSD.Product.Domain.Interfaces.Repositories.Common;
using MSD.Product.Domain.Models.Common;
using MSD.Product.Repository.Db.Context;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MSD.Product.Repository.Db.Common
{
    public abstract class CrudRepositoryBase<TEntity> : RepositoryDbBase, ICrudRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected EntitiesContext context;
        protected DbSet<TEntity> set;

        public CrudRepositoryBase(EntitiesContext context)
        {
            this.context = context;
            this.set = this.context.Set<TEntity>();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await this.GetAsync(id);

            if (entity == null)
                return;

            set.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await this.Query().FirstOrDefaultAsync(predicate);

        public virtual async Task<TEntity> GetAsync(Guid id) => await FirstOrDefaultAsync(e => e.Id == id);

        public virtual IQueryable<TEntity> Query() => set.AsQueryable();

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate) => set.Where(predicate);

        public virtual PagedResult<TEntity> Page(Expression<Func<TEntity, bool>> predicate, int page = 1, int pageSize = Constants.DefaultPageSize) => this.Page(this.Query(predicate), page, pageSize);

        public virtual PagedResult<T> Page<T>(IQueryable<T> query, int page = 1, int pageSize = Constants.DefaultPageSize)
        {
            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var startIndex = ((page - 1) * pageSize);
            var items = query.Skip(startIndex).Take(pageSize).ToList();
            var result = new PagedResult<T>(page, totalPages, items);

            return result;
        }

        public virtual async Task SaveAsync(TEntity entity)
        {
            if (entity.Id == null || entity.Id == Guid.Empty)
                await set.AddAsync(entity);
            else
                context.Entry(entity).State = EntityState.Modified;

            await this.context.SaveChangesAsync();
        }
    }
}
