using System;
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
