using Microsoft.EntityFrameworkCore;
using NotissimusTest.Models;
using System.Reflection;

namespace NotissimusTest.Database;

public class TestDatabase : DbContext, ITestDatabase
{
    public DbSet<Offer> Offers { get; set; }
    public DbSet<ArtistTitle> ArtistTitles { get; set; }
    public DbSet<Category> Categories { get; set; }

    public TestDatabase(DbContextOptions<TestDatabase> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}