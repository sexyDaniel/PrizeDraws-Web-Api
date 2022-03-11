using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrizeDraws.WebApi.Models;

namespace PrizeDraws.WebApi.Persistence.Configurations;

public class PromotionConfiguration:IEntityTypeConfiguration<Promotion>
{
    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired();
    }
}