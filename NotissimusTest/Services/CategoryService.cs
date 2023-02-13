using Microsoft.EntityFrameworkCore;
using NotissimusTest.Database;
using NotissimusTest.Models;
using NotissimusTest.Providers;

namespace NotissimusTest.Services;

public class CategoryService : ICategoryService
{
    public CategoryService(ILogger<OfferService> logger, ITestDatabase database, IMarketProvider marketProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _database = database ?? throw new ArgumentNullException(nameof(database));
        _marketProvider = marketProvider ?? throw new ArgumentNullException(nameof(marketProvider));
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        var category = await _database.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            category = await _marketProvider.GetCategoryByIdAsync(id);

            if (category != null)
            {
                await _database.Categories.AddAsync(category);
                await _database.SaveChangesAsync();
                _logger.LogInformation($"category {id} saved");
            }
        }

        return category;
    }

    private readonly ILogger<OfferService> _logger;
    private readonly ITestDatabase _database;
    private readonly IMarketProvider _marketProvider;
}