﻿using MongoDBOperator;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ExcelDataHandler;
using WayneCarCorps.Data;
using WayneCarCorps.Data.Migrations;
using WayneCarCorps.MongoDBModels;

namespace WayneCarCorps.ConsoleClient
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WayneCarCorpsContext, Configuration>());
            using (var ctx = new WayneCarCorpsContext())
            {
                Console.WriteLine(ctx.Cars.Count());
            }

            ExcelReader.ExtractZipFiles();
            PDFWriter.PdfExporter.CreatePdfTable();
            //UpdateMongoDB();
        }

        private static void UpdateMongoDB()
        {
            var mongoCarsCollection = CreateCarsCollection();
            var mongoDBLoader = new MongoDBLoader<MongoCar>();
            mongoDBLoader.LoadEntities(mongoCarsCollection, "Cars");
        }

        public static IEnumerable<MongoCar> CreateCarsCollection()
        {
            var carsCollection = new List<MongoCar>();

            carsCollection.Add(new MongoCar("VW", "Golf", "Estate", 2016, "Red", 150, 5, 17500, "Porsche Sofia East"));
            carsCollection.Add(new MongoCar("Lancia", "Delta", "Sallon", 2016, "White", 140, 5, 17800, "Auto Italia"));
            carsCollection.Add(new MongoCar("Skoda", "Octavia", "Estate", 2016, "Silver", 150, 5, 16500, "Euratek Auto"));
            carsCollection.Add(new MongoCar("BMW", "X5", "Off-Road", 2016, "Black", 320, 5, 75300, "Daru Car"));
            carsCollection.Add(new MongoCar("BMW", "M3", "Sport", 2016, "Blue", 530, 2, 56000, "Daru Car"));

            return carsCollection;
        }
    }
}
