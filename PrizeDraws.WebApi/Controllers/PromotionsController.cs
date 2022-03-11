using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrizeDraws.WebApi.Dtos;
using PrizeDraws.WebApi.Models;
using PrizeDraws.WebApi.Persistence;

namespace PrizeDraws.WebApi.Controllers;

[ApiController]
[Route("promo")]
public class PromotionsController : Controller
{
    private IDbContext _context;
    public PromotionsController(IDbContext context)
    {
        _context = context;
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePromotion([FromRoute]int id,[FromBody] UpdatePromotionDto dto)
    {
        var promo = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id);

        if (promo == null)
        {
            return BadRequest("Такого розыгрыша нет");
        }

        promo.Description = dto.Description;
        promo.Name = string.IsNullOrEmpty(dto.Name) ? promo.Name : dto.Name;

        await _context.SaveChangesAsync(new CancellationToken());
        return Ok();
    }

    #region Delete

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePromotion([FromRoute] int id)
    {
        var promo = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id);

        if (promo == null)
        {
            return BadRequest("Такого розыгрыша нет");
        }

        _context.Promotions.Remove(promo);

        await _context.SaveChangesAsync(new CancellationToken());
        return Ok();
    }
    
    [HttpDelete("{id:int}/participant/{participantId:int}")]
    public async Task<IActionResult> DeleteParticipant([FromRoute] int id,[FromRoute] int participantId)
    {
        var promo = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id);

        if (promo == null)
        {
            return BadRequest("Такого розыгрыша нет");
        }

        var participant = await _context
            .Participants
            .FirstOrDefaultAsync(p=>p.PromotionId==id && p.Id ==participantId);

        if (participant == null)
        {
            return BadRequest("Такого пользователя нет");
        }

        _context.Participants.Remove(participant);
        await _context.SaveChangesAsync(new CancellationToken());
        
        return Ok();
    }
    
    [HttpDelete("{id:int}/prize/{prizeId:int}")]
    public async Task<IActionResult> DeletePrize([FromRoute] int id,[FromRoute] int prizeId)
    {
        var promo = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id);

        if (promo == null)
        {
            return BadRequest("Такого розыгрыша нет");
        }

        var prize = await _context
            .Prizes
            .FirstOrDefaultAsync(p=>p.PromotionId==id && p.Id ==prizeId);

        if (prize == null)
        {
            return BadRequest("Такого подарка нет");
        }

        _context.Prizes.Remove(prize);
        await _context.SaveChangesAsync(new CancellationToken());
        
        return Ok();
    }
    

    #endregion
    
    
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> PromotionById([FromRoute] int id)
    {
        var promo = from p in _context.Promotions
            where p.Id == id
            select new GetPromotionByIdDto()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Prizes = _context.Prizes.Where(prize => prize.PromotionId == id).ToList(),
                Participants = _context.Participants.Where(participant => participant.PromotionId == id).ToList(),
            };
                
        return Ok(promo);
    }

    [HttpGet]
    public async Task<IActionResult> Promotions()
    {
        var promos = await _context
            .Promotions
            .Select(p => new GetPromotionsDto()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            })
            .ToListAsync();
        
        return Ok(promos);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePromotion([FromBody] CreatePromotionDto dto)
    {
        var promo = new Promotion()
        {
            Name = dto.Name, 
            Description = dto.Description
        };

        await _context.Promotions.AddAsync(promo);
        await _context.SaveChangesAsync(new CancellationToken());
        
        return Ok(new {promoId = promo.Id});
    }
    
    [HttpPost("{id:int}/participant")]
    public async Task<IActionResult> CreateParticipant([FromRoute] int id,[FromBody] CreateParticipantDto dto)
    {
        var promo = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id);

        if (promo == null)
        {
            return BadRequest("Такого розыгрыша нет");
        }
        
        var participant = new Participant()
        {
            Name = dto.Name,
            PromotionId = id
        };
        
        await _context.Participants.AddAsync(participant);
        await _context.SaveChangesAsync(new CancellationToken());
        
        return Ok(new {participantId = participant.Id});
    }
    
    [HttpPost("{id:int}/prize")]
    public async Task<IActionResult> CreatePrize([FromRoute] int id,[FromBody] CreatePrizeDto dto)
    {
        var promo = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id);

        if (promo == null)
        {
            return BadRequest("Такого розыгрыша нет");
        }
        
        var prize = new Prize()
        {
            Description = dto.Description,
            PromotionId = id
        };
        
        await _context.Prizes.AddAsync(prize);
        await _context.SaveChangesAsync(new CancellationToken());
        
        return Ok(new {prizeId = prize.Id});
    }
    
    [HttpPost("{id:int}/raffle")]
    public async Task<IActionResult> SpendRaffle([FromRoute] int id)
    {
        var promo = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id);

        if (promo == null)
        {
            return BadRequest("Такого розыгрыша нет");
        }

        var prizes = await _context
            .Prizes
            .Where(p => p.PromotionId == id)
            .ToListAsync();
        
        var participants = await _context
                .Participants
                .Where(p => p.PromotionId == id)
                .Select(p=>p.Id)
                .OrderBy(p=>p)
                .ToListAsync();

        if (prizes.Count() != participants.Count)
        {
            return Conflict("Невозможно провести розыгрыш");
        }

        var random = new Random();

        foreach (var prize in prizes)
        {
            _context.Raffles.Add(new Raffle()
            {
                PromotionId = id,
                PrizeId = prize.Id,
                ParticipantId = random.Next(participants[0],participants[^1])
            });
        }
        await _context.SaveChangesAsync(new CancellationToken());

        var res = from r in _context.Raffles
            where r.PromotionId == id
            join a in _context.Participants on r.ParticipantId equals a.Id
            join prize in _context.Prizes on r.PrizeId equals prize.Id
            select new GetReffle()
            {
                Winner = new Participant() {Name = a.Name, Id = a.Id},
                Prizes = new Prize() {Description = prize.Description, Id = prize.Id}
            };

        return Ok(res.ToListAsync());
    }
}