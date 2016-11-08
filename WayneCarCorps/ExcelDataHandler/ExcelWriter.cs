using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using WayneCarCorps.MySQL;
using WayneCarCorps.MySQL.ConsoleClient;
using WayneCarCorps.MySQL.Models;

namespace ExcelDataHandler
{
    public class ExcelWriter
    {
        private const string ExcelFilePath = @"../../../Excel-Report.xlsx";

        public static void WriteToExcel(OpenAccessRepository<SalesReport> repository)
        {
            FileInfo newFile = new FileInfo(ExcelFilePath);

            using (ExcelPackage xlPackage = new ExcelPackage(newFile))
            {
                var ws = xlPackage.Workbook.Worksheets.Add("Report");

                ws.Cells["A1"].Value = "Car Model";
                ws.Cells["B1"].Value = "Manufacturer";
                ws.Cells["C1"].Value = "Seats";
                ws.Cells["D1"].Value = "Dealer";
                ws.Cells["E1"].Value = "TotalSum";
                ws.Cells["A1:E1"].Style.Font.Bold = true;

                int rowCount = 2;
                foreach (var sale in StartUp.GetSalesReportsFromMySqlDatabase(repository))
                {
                    ws.Cells["A" + rowCount].Value = sale.CarModel;
                    ws.Cells["B" + rowCount].Value = sale.Manufacturer;
                    ws.Cells["C" + rowCount].Value = sale.Seats;
                    ws.Cells["D" + rowCount].Value = sale.Dealer;
                    ws.Cells["E" + rowCount].Value = sale.TotalSum;
                    rowCount++;
                }
               
                xlPackage.Save();
            }
        }
    }
}
