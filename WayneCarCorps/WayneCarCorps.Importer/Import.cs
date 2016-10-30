using MongoDBOperator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayneCarCorps.Data;
using WayneCarCorps.MongoDBModels;


namespace WayneCarCorps.Importer
{
    class Import
    {
        static void Main()
        {
            /*var extractor = new Extractor<Car>();
            var cars  = extractor.GetEntitiesCollection("Cars"); */
        }

        private static void ImportCars(Extractor<Car> extractor, WayneCarCorpsContext db)
        {
            var cars = extractor.GetEntitiesCollection("Cars");
            foreach (var car in cars)
            {
                //var carToBeAdded = new WayneCarCorps.Models.Car
            }
        }
    }
}
