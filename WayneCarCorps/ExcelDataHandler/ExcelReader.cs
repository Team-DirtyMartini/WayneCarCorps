using System;
using System.Data.OleDb;
using System.Linq;
using Ionic.Zip;
using WayneCarCorps.Data;
using WayneCarCorps.Models;

namespace ExcelDataHandler
{
    public class ExcelReader
    {
        public static void ExtractZipFiles()
        {
            string zipToUnpack = "../../../Sales-Reports.zip";
            string unpackDirectory = "../../../Reports";

            using (ZipFile zipFile = ZipFile.Read(zipToUnpack))
            {
                foreach (ZipEntry zipEntry in zipFile)
                {
                    //    Console.WriteLine(zipEntry);
                    zipEntry.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                    string currentZip = zipEntry.FileName;
                    //   Console.WriteLine(currentZip);
                    if (currentZip.IndexOf("xls") >= 0)
                    {
                        GetExcelInformation(currentZip);
                    }
                }
            }
        }

        public static void GetExcelInformation(string fileName)
        {
            string connectionString = @"Provider= Microsoft.ACE.OLEDB.12.0;Data Source = ..\..\..\Reports\" + fileName + @";Extended Properties = ""Excel 12.0 Xml;HDR=YES""";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);

            dbConnection.Open();
            var dbContext = new WayneCarCorpsContext();

            int indexOfReportInFileName = fileName.LastIndexOf("-Report");
            int indexOfDealership = fileName.IndexOf("Dealership") + "dealership-".Length;
            var dealershipName = fileName.Substring(indexOfDealership, indexOfReportInFileName - indexOfDealership);

            using (dbConnection)
            {
                string xslStringCommand = @"SELECT * FROM [Sales$]";
                OleDbCommand xslCommand = new OleDbCommand(xslStringCommand, dbConnection);
                OleDbDataReader reader = xslCommand.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        //Checks if current row is Null. reader.Read() doesn't catch it.
                        if (reader["ModelId"] is DBNull)
                        {
                            break;
                        }

                        double modelId = (double)reader["ModelId"];
                        double soldCars = (double)reader["SoldCars"];
                        double pricePerCar = (double)reader["PricePerCar"];
                        double income = (double)reader["Income"];
                        string dateOfReport = fileName.Substring(fileName.Length - 14, 10);
                        int dealerShipId = dbContext.Dealers.Where(x => x.Name == dealershipName).Select(x => x.Id).FirstOrDefault();

                        DateTime date = DateTime.Parse(dateOfReport);

                        var sale = new Sale()
                        {
                            CarId = (int)modelId,
                            SoldCars = (int)soldCars,
                            PricePerCar = (int)pricePerCar,
                            IncomeFromCar = (int)income,
                            Date = date,
                            DealerId = dealerShipId
                        };

                        dbContext.Sales.Add(sale);
                    }
                }

                dbContext.SaveChanges();
            }
        }
    }
}