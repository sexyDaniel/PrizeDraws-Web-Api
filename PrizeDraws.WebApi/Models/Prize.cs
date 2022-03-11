using System.Text.Json.Serialization;

namespace PrizeDraws.WebApi.Models;

public class Prize
{
    public int Id { get; set; }
    [JsonIgnore]
    public int PromotionId { get; set; }
    public string Description { get; set; }
}