using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotissimusTest.Models;

namespace NotissimusTest.Database;

public class ArtistTitleConfiguration : IEntityTypeConfiguration<ArtistTitle>
{
    public void Configure(EntityTypeBuilder<ArtistTitle> builder)
    {
        builder.UseTptMappingStrategy();

        builder.Property(o => o.Artist).IsRequired(false);
        builder.Property(o => o.Media).IsRequired(false);
        builder.Property(o => o.Year).IsRequired(false);
    }
}