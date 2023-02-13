using System.Xml.Serialization;

namespace NotissimusTest.Providers.Models;

[Serializable, XmlRoot("offer")]
public class ArtistTitleDto
{
    [XmlElement] public string artist { get; set; }
    [XmlElement] public string title { get; set; }
    [XmlElement] public int year { get; set; }
    [XmlElement] public string media { get; set; }
}