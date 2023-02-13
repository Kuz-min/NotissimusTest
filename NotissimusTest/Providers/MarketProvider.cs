using MapsterMapper;
using NotissimusTest.Constants;
using NotissimusTest.Models;
using NotissimusTest.Providers.Models;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NotissimusTest.Providers;

public class MarketProvider : IMarketProvider
{
    public MarketProvider(IMapper mapper)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Offer?> GetOfferByIdAsync(int id)
    {
        var content = await LoadAsync();
        var offer = ParseOffer(content, id);
        return offer;
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        var content = await LoadAsync();
        var category = ParseCategory(content, id);
        return category;
    }

    private async Task<string> LoadAsync()
    {
        HttpClient http = new HttpClient();
        var httpResponse = await http.GetAsync("http://partner.market.yandex.ru/pages/help/YML.xml");
        httpResponse.EnsureSuccessStatusCode();
        var bytes = await httpResponse.Content.ReadAsByteArrayAsync();
        return Encoding.GetEncoding("windows-1251").GetString(bytes, 0, bytes.Length);
    }

    private Offer? ParseOffer(string content, int id)
    {
        var xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(content);
        var xmlOffer = xmlDocument.SelectSingleNode($"/yml_catalog/shop/offers/offer[@id={id}]");

        if (xmlOffer == null)
            return null;

        var xmlSerializer = new XmlSerializer(typeof(OfferDto));
        var offerDto = (OfferDto)xmlSerializer.Deserialize(new XmlNodeReader(xmlOffer))!;

        object detailsDto = offerDto.type switch
        {
            OfferType.ArtistTitle => new XmlSerializer(typeof(ArtistTitleDto)).Deserialize(new XmlNodeReader(xmlOffer))!,
            _ => throw new InvalidOperationException($"unsupported or empty offer type {offerDto?.type}"),
        };

        Offer offer = offerDto.type switch
        {
            OfferType.ArtistTitle => new ArtistTitle(),
            _ => throw new InvalidOperationException($"unsupported or empty offer type {offerDto?.type}"),
        };

        _mapper.Map(offerDto, offer);
        _mapper.Map(detailsDto, offer, detailsDto.GetType(), offer.GetType());

        return offer;
    }

    private Category? ParseCategory(string content, int id)
    {
        var xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(content);
        var xmlCategory = xmlDocument.SelectSingleNode($"/yml_catalog/shop/categories/category[@id={id}]");

        if (xmlCategory == null)
            return null;

        var xmlSerializer = new XmlSerializer(typeof(CategoryDto));
        var categoryDto = (CategoryDto)xmlSerializer.Deserialize(new XmlNodeReader(xmlCategory))!;

        var category = _mapper.Map<Category>(categoryDto);

        return category;
    }

    private readonly IMapper _mapper;
}