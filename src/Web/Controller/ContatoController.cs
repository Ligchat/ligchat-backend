using LigChat.Backend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using tests_.src.Application.Interface.ContatoInterface;

namespace tests_.src.Web.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _contatoService;

        public ContatoController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetContatos([FromQuery] int sectorId)
        {
            var contatos = _contatoService.GetAll(sectorId);
            return Ok(contatos);
        }

        [HttpGet("{id}")]
        public ActionResult<Contact> GetContato(int id)
        {
            var contato = _contatoService.GetById(id);
            if (contato == null)
            {
                return NotFound();
            }
            return Ok(contato);
        }

        [HttpPost]
        public ActionResult<Contact> CreateContato([FromBody] Contact contato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdContato = _contatoService.Create(contato);
            return CreatedAtAction(nameof(GetContato), new { id = createdContato.Id }, createdContato);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContato(int id, [FromBody] Contact contato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if ((int)contato.Id != id)
            {
                return BadRequest();
            }

            _contatoService.Update(contato);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContato(int id)
        {
            var contato = _contatoService.GetById(id);
            if (contato == null)
            {
                return NotFound();
            }

            _contatoService.Delete(id);
            return NoContent();
        }
    }
} 