using PrizeDraws.WebApi.Models;

namespace PrizeDraws.WebApi.Dtos;

public class GetReffle
{
    public Participant Winner { get; set; }
    public Prize Prizes { get; set; }
}