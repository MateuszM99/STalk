using Microsoft.EntityFrameworkCore;
using STalk.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace STalk.Repositories
{
    public class Repository<Entity> : IRepository<Entity> where Entity : class
    {
        public DbContext Context;
        public DbSet<Entity> Q;
        public Repository()
        {
            Q = Context.Set<Entity>();
        }
        public Repository(DbContext context)
        {
            Q = context.Set<Entity>();
        }

        public void Add(Entity entity)
        {
            Q.Add(entity);
        }

        public void Delete(Entity entity)
        {
            Q.Remove(entity);
        }

        public Entity Get(Expression<Func<Entity, bool>> predicate)
        {
            return Q.FirstOrDefault(predicate);
        }

        public IEnumerable<Entity> GetAll()
        {
            return Q.AsEnumerable();
        }

        public void Update(Entity entity)
        {
            Q.Update(entity);
        }
        public void SaveChanges()
        {
            Context.SaveChanges();
        }

    }
}
