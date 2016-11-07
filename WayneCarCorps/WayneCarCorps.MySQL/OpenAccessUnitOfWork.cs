using WayneCarCorps.Data.Common;
using WayneCarCorps.MySQL.FluentModels;

namespace WayneCarCorps.MySQL
{
    public class OpenAccessUnitOfWork : IUnitOfWork
    {
        private FluentModelContext dbContext;
        public OpenAccessUnitOfWork(FluentModelContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Commit()
        {
            this.dbContext.SaveChanges();
        }
    }
}
