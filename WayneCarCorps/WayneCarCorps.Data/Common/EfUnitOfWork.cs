using System.Data.Entity;

namespace WayneCarCorps.Data.Common
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private DbContext dbContext;

        public EfUnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Commit()
        {
            this.dbContext.SaveChanges();
        }
    }
}