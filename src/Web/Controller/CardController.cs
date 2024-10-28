namespace tests_.src.Web.Controller
{
    using global::tests_.src.Application.Interface.CardInterface;
    using global::tests_.src.Domain.Entities;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
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
        public ActionResult<Card> CreateCard(Card card)
        {
            var createdCard = _cardService.Create(card);
            return CreatedAtAction(nameof(GetCard), new { id = createdCard.Id }, createdCard);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCard(int id, Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            _cardService.Update(card);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCard(int id)
        {
            _cardService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}/move")]
        public IActionResult MoveCard(int id, int newColumnId)
        {
            var card = _cardService.GetById(id);
            if (card == null)
            {
                return NotFound();
            }

            _cardService.MoveCard(id, newColumnId);
            return NoContent();
        }
    }
}