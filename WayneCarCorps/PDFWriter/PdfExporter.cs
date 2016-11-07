using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WayneCarCorps.Data.Common;
using WayneCarCorps.Models;

namespace PDFWriter
{
    public class PdfExporter
    {
        private const string FilePaths = "../../../PdfReports/Sales-report-{0}.pdf";
        private const string Dealer = "Dealer";
        private const string Model = "Model";
        private const string SoldCars = "Sold cars";
        private const string IncomeFromCars = "Income from cars";
        private const string PdfFont = "Segoe UI";

        public PdfExporter(IRepository<Sale> salesRepository)
        {
            this.SalesRepository = salesRepository;
        }

        public IRepository<Sale> SalesRepository { get; set; }

        public void CreatePdfTable()
        {
            var sales = this.SalesRepository.All().Select(x => new
            {
                DealerName = x.Dealer.Name,
                Car = x.Car.Model.Name,
                SoldCars = x.SoldCars,
                TotalIncomeForDay = x.IncomeFromCar,
                Date = x.Date
            });

            var dates = this.SalesRepository.All().GroupBy(sale => sale.Date);

            foreach (var date in dates)
            {
                var currentDate = $"{date.Key.Day}-{date.Key.Month}-{date.Key.Year}";
                string filePath = string.Format(FilePaths, currentDate);
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

                using (writer)
                {
                    document.Open();

                    PdfPTable table = new PdfPTable(4);
                    PdfPCell dealerCell = new PdfPCell(new Phrase(Dealer));
                    dealerCell.BackgroundColor = BaseColor.GRAY;

                    PdfPCell modelCell = new PdfPCell(new Phrase(Model));
                    modelCell.BackgroundColor = BaseColor.GRAY;

                    PdfPCell soldCarsCell = new PdfPCell(new Phrase(SoldCars));
                    soldCarsCell.BackgroundColor = BaseColor.GRAY;

                    PdfPCell incomeFromCarCell = new PdfPCell(new Phrase(IncomeFromCars));
                    incomeFromCarCell.BackgroundColor = BaseColor.GRAY;

                    table.AddCell(dealerCell);
                    table.AddCell(modelCell);
                    table.AddCell(soldCarsCell);
                    table.AddCell(incomeFromCarCell);

                    PdfPCell cell = new PdfPCell(new Phrase("Date: " + currentDate));
                    cell.Colspan = 4;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;

                    table.AddCell(cell);
                    decimal totalSumForTheDay = 0;

                    foreach (var sale in sales)
                    {
                        if (sale.Date == date.Key)
                        {
                            table.AddCell(sale.DealerName);
                            table.AddCell(sale.Car);
                            table.AddCell(sale.SoldCars.ToString());
                            table.AddCell(sale.TotalIncomeForDay.ToString());
                            totalSumForTheDay += sale.TotalIncomeForDay;
                        }
                    }

                    Font font = FontFactory.GetFont(PdfFont, 12.0f, Font.BOLD);
                    PdfPCell totalSumCell =
                        new PdfPCell(
                            new Phrase(
                                $"Total sum for {currentDate}: {totalSumForTheDay}",
                                font));
                    totalSumCell.Colspan = 4;
                    totalSumCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(totalSumCell);

                    document.Add(table);
                    document.Close();
                }
            }
        }
    }
}
