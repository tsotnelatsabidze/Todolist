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
            return this.dbContext.Set<TEntity>().Find(id);
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
            TEntity entityToDelete = this.dbContext.Set<TEntity>().Find(id);
            this.Delete(entityToDelete);
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
