using System.Xml.Serialization;

namespace NotissimusTest.Providers.Models;

[Serializable, XmlRoot("offer")]
public class OfferDto
{
    [XmlAttribute] public int id { get; set; }
    [XmlAttribute] public string type { get; set; }
    [XmlAttribute] public int bid { get; set; }
    [XmlAttribute] public int cbid { get; set; }
    [XmlAttribute] public bool available { get; set; }
    [XmlElement] public string url { get; set; }
    [XmlElement] public int price { get; set; }
    [XmlElement] public string currencyId { get; set; }
    [XmlElement] public int categoryId { get; set; }
    [XmlElement] public string picture { get; set; }
    [XmlElement] public bool delivery { get; set; }
    [XmlElement] public string description { get; set; }
}