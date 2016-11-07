using System.Collections.Generic;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;
using WayneCarCorps.MySQL.Models;

namespace WayneCarCorps.MySQL.FluentModels
{
    public partial class FluentModelMetadataSource : FluentMetadataSource
    {
        private const string ReportSalesTableName = "ReportSales";

        protected override IList<MappingConfiguration> PrepareMapping()
        {
            List<MappingConfiguration> configurations =
                new List<MappingConfiguration>();

            var reportMapping = new MappingConfiguration<SalesReport>();
            reportMapping.MapType(report => new
            {
                CarId = report.CarModelId,
                CarModel = report.CarModel,
                Manufacturer = report.Manufacturer,
                NumberOfSeats = report.Seats,
                Dealer = report.Dealer,
                TotalSum = report.TotalSum,
                SoldCars = report.SoldCars
            }).ToTable(ReportSalesTableName);

            reportMapping.HasProperty(s => s.CarModelId).IsIdentity(KeyGenerator.Autoinc);

            configurations.Add(reportMapping);

            return configurations;
        }
    }
}
