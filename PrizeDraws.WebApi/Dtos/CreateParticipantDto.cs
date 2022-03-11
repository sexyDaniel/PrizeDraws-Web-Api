using System.ComponentModel.DataAnnotations;

namespace PrizeDraws.WebApi.Dtos;

public class CreateParticipantDto
{
    [Required(ErrorMessage = "Не указано имя учатника")]
    public string Name { get; set; }
}