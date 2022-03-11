using System.ComponentModel.DataAnnotations;

namespace PrizeDraws.WebApi.Dtos;

public class CreatePromotionDto
{
    [Required(ErrorMessage = "Не указано название розыгрыша")]
    public string Name { get; set; }
    public string Description { get; set; }
}