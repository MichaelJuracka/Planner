using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using Planner.Data.Interfaces;

namespace Planner.Data.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext dbContext;
        protected DbSet<TEntity> dbSet;
        public BaseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }
        public void Delete(int id)
        {
            TEntity entity = FindById(id);
            try
            {
                dbSet.Remove(entity);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                dbContext.Entry(entity).State = EntityState.Unchanged;
                throw;
            }
        }
        public TEntity FindById(int id)
        {
            return dbSet.Find(id);
        }
        public ObservableCollection<TEntity> GetAll()
        {
            var collection = new ObservableCollection<TEntity>(dbSet.ToList());
            return collection;
        }
        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            dbContext.SaveChanges();
        }
        public void Update(TEntity entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
