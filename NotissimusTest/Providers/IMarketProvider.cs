using NotissimusTest.Models;

namespace NotissimusTest.Providers;

public interface IMarketProvider
{
    Task<Offer?> GetOfferByIdAsync(int id);
    Task<Category?> GetCategoryByIdAsync(int id);
}