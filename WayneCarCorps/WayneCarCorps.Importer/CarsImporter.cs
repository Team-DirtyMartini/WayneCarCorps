using MongoDBOperator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using WayneCarCorps.Data;
using WayneCarCorps.Models;

namespace WayneCarCorps.Importer
{
   public class CarsImporter
    {
        private WayneCarCorpsContext db;
       // private Car car;
        public CarsImporter(ICollection<WayneCarCorps.MongoDBModels.Car> cars, WayneCarCorpsContext db, Car car)
        {
            this.db = db;
            
        }

        //public void Import()
        //{
        //    var cars = extractor.GetEntitiesCollection("Cars");
        //    foreach (var item in cars)
        //    {
        //        var carToBeAdded = DeepClone<Car>(this.car);
        //        carToBeAdded.ColorId = item.co
        //    }
        //}

       
        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
