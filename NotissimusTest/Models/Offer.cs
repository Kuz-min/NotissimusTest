namespace NotissimusTest.Models;

public class Offer
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int Bid { get; set; }
    public int Cbid { get; set; }
    public bool Available { get; set; }
    public string Url { get; set; }
    public int Price { get; set; }
    public string CurrencyId { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string Picture { get; set; }
    public bool Delivery { get; set; }
    public string Description { get; set; }
}