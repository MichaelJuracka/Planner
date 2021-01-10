using System.Collections.ObjectModel;

namespace Planner.Data.Interfaces
{
    public interface IRepository<TEntity>
    {
        ObservableCollection<TEntity> GetAll();
        TEntity FindById(int id);
        void Insert(TEntity entity);
        void Delete(int id);
        void Update(TEntity entity);
    }
}
