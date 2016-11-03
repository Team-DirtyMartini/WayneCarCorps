using MongoDBOperator;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayneCarCorps.Data;
using WayneCarCorps.Data.Migrations;
using WayneCarCorps.Models;
using WayneCarCorps.MongoDBModels;

namespace WayneCarCorps.Importer
{
    class Startup
    {
        static void Main()
        {
        
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WayneCarCorpsContext, Configuration>());
            var extractor = new MongoDBExtractor<MongoCar>();
            var cars = extractor.GetEntitiesCollection("Cars");
            using (var db = new WayneCarCorpsContext())
            {
                var carsImporter = new CarsImporter(db);
                carsImporter.import(cars);
            }


            XmlImporter.Create().Import();
        }


    }
}
