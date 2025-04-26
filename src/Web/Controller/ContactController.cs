using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LigChat.Backend.Application.Common.Mappings.ContactActionResults;
using LigChat.Backend.Application.Interface.ContactInterface;
using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Domain.DTOs.ContactDto;

namespace LigChat.Backend.Web.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase, IContactControllerInterface
    {
        private readonly IContactServiceInterface _contactService;

        // Construtor que injeta o serviço de contato.
        // Recebe uma instância de IContactServiceInterface que será usada para operações de CRUD.
        public ContactController(IContactServiceInterface contactService)
        {
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
        }

        /// <summary>
        /// Obtém todos os contatos.
        /// Retorna uma resposta com uma lista de contatos.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll([FromQuery] bool? isOfficial = null)
        {
            var response = _contactService.GetAll();
            
            if (isOfficial.HasValue)
            {
                response.Data = response.Data.Where(c => c.IsOfficial == isOfficial.Value).ToList();
            }

            return StatusCode(int.Parse(response.Code), response);
        }

        /// <summary>
        /// Obtém um contato pelo ID.
        /// Retorna uma resposta com os dados do contato ou uma mensagem de erro se o contato não for encontrado.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var response = _contactService.GetById(id);
            if (response == null)
            {
                return NotFound();
            }
            return StatusCode(int.Parse(response.Code), response);
        }

        /// <summary>
        /// Cria um novo contato com base nos dados fornecidos.
        /// Retorna uma resposta com os dados do contato criado ou uma mensagem de erro se a criação falhar.
        /// </summary>
        [HttpPost]
        public IActionResult Save([FromBody] CreateContactRequestDTO contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _contactService.Save(contact);
            return StatusCode(int.Parse(response.Code), response);
        }

        /// <summary>
        /// Atualiza um contato existente com base nos dados fornecidos.
        /// Retorna uma resposta 204 se a atualização for bem-sucedida ou uma mensagem de erro se a atualização falhar.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateContactRequestDTO contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id <= 0)
            {
                return BadRequest("Invalid ID");
            }

            var response = _contactService.Update(id, contact);
            return StatusCode(int.Parse(response.Code), response);
        }

        /// <summary>
        /// Deleta um contato pelo ID.
        /// Retorna uma resposta 204 se a exclusão for bem-sucedida ou uma mensagem de erro se o contato não for encontrado.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID");
            }

            var response = _contactService.Delete(id);
            return StatusCode(int.Parse(response.Code), response);
        }

        [HttpGet("sector/{sectorId}")]
        public IActionResult GetBySector([FromRoute] int sectorId, [FromQuery] bool? isOfficial = null)
        {
            var response = _contactService.GetBySector(sectorId);
            
            if (isOfficial.HasValue)
            {
                response.Data = response.Data.Where(c => c.IsOfficial == isOfficial.Value).ToList();
            }
            
            if (!response.Data.Any())
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }
    }
}
