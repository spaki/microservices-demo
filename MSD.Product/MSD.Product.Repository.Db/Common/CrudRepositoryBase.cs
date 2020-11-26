using Microsoft.EntityFrameworkCore;
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
