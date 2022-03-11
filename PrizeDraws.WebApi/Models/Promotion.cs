namespace PrizeDraws.WebApi.Models;

public class Promotion
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Prize> Prizes { get; set; } = new List<Prize>();
    public List<Participant> Participants { get; set; } = new List<Participant>();
}