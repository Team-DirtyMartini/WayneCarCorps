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


namespace WayneCarCorps.Importer
{
    class Startup
    {
        static void Main()
        {
            /*var extractor = new Extractor<Car>();
            var cars  = extractor.GetEntitiesCollection("Cars"); */
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WayneCarCorpsContext, Configuration>());
            //var extractor = new Extractor<WayneCarCorps.MongoDBModels.Colour>();
            //var colors = extractor.GetEntitiesCollection("Colours");

            //using (var db = new WayneCarCorpsContext())
            //{

            //    var colorImporter = new ColorsImporter(colors,db);
            //    colorImporter.Import();
            //    Console.WriteLine(db.Colors.Count());
            //}

            XmlImporter.Create().Import();
        }


    }
}
