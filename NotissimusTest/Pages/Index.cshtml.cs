using Microsoft.AspNetCore.Mvc.RazorPages;
using NotissimusTest.Models;
using NotissimusTest.Services;

namespace NotissimusTest.Pages;

public class IndexModel : PageModel
{
    public Offer? Offer { get; private set; }

    public IndexModel(IOfferService offerService)
    {
        _offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
    }

    public async Task OnGet()
    {
        Offer = await _offerService.GetByIdAsync(12344);
    }

    private readonly IOfferService _offerService;
}