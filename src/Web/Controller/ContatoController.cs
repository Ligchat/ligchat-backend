namespace tests_.src.Web.Controller
{
    using global::tests_.src.Application.Interface.ContatoInterface;
    using global::tests_.src.Domain.Entities;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [ApiController]
    [Route("api/[controller]")]
    public class ContatosController : ControllerBase
    {
        private readonly IContatoService _contatoService;

        public ContatosController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contato>> GetContatos(int sectorId)
        {
            var contatos = _contatoService.GetAll(sectorId);
            return Ok(contatos);
        }

        [HttpGet("{id}")]
        public ActionResult<Contato> GetContato(int id)
        {
            var contato = _contatoService.GetById(id);
            if (contato == null)
            {
                return NotFound();
            }
            return Ok(contato);
        }

        [HttpPost]
        public ActionResult<Contato> CreateContato(Contato contato)
        {
            var createdContato = _contatoService.Create(contato);
            return CreatedAtAction(nameof(GetContato), new { id = createdContato.Id }, createdContato);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContato(int id, Contato contato)
        {
            if (id == null)
            {
                return BadRequest();
            }

            _contatoService.Update(contato);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContato(int id)
        {
            _contatoService.Delete(id);
            return NoContent();
        }
    }

}
