using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayneCarCorps.Data.Common
{
    public class EfRepository<T> : IRepository<T> where T: class
    {
        public EfRepository(DbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

     

        protected DbContext Context { get; set; }

        protected DbSet<T> DbSet { get; set; }

        public IEnumerable<T> All()
        {
            return this.DbSet.ToList();
        }

        public void Add(T entity)
        {                     
            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Added;
        }

        public void Delete(T entity)
        {
            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public void Update(T entity)
        {
            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;
        }
    }
}
