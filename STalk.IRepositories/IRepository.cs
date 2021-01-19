using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace STalk.IRepositories
{
    public interface IRepository<Entity>
    {
        void Delete(Entity entity);
        void Add(Entity entity);
        void Update(Entity entity);
        Entity Get(Expression<Func<Entity, bool>> id);
        IEnumerable<Entity> GetAll();
        void SaveChanges();
    }
}
