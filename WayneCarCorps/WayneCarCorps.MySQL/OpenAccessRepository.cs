using System.Collections.Generic;
using Telerik.OpenAccess;
using WayneCarCorps.Data.Common;
using WayneCarCorps.MySQL.FluentModels;

namespace WayneCarCorps.MySQL
{
    public class OpenAccessRepository<T> : IRepository<T> where T: class
    {
        public OpenAccessRepository(FluentModelContext context)
        {
            this.Context = context;

        }

        public FluentModelContext Context { get; set; }

        public IEnumerable<T> All()
        {
            return this.Context.GetAll<T>();
        }

        public T GetById(object id)
        {
            ObjectKey objectKey = new ObjectKey(typeof(T).Name, id);
            T entity = this.Context.GetObjectByKey(objectKey) as T;

            return entity;
        }

        public void Add(T entity)
        {
           this.Context.Add(entity);
        }

        public void Delete(T entity)
        {
           this.Context.Delete(entity);
        }

        public void Update(T entity)
        {
            // the update method is missing because the updates are automatically tracked by the
            // context and you just need to call the SaveChanges method to commit the changes in the database.
        }
    }
}
