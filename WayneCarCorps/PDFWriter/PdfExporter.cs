using System.Data.Entity;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WayneCarCorps.Data;

namespace PDFWriter
{
    public class PdfExporter
    {
        public static void CreatePdfTable()
        {
            var dbContext = new WayneCarCorpsContext();

            var sales = dbContext.Sales.Include("Dealer").Select(x => new
            {
                DealerName = x.Dealer.Name,
                Car = x.Car.Model.Name,
                SoldCars = x.SoldCars,
                TotalIncomeForDay = x.IncomeFromCar,
                Date = x.Date
            });

            var dates = dbContext.Sales.GroupBy(p => p.Date).ToList();

            foreach (var date in dates)
            {
                var currentDate = $"{date.Key.Day}-{date.Key.Month}-{date.Key.Year}";
                string filePath = $"../../PdfReports/Sales-report-{currentDate}.pdf";
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

                using (writer)
                {
                    document.Open();

                    PdfPTable table = new PdfPTable(4);
                    PdfPCell dealerCell = new PdfPCell(new Phrase("Dealer"));
                    dealerCell.BackgroundColor = BaseColor.GRAY;

                    PdfPCell modelCell = new PdfPCell(new Phrase("Model"));
                    modelCell.BackgroundColor = BaseColor.GRAY;

                    PdfPCell soldCarsCell = new PdfPCell(new Phrase("Sold cars"));
                    soldCarsCell.BackgroundColor = BaseColor.GRAY;

                    PdfPCell incomeFromCarCell = new PdfPCell(new Phrase("Income from cars"));
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

                    Font font = FontFactory.GetFont("Segoe UI", 12.0f, Font.BOLD);
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
