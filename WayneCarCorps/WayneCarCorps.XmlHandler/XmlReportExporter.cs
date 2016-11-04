using System.Linq;
using System.Text;
using System.Xml;
using WayneCarCorps.Data;

namespace WayneCarCorps.XmlHandler
{
    public class XmlReportExporter
    {
        private const string FilePath = "../../reports.xml";
        
        public static void GetSalesForEachDealership()
        {
            var dbContext = new WayneCarCorpsContext();

            var dealerships = dbContext.Sales.GroupBy(x => new { x.Dealer.Name, x.Date }).ToList();
            int countOfDates = dealerships.Select(x => x.Key.Date).Distinct().Count();

            Encoding encoding = Encoding.GetEncoding("windows-1251");
            using (var writer = new XmlTextWriter(FilePath, encoding))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("sales");

                int count = 0;

                foreach (var dealership in dealerships)
                {
                    if (count % countOfDates == 0)
                    {
                        writer.WriteStartElement("sale");
                        writer.WriteAttributeString("dealership", dealership.Key.Name);
                    }

                    count++;

                    var currentDate =
                        $"{dealership.Key.Date.Day}-{dealership.Key.Date.Month}-{dealership.Key.Date.Year}";

                    var totalSum = dealership.Sum(x => x.IncomeFromCar);
                    var totalNumberOfCars = dealership.Sum(x => x.SoldCars);

                    WriteToXml(writer, currentDate, totalSum.ToString(), totalNumberOfCars.ToString());

                    if (count % countOfDates == 0)
                    {
                        writer.WriteEndElement();
                        count = 0;
                    }
                }

                writer.WriteEndDocument();
            }
        }

        private static void WriteToXml(XmlTextWriter writer, string date, string income, string soldCars)
        {
            writer.WriteStartElement("summaryOfSales");
            writer.WriteAttributeString("date", date);
            writer.WriteAttributeString("income", income);
            writer.WriteAttributeString("soldCars", soldCars);
            writer.WriteEndElement();
        }
    }
}
