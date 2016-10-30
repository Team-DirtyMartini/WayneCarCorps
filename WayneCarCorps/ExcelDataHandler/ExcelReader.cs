using System;
using System.Data.OleDb;
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
                    Console.WriteLine(zipEntry);
                    zipEntry.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                    string currentZip = zipEntry.FileName;
                    Console.WriteLine(currentZip);
                    if (currentZip.IndexOf("xls") >= 0)
                    {
                        //TODO: Importing in SQL, but need more info! 
                        // No info in SQL Sevres, so it will throw 
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
                        if (reader["CarId"] is DBNull)
                        {
                            break;
                        }

                        double carId = (double)reader["CarId"];
                        double soldCars = (double)reader["SoldCars"];
                        double pricePerCar = (double)reader["PricePerCar"];
                        double income = (double)reader["Income"];
                        string dateOfReport = fileName.Substring(fileName.Length - 14, 10);
                        DateTime date = DateTime.Parse(dateOfReport);

                        var sale = new Sale()
                        {
                            CarId = (int)carId,
                            SoldCars = (int)soldCars,
                            PricePerCar = (int)pricePerCar,
                            IncomeFromCar = (int)income,
                            Date = date
                        };

                        dbContext.Sales.Add(sale);
                       // Console.WriteLine($"Card id: {carId} - SoldCars: {soldCars} - PricePerCar: {pricePerCar} - Income: {income}");
                    }
                }

                dbContext.SaveChanges();
            }
        }

        public static void Main(string[] args)
        {
            ExtractZipFiles();
        }
    }
}