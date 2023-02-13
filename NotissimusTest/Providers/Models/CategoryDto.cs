using System.Xml.Serialization;

namespace NotissimusTest.Providers.Models;

[Serializable, XmlRoot("category")]
public class CategoryDto
{
    [XmlAttribute] public int id { get; set; }
    [XmlAttribute] public int parentId { get; set; }
    [XmlText] public string name { get; set; }
}