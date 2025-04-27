using Microsoft.AspNetCore.Mvc;
using tests_.src.Application.Interface.ColunaInterface;
using tests_.src.Domain.Entities;

namespace tests_.src.Web.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColunasController : ControllerBase
    {
        private readonly IColunaService _colunaService;

        public ColunasController(IColunaService colunaService)
        {
            _colunaService = colunaService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Coluna>> GetColunas(int sectorId)
        {
            var colunas = _colunaService.GetAll(sectorId);
            return Ok(colunas);
        }

        [HttpGet("{id}")]
        public ActionResult<Coluna> GetColuna(int id)
        {
            var coluna = _colunaService.GetById(id);
            if (coluna == null)
            {
                return NotFound();
            }
            return Ok(coluna);
        }

        [HttpPost]
        public ActionResult<Coluna> CreateColuna(Coluna coluna)
        {
            var createdColuna = _colunaService.Create(coluna);
            return CreatedAtAction(nameof(GetColuna), new { id = createdColuna.Id }, createdColuna);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateColuna(int id, Coluna coluna)
        {
            if (id != coluna.Id)
            {
                return BadRequest();
            }

            _colunaService.Update(coluna);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteColuna(int id)
        {
            if (_colunaService.TryDelete(id, out var error))
            {
                return NoContent();
            }
            return BadRequest(new { message = error });
        }

        [HttpPut("{id}/move")]
        public IActionResult MoveColuna(int id, [FromBody] MoveColunaRequest request)
        {
            var coluna = _colunaService.GetById(id);
            if (coluna == null)
            {
                return NotFound();
            }

            _colunaService.MoveColuna(id, request.NewPosition);
            return NoContent();
        }

        [HttpPut("{id}/name")]
        public IActionResult UpdateColunaName(int id, [FromBody] UpdateColunaNameRequest request)
        {
            if (request.Name == null || request.Name.Length > 27)
            {
                return BadRequest(new { message = "O nome da coluna deve ter no máximo 27 caracteres." });
            }
            var updated = _colunaService.UpdateName(id, request.Name);
            if (updated == null)
            {
                return NotFound();
            }
            return Ok(new { id = updated.Id, name = updated.Name });
        }

        public class MoveColunaRequest
        {
            public int NewPosition { get; set; }
        }

        public class UpdateColunaNameRequest
        {
            public string Name { get; set; }
        }
    }
}
