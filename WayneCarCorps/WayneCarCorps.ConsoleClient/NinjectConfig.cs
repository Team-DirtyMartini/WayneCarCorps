using Ninject.Extensions.Factory;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayneCarCorps.Data;
using WayneCarCorps.Data.Common;
using WayneCarCorps.Models;

namespace WayneCarCorps.ConsoleClient
{
    public class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            this.Bind<DbContext>().To<WayneCarCorpsContext>().InSingletonScope();
            this.Bind(typeof(IRepository<>)).To(typeof(EfRepository<>));
            this.Bind<IModelsFactory>().ToFactory().InSingletonScope();
            this.Bind<IUnitOfWork>().To<EfUnitOfWork>();
        }
    }
}
