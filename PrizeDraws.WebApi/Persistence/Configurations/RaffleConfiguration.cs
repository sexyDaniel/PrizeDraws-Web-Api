using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrizeDraws.WebApi.Models;

namespace PrizeDraws.WebApi.Persistence.Configurations;

public class RaffleConfiguration:IEntityTypeConfiguration<Raffle>
{
    public void Configure(EntityTypeBuilder<Raffle> builder)
    {
        builder.HasKey(r => new {r.ParticipantId, r.PrizeId, r.PromotionId});
    }
}