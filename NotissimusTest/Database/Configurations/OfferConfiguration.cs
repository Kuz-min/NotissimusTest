using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotissimusTest.Models;

namespace NotissimusTest.Database;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.UseTptMappingStrategy();
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id).ValueGeneratedNever();
    }
}