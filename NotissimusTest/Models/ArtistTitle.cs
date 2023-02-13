namespace NotissimusTest.Models;

public class ArtistTitle : Offer
{
    public string Title { get; set; }
    public string? Artist { get; set; }
    public string? Media { get; set; }
    public int? Year { get; set; }
}