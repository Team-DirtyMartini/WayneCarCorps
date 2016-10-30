using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayneCarCorps.Data;
using WayneCarCorps.Data.Migrations;
using WayneCarCorps.Models;

namespace WayneCarCorps.ConsoleClient
{
    class Startup
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WayneCarCorpsContext, Configuration>());
            using (var ctx = new WayneCarCorpsContext())
            {
                Console.WriteLine(ctx.Cars.Count());
            }
        }
    }
}
