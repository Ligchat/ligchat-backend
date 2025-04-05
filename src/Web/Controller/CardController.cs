namespace tests_.src.Web.Controller
{
    using global::tests_.src.Application.Interface.CardInterface;
    using global::tests_.src.Domain.Entities;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using LigChat.Backend.Web.Extensions.Database;
    using global::tests_.src.Domain.DTOs.CardDto;


    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly DatabaseConfiguration _context;

        public CardsController(ICardService cardService, DatabaseConfiguration context)
        {
            _cardService = cardService;
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Card>> GetCards(int sectorId)
        {
            var cards = _cardService.GetAll(sectorId);
            return Ok(cards);
        }

        [HttpGet("{id}")]
        public ActionResult<Card> GetCard(int id)
        {
            var card = _cardService.GetById(id);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }

        [HttpPost]
        public ActionResult<Card> CreateCard([FromBody] CreateCardRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCard = _cardService.Create(request);
            return Ok(createdCard);
        }

        [HttpPut("{id}")]
        public ActionResult<Card> UpdateCard(int id, Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            var updatedCard = _cardService.Update(card);
            return Ok(updatedCard);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCard(int id)
        {
            _cardService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}/move")]
        public IActionResult MoveCard(int id, [FromBody] MoveCardRequest request)
        {
            var card = _cardService.GetById(id);
            if (card == null)
            {
                return NotFound();
            }

            try
            {
                _cardService.MoveCard(id, request.NewColumnId, request.NewPosition);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro ao mover o card: {ex.Message}" });
            }
        }

        public class CreateCardRequest
        {
            [Required]
            public int ContactId { get; set; }
            public int? ColumnId { get; set; }
            public int Position { get; set; }
            public int SectorId { get; set; }
            public string? Priority { get; set; }
            public string? ContactStatus { get; set; }
            public string? Content { get; set; }
            public DateTime? LastContact { get; set; }
            public int? AssignedTo { get; set; }
        }

        public class MoveCardRequest
        {
            public int NewColumnId { get; set; }
            public int NewPosition { get; set; }
        }
    }
}