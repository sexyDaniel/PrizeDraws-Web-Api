using System.Text.Json.Serialization;

namespace PrizeDraws.WebApi.Models;

public class Raffle
{
    public int Id { get; set; }
    [JsonIgnore]
    public int PromotionId { get; set; }
    public int ParticipantId { get; set; }
    public int PrizeId { get; set; }
}