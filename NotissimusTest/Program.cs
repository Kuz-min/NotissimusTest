using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using NotissimusTest.Database;
using NotissimusTest.Providers;
using NotissimusTest.Services;

namespace NotissimusTest;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Mapper
        builder.Services.AddSingleton((_) =>
        {
            var config = new TypeAdapterConfig();
            config.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
            return config;
        });
        builder.Services.AddScoped<IMapper, ServiceMapper>();

        //Database
        builder.Services.AddDbContext<TestDatabase>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        builder.Services.AddScoped<ITestDatabase>(provider => provider.GetRequiredService<TestDatabase>());

        //Providers
        builder.Services.AddTransient<IMarketProvider, MarketProvider>();

        //Services
        builder.Services.AddTransient<IOfferService, OfferService>();
        builder.Services.AddTransient<ICategoryService, CategoryService>();

        //Razor
        builder.Services.AddRazorPages();


        //Middlewares
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.MapRazorPages();

        app.Run();
    }
}