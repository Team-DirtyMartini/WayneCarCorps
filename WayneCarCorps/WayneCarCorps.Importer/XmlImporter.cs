using MongoDBOperator;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using WayneCarCorps.Data.Common;
using WayneCarCorps.Importer.Models;
using WayneCarCorps.Models;
using WayneCarCorps.MongoDBModels;

namespace WayneCarCorps.Importer
{
    public class XmlImporter
    {
        private IModelsFactory modelsFactory;
        private IUnitOfWork unitOfWork;
        private IRepository<Country> countries;

        public XmlImporter(IModelsFactory modelsFactory,
            IUnitOfWork unitOfWork,
           IRepository<Country> countries)
        {
            this.modelsFactory = modelsFactory;
            this.unitOfWork = unitOfWork;
            this.countries = countries;
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

            foreach (var country in countries)
            {
                var newCountry = this.modelsFactory.CreateCountry();
                newCountry.Name = country.Name;                

                this.countries.Add(newCountry);

                //if (addedCountries % 100 == 0)
                //{
                //    db.SaveChanges();
                //    db = new WayneCarCorpsContext();
                //    db.Configuration.AutoDetectChangesEnabled = false;
                //    db.Configuration.ValidateOnSaveEnabled = false;
                //}
            }

            unitOfWork.Commit();
            //db.Configuration.AutoDetectChangesEnabled = true;
            //db.Configuration.ValidateOnSaveEnabled = true;
        }
    }
}
