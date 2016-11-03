using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser.clipper;
using WayneCarCorps.Data;

namespace PDFWriter
{
    class Program
    {
        static void Main()
        {
            CreatePdfTable("SalesReport.pdf");
        }

        public static void CreatePdfTable(string pdfFile)
        {
            var dbContext = new WayneCarCorpsContext();
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("../../" + pdfFile, FileMode.Create));
            
            using (writer)
            {
                document.Open();

                var sales = dbContext.Sales.Include("Dealer").Select(x => new
                {
                    DealerName = x.Dealer.Name,
                    Car = x.Car.Model.Name,
                    SoldCars = x.SoldCars,
                    TotalIncomeForDay = x.IncomeFromCar
                });

                var dates = dbContext.Sales.GroupBy(p => p.Date).ToList();

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
                table.AddCell(incomeFromCarCell);
                table.AddCell(incomeFromCarCell);

                foreach (var date in dates)
                {
                    Console.WriteLine(date.Key);
                    PdfPCell cell = new PdfPCell(new Phrase("Date: " + date.Key.ToString()));
                    cell.Colspan = 4;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);

                    foreach (var sale in sales)
                    {
                        Console.WriteLine(sale.DealerName);
                        table.AddCell(sale.DealerName);
                        table.AddCell(sale.Car.ToString());
                        table.AddCell(sale.SoldCars.ToString());
                        table.AddCell(sale.TotalIncomeForDay.ToString());
                    }

                    document.Add(table);
                }
                
                document.Close();
            }
        }
    }
}
