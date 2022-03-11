using Microsoft.EntityFrameworkCore;
using PrizeDraws.WebApi.Models;

namespace PrizeDraws.WebApi.Persistence;

public interface IDbContext
{
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<Prize> Prizes { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Raffle> Raffles { get; set; }

    Task<int> SaveChangesAsync(CancellationToken token);
}