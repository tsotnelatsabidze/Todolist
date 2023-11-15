using Microsoft.EntityFrameworkCore;
using TodoListApp.Services.Database.Interfaces;

namespace TodoListApp.Services.Database.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly TodoListDbContext dbContext;

        public GenericRepository(TodoListDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IQueryable<TEntity> GetAll()
        {
            return this.dbContext.Set<TEntity>();
        }

        public TEntity GetById(object id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return this.dbContext.Set<TEntity>().Find(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Insert(TEntity entity)
        {
            _ = this.dbContext.Set<TEntity>().Add(entity);
            _ = this.dbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Modified;
            _ = this.dbContext.SaveChanges();
        }

        public void Delete(object id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            TEntity entityToDelete = this.dbContext.Set<TEntity>().Find(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (entityToDelete != null)
            {
                this.Delete(entityToDelete);
            }
        }

        public void Delete(TEntity entity)
        {
            if (this.dbContext.Entry(entity).State == EntityState.Detached)
            {
                _ = this.dbContext.Set<TEntity>().Attach(entity);
            }

            _ = this.dbContext.Set<TEntity>().Remove(entity);
            _ = this.dbContext.SaveChanges();
        }
    }
}
