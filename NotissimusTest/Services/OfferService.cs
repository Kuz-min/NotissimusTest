using Microsoft.EntityFrameworkCore;
using NotissimusTest.Database;
using NotissimusTest.Models;
using NotissimusTest.Providers;

namespace NotissimusTest.Services;

public class OfferService : IOfferService
{
    public OfferService(ILogger<OfferService> logger, ITestDatabase database, ICategoryService categoryService, IMarketProvider marketProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _database = database ?? throw new ArgumentNullException(nameof(database));
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        _marketProvider = marketProvider ?? throw new ArgumentNullException(nameof(marketProvider));
    }

    public async Task<Offer?> GetByIdAsync(int id)
    {
        var offer = await _marketProvider.GetOfferByIdAsync(id);

        if (offer != null)
        {
            var category = await _categoryService.GetByIdAsync(offer.CategoryId);

            if (category == null)
                throw new Exception($"unknown category id {offer.CategoryId}");

            offer.Category = category;

            if (await _database.Offers.FirstOrDefaultAsync(o => o.Id == id) == null)
            {
                await _database.Offers.AddAsync(offer);
                await _database.SaveChangesAsync();
                _logger.LogInformation($"offer {id} saved");
            }
        }

        return offer;
    }

    private readonly ILogger<OfferService> _logger;
    private readonly ITestDatabase _database;
    private readonly ICategoryService _categoryService;
    private readonly IMarketProvider _marketProvider;
}