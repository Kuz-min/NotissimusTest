using Microsoft.EntityFrameworkCore;
using NotissimusTest.Models;

namespace NotissimusTest.Database;

public interface ITestDatabase
{
    DbSet<ArtistTitle> ArtistTitles { get; }
    DbSet<Offer> Offers { get; }
    DbSet<Category> Categories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}