using MongoDBOperator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayneCarCorps.Data;
using WayneCarCorps.Models;

namespace WayneCarCorps.Importer
{
    public class ColorsImporter
    {
        private WayneCarCorpsContext db;
        private IEnumerable<WayneCarCorps.MongoDBModels.Colour> colors;
        public ColorsImporter(IEnumerable<WayneCarCorps.MongoDBModels.Colour> colors, WayneCarCorpsContext db)
        {
            this.db = db;
            this.colors = colors;
        }

        public void Import()
        {


            foreach (var item in this.colors)
            {
                var colorToAdd = new Color
                {
                    Name = item.Name
                };

                this.db.Colors.Add(colorToAdd);
            }
            db.SaveChanges();
        }
    }
}
