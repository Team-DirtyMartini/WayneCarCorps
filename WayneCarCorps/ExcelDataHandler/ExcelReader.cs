using System;
using System.Data.OleDb;
using System.Linq;
using Ionic.Zip;
using WayneCarCorps.Data.Common;
using WayneCarCorps.Models;

namespace ExcelDataHandler
{
    public class ExcelReader
    {
        private const string ZipFileToUnpackLocation = "../../../Sales-Reports.zip";
        private const string PathToUnpackZip = "../../../Reports";

        private const string ConnectionString =
            @"Provider= Microsoft.ACE.OLEDB.12.0;Data Source = ..\..\..\Reports\{0};Extended Properties = ""Excel 12.0 Xml;HDR=YES""";

        private IRepository<Sale> repositorySales;
        private IRepository<Dealer> repositoryDealers;
        private IUnitOfWork unitOfWork;
        private IModelsFactory modelsFactory;

        public ExcelReader(IRepository<Sale> repositorySales, IRepository<Dealer> repositoryDealers, IUnitOfWork unitOfWork, IModelsFactory modelsFactory)
        {
            this.repositorySales = repositorySales;
            this.repositoryDealers = repositoryDealers;
            this.unitOfWork = unitOfWork;
            this.modelsFactory = modelsFactory;
        }

        public void ExtractZipFiles()
        {
            using (ZipFile zipFile = ZipFile.Read(ZipFileToUnpackLocation))
            {
                foreach (ZipEntry zipEntry in zipFile)
                {
                    zipEntry.Extract(PathToUnpackZip, ExtractExistingFileAction.OverwriteSilently);
                    string currentZip = zipEntry.FileName;

                    if (currentZip.IndexOf("xls") >= 0)
                    {
                        this.GetExcelInformation(currentZip);
                    }
                }
            }
        }

        private void GetExcelInformation(string fileName)
        {
            string connectionString = string.Format(ConnectionString, fileName);
            OleDbConnection dbConnection = new OleDbConnection(connectionString);

            dbConnection.Open();

            int indexOfReportInFileName = fileName.LastIndexOf("-Report");
            int indexOfDealership = fileName.IndexOf("Dealership") + "dealership-".Length;
            var dealershipName = fileName.Substring(indexOfDealership, indexOfReportInFileName - indexOfDealership);

            using (dbConnection)
            {
                string xslStringCommand = @"SELECT * FROM [Sales$]";
                OleDbCommand xslCommand = new OleDbCommand(xslStringCommand, dbConnection);
                OleDbDataReader reader = xslCommand.ExecuteReader();
                int count = 0;

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
                        int dealerShipId = this.repositoryDealers.All().Where(x => x.Name == dealershipName).Select(x => x.Id).FirstOrDefault();

                        DateTime date = DateTime.Parse(dateOfReport);

                        var sale = modelsFactory.CreateSale();

                        sale.CarId = (int)modelId;
                        sale.SoldCars = (int)soldCars;
                        sale.PricePerCar = (int)pricePerCar;
                        sale.IncomeFromCar = (int)income;
                        sale.Date = date;
                        sale.DealerId = dealerShipId;

                        if (count % 100 == 0)
                        {
                            unitOfWork.Commit();
                        }

                        this.repositorySales.Add(sale);
                    }
                }

                unitOfWork.Commit();
            }
        }
    }
}