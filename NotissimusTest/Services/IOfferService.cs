using NotissimusTest.Models;

namespace NotissimusTest.Services;

public interface IOfferService
{
    Task<Offer?> GetByIdAsync(int id);
}