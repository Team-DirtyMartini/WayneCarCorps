using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayneCarCorps.Data.Common
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private DbContext db;
        public EfUnitOfWork(DbContext db)
        {
            this.db = db;
        }
        public void Commit()
        {
            this.db.SaveChanges();
        }
    }
}
