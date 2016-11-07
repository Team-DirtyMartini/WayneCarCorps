using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using WayneCarCorps.MySQL.Models;
using WayneCarCorps.MySQL.FluentModels;

namespace WayneCarCorps.MySQL.ConsoleClient
{
    public class StartUp
    {
        private const string JsonFilesPath = "../../../JsonReports";

        public static void Main(string[] args)
        {
            var files = ReadJsonFiles();

            var dbContext = new FluentModelContext();
            var repository = new OpenAccessRepository<SalesReport>(dbContext);
            var unitOfWork = new OpenAccessUnitOfWork(dbContext);

            ImportToMySqlDatabase(repository, unitOfWork, files);
        }

        private static IEnumerable<SalesReport> ReadJsonFiles()
        {
            var files = Directory.GetFiles(JsonFilesPath).Where(fileName => fileName.EndsWith(".json")).ToList();

            var sales = new List<SalesReport>();

            foreach (var file in files)
            {
                var fileContent = File.ReadAllText(file);
                var fileCars = JsonConvert.DeserializeObject<SalesReport>(fileContent);
                sales.Add(fileCars);
            }

            return sales;
        }

        private static void ImportToMySqlDatabase(OpenAccessRepository<SalesReport> salesRepository, OpenAccessUnitOfWork unitOfWork, IEnumerable<SalesReport> readFiles)
        {
            int count = 0;

            foreach (var report in readFiles)
            {
                salesRepository.Add(report);

                if (count % 100 == 0)
                {
                    unitOfWork.Commit();
                }

                count++;
            }

            unitOfWork.Commit();
        }
    }
}
