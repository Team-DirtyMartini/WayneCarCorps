using MongoDBOperator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WayneCarCorps.Data;
using WayneCarCorps.Importer.Models;
using WayneCarCorps.Models;
using WayneCarCorps.MongoDBModels;

namespace WayneCarCorps.Importer
{
    public class XmlImporter
    {       
        private XmlImporter()
        {
            
        }

        public static XmlImporter Create()
        {
            return new XmlImporter();
        }

        public void Import()
        {
            var countries = this.Deserialize<CountryXmlModel>("../../XmlFiles/CountriesXml.xml", "Countries");
            this.ProcessCountries(countries);

            var cars = this.Deserialize<MongoCar>("../../XmlFiles/MongoCars.xml", "Cars");
            this.ProcessCarsInMongo(cars);
        }

        private IEnumerable<TModel> Deserialize<TModel>(string fileName, string rootElement)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("File not found!", fileName);
            }

            var serializer = new XmlSerializer(typeof(List<TModel>), new XmlRootAttribute(rootElement));
            IEnumerable<TModel> result;
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                result = (IEnumerable<TModel>)serializer.Deserialize(fs);
            }

            return result;
        }

        private void ProcessCarsInMongo(IEnumerable<MongoCar> cars)
        {
            var mongoLoader = new MongoDBLoader<MongoCar>();
            mongoLoader.LoadEntities(cars, "Cars");
        }

        private void ProcessCountries(IEnumerable<CountryXmlModel> countries)
        {
            var addedCountries = 0;
            var db = new WayneCarCorpsContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.ValidateOnSaveEnabled = false;

            foreach (var country in countries)
            {
                var newCountry = new Country
                {
                    Name = country.Name
                };

                db.Countries.Add(newCountry);
                addedCountries++;

                if (addedCountries % 100 == 0)
                {
                    db.SaveChanges();
                    db = new WayneCarCorpsContext();
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ValidateOnSaveEnabled = false;
                }
            }

            db.SaveChanges();
            db.Configuration.AutoDetectChangesEnabled = true;
            db.Configuration.ValidateOnSaveEnabled = true;
        }
    }
}
