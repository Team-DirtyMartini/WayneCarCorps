using System.Collections.Generic;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;

namespace WayneCarCorps.Models.Fluent
{
    public class WayneCarCorpsFluentMetadata : FluentMetadataSource
    {
        private const string CarsTableName = "Cars";
        private const string AddressedTableName = "Addresses";
        private const string CarTypesTableName = "CarTypes";
        private const string ColorsTableName = "Colors";
        private const string ModelsTableName = "Models";
        private const string ManufacturersTableName = "Manufacturers";
        private const string CountriesTableName = "Countries";
        private const string DealersTableName = "Dealers";
        private const string SalesTableName = "Sales";

        private const string CardIdKey = "CarId";
        private const string DealerIdKey = "DealerId";
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            IList<MappingConfiguration> configurations = new List<MappingConfiguration>();

            var carMapping = new MappingConfiguration<Car>();
            carMapping.MapType(car => new
            {
                Id = car.Id,
                ModelId = car.ModelId,
                Year = car.Year,
                Price = car.Price,
                Power = car.Power,
                ColorId = car.ColorId,
                DealerId = car.DealerId
            }).ToTable(CarsTableName);

            carMapping.HasProperty(c => c.Id).IsIdentity(KeyGenerator.Autoinc);
            carMapping.HasAssociation(c => c.Model).WithOpposite(m => m.Cars)
                .HasConstraint((c, m) => c.ModelId == m.Id);
            carMapping.HasAssociation(c => c.Color).WithOpposite(color => color.Cars)
                .HasConstraint((c, color) => c.ColorId == color.Id);
            carMapping.HasAssociation(c => c.Dealer).WithOpposite(d => d.Cars)
                .HasConstraint((c, d) => c.DealerId == d.Id);
            
            var addressMapping = new MappingConfiguration<Address>();
            addressMapping.MapType(address => new
            {
                Id = address.Id,
                AdressLine = address.AddressLine,
                CountryId = address.CountryId
            }).ToTable(AddressedTableName);

            addressMapping.HasProperty(a => a.Id).IsIdentity(KeyGenerator.Autoinc);
            addressMapping.HasAssociation(a => a.Country).WithOpposite(c => c.Addresses)
                .HasConstraint((a, c) => a.CountryId == c.Id);
            
            var carTypeMapping = new MappingConfiguration<CarType>();
            carTypeMapping.MapType(carType => new
            {
                Id = carType.Id,
                Name = carType.Name
            }).ToTable(CarTypesTableName);

            carTypeMapping.HasProperty(c => c.Id).IsIdentity(KeyGenerator.Autoinc);
            
            var colorMapping = new MappingConfiguration<Color>();
            colorMapping.MapType(color => new
            {
                Id = color.Id,
                Name = color.Name
            }).ToTable(ColorsTableName);

            colorMapping.HasProperty(c => c.Id).IsIdentity(KeyGenerator.Autoinc);
            
            var modelMapping = new MappingConfiguration<Model>();
            modelMapping.MapType(model => new
            {
                Id = model.Id,
                Name = model.Name,
                ManufacturerId = model.ManufacturerId,
                CarTypeId = model.CarTypeId,
                NumberOfSeats = model.NumberOfSeats
            }).ToTable(ModelsTableName);

            modelMapping.HasProperty(m => m.Id).IsIdentity(KeyGenerator.Autoinc);
            modelMapping.HasAssociation(model => model.Manufacturer).WithOpposite(manufacturer => manufacturer.Models)
                .HasConstraint((model, manufacturer) => model.ManufacturerId == model.Id);
            modelMapping.HasAssociation(m => m.CarType).WithOpposite(ct => ct.Models)
                .HasConstraint((m, ct) => m.CarTypeId == ct.Id);
            
            var manufacturerMapping = new MappingConfiguration<Manufacturer>();
            manufacturerMapping.MapType(manufacturer => new
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
                AddressId = manufacturer.AddressId
            }).ToTable(ManufacturersTableName);

            manufacturerMapping.HasProperty(m => m.Id).IsIdentity(KeyGenerator.Autoinc);
            manufacturerMapping.HasAssociation(m => m.Address).WithOpposite(a => a.Manufacturers)
                .HasConstraint((m, a) => m.AddressId == a.Id);
            
            var countryMapping = new MappingConfiguration<Country>();
            countryMapping.MapType(country => new
            {
                Id = country.Id,
                Name = country.Name,
            }).ToTable(CountriesTableName);
            countryMapping.HasProperty(c => c.Id).IsIdentity(KeyGenerator.Autoinc);
            
            var dealerMapping = new MappingConfiguration<Dealer>();
            dealerMapping.MapType(dealer => new
            {
                Id = dealer.Id,
                Name = dealer.Name,
                AddressId = dealer.AddressId,
                Incomes = dealer.Incomes,
                Expenses = dealer.Expenses
            }).ToTable(DealersTableName);

            dealerMapping.HasProperty(d => d.Id).IsIdentity(KeyGenerator.Autoinc);
            dealerMapping.HasAssociation(d => d.Address).WithOpposite(a => a.Dealers)
                .HasConstraint((d, a) => d.AddressId == a.Id);
            
            var saleMapping = new MappingConfiguration<Sale>();
            saleMapping.MapType(sale => new
            {
                Id = sale.Id,
                CarId = sale.CarId,
                SoldCars = sale.SoldCars,
                PricePerCar = sale.PricePerCar,
                IncomeFromCar = sale.IncomeFromCar,
                DealerId = sale.DealerId,
                Date = sale.Date
            }).ToTable(SalesTableName);

            saleMapping.HasProperty(s => s.Id).IsIdentity(KeyGenerator.Autoinc);
            
            saleMapping.HasAssociation(s => s.Car).ToColumn(CardIdKey)
                .HasConstraint((s, c) => s.CarId == c.Id);
            saleMapping.HasAssociation(s => s.Dealer).ToColumn(DealerIdKey)
                .HasConstraint((s, d) => s.DealerId == d.Id);

            configurations.Add(carMapping);
            configurations.Add(addressMapping);
            configurations.Add(carTypeMapping);
            configurations.Add(colorMapping);
            configurations.Add(modelMapping);
            configurations.Add(manufacturerMapping);
            configurations.Add(countryMapping);
            configurations.Add(dealerMapping);
            configurations.Add(saleMapping);

            return configurations;
        }

        protected override MetadataContainer CreateModel()
        {
            MetadataContainer container =  base.CreateModel();
            MetaNameGenerator metanamegenerator = container.NameGenerator;
            container.DefaultMapping.NullForeignKey = true;
            metanamegenerator.RemoveCamelCase = false;
            metanamegenerator.ResolveReservedWords = false;
            
            return container;
        }
    }
}
