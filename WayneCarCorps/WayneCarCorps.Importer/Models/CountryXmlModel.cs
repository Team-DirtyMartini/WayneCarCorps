using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WayneCarCorps.Importer.Models
{
    [Serializable]
    [XmlType("Country")]
    public class CountryXmlModel
    {
        public string Name { get; set; }
    }
}
