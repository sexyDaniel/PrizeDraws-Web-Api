using Microsoft.EntityFrameworkCore;
using PrizeDraws.WebApi.Models;
using PrizeDraws.WebApi.Persistence.Configurations;

namespace PrizeDraws.WebApi.Persistence;

public class MsSqlDbContext:DbContext,IDbContext
{
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<Prize> Prizes { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Raffle> Raffles { get; set; }

    public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options):base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PromotionConfiguration());
        modelBuilder.ApplyConfiguration(new RaffleConfiguration());
        modelBuilder.ApplyConfiguration(new ParticipantConfiguration());
        modelBuilder.ApplyConfiguration(new PrizeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}