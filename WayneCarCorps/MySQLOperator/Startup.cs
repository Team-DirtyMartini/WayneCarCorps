using System.Collections.Generic;
using Telerik.OpenAccess;
using WayneCarCorps.Data;
using WayneCarCorps.Models;
using WayneCarCorps.Models.Fluent;
using System.Linq;
using Omu.ValueInjecter;
using Telerik.OpenAccess.Metadata;
using System;
using System.Reflection;
using Omu.ValueInjecter.Injections;
using Telerik.OpenAccess.Metadata.Relational;
using System.Data;

namespace MySQLOperator
{
    class Startup
    {
        static void Main(string[] args)
        {
            UpdateDatabase();
        }

        private static void UpdateDatabase()
        {
            IList<Address> addrs = new List<Address>();
            IList<Country> countries = new List<Country>();
            IList<Dealer> dealers = new List<Dealer>();
            IList<CarType> carTypes = new List<CarType>();
            IList<Color> colors = new List<Color>();
            IList<Model> models = new List<Model>();
            IList<Car> cars = new List<Car>();
            IList<Manufacturer> manufacturers = new List<Manufacturer>();
            IList<Sale> sales = new List<Sale>();

            using (var ctx = new WayneCarCorpsContext())
            {
                addrs = SerializeCollection(ctx.Addresses.ToList());
                dealers = SerializeCollection(ctx.Dealers.ToList());
                countries = SerializeCollection(ctx.Countries.ToList());
                cars = SerializeCollection(ctx.Cars.ToList());
                carTypes = SerializeCollection(ctx.CarTypes.ToList());
                colors = SerializeCollection(ctx.Colors.ToList());
                models = SerializeCollection(ctx.Models.ToList());
                manufacturers = SerializeCollection(ctx.Manufacturers.ToList());
                sales = SerializeCollection(ctx.Sales.ToList());
            }
            
            using (var context = new WayneCarCorpsFluentModel())
            {
                ResetDB(context);

                InsertCollectionIntoDatabase(sales, context);
                InsertCollectionIntoDatabase(carTypes, context);
                InsertCollectionIntoDatabase(countries, context);
                InsertCollectionIntoDatabase(colors, context);
                InsertCollectionIntoDatabase(addrs, context);
                InsertCollectionIntoDatabase(manufacturers, context);
                InsertCollectionIntoDatabase(models, context);
                InsertCollectionIntoDatabase(dealers, context);
                InsertCollectionIntoDatabase(cars, context);


                context.SaveChanges();
            }

        }

        private static void ResetDB(OpenAccessContext context)
        {
            var schemaHandler = context.GetSchemaHandler();
            string script = null;
            if (schemaHandler.DatabaseExists())
            {
                DropAllTables(context);
            }
            else
            {
                schemaHandler.CreateDatabase();
            }

            script = schemaHandler.CreateDDLScript();

            if (!string.IsNullOrEmpty(script))
            {
                schemaHandler.ForceExecuteDDLScript(script);
            }
        }

        private static IList<T> SerializeCollection<T>(IList<T> collection)
            where T : class, new()
        {
            IList<T> serializedCollection = new List<T>();

            foreach (var obj in collection)
            {
                T serializedObj = SerializeObject(obj);
                serializedCollection.Add(serializedObj);
            }

            return serializedCollection;
        }

        private static T SerializeObject<T>(T objectToSerialize)
            where T : class, new() 
        {
            var properties = objectToSerialize.GetType().GetProperties();
            HashSet<string> propertiesToIgnore = new HashSet<string>();
             
            foreach(var p in properties)
            {
                if (p.PropertyType.IsValueType ||
                    p.PropertyType == typeof(string))
                {
                    continue;
                }
                else if(p.PropertyType.IsClass || 
                    p.PropertyType.GetInterface(typeof(IEnumerable<>).FullName) != null)
                {
                    propertiesToIgnore.Add(p.Name);
                }
            }

            T serializedObj = new T().InjectFrom(
                new LoopInjection(propertiesToIgnore.ToArray()), 
                objectToSerialize) 
                as T;

            return serializedObj;
        }

        private static void DropAllTables(OpenAccessContext context)
        {
            var tableNames = GetTables(context.Metadata);

            using (IDbConnection connection = context.Connection)
            {
                using(IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        string.Format(
                        "SET foreign_key_checks = 0;" +
                        "DROP TABLE IF EXISTS {0};" +
                        "SET foreign_key_checks = 1;"
                        , string.Join(",", tableNames));


                    var aff = command.ExecuteNonQuery();

                    context.SaveChanges();
                    Console.WriteLine(aff);
                }
            }
        }

        private static void InsertCollectionIntoDatabase<T>(IList<T> collection, OpenAccessContext context)
        {
            foreach(var obj in collection)
            {
                context.Add(obj);
            }
        }

        private static string[] GetTables(MetadataContainer container)
        {
            HashSet<string> tableNames = new HashSet<string>();
            foreach(MetaTable table in container.Tables)
            {
                tableNames.Add(table.Name);
            }
            return tableNames.ToArray();
        }

    }
}
