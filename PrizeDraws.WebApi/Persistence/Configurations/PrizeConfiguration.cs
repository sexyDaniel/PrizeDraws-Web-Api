using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrizeDraws.WebApi.Models;

namespace PrizeDraws.WebApi.Persistence.Configurations;

public class PrizeConfiguration:IEntityTypeConfiguration<Prize>
{
    public void Configure(EntityTypeBuilder<Prize> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Description).IsRequired();
    }
}