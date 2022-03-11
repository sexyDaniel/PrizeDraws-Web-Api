using System.Text.Json.Serialization;

namespace PrizeDraws.WebApi.Models;

public class Participant
{
    public int Id { get; set; }
    [JsonIgnore]
    public int PromotionId { get; set; }
    public string Name { get; set; }
}