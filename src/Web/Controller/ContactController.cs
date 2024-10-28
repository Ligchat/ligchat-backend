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
    [Route("api/whatsappContacts")]
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
        public IActionResult GetAll()
        {
            var contactListResponse = _contactService.GetAll();
            if (contactListResponse == null || !contactListResponse.Data.Any())
            {
                // Retorna uma resposta 404 se nenhum contato for encontrado.
                return NotFound(new ContactListResponse
                {
                    Message = "No contacts found.",
                    Code = "404",
                    Data = new List<ContactViewModel>()
                });
            }

            return Ok(contactListResponse);
        }

        /// <summary>
        /// Obtém um contato pelo ID.
        /// Retorna uma resposta com os dados do contato ou uma mensagem de erro se o contato não for encontrado.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var contactResponse = _contactService.GetById(id);

            if (contactResponse == null)
            {
                // Retorna uma resposta 404 se o contato não for encontrado.
                return NotFound(new SingleContactResponse
                {
                    Message = "Contact not found.",
                    Code = "404",
                    Data = null
                });
            }

            return Ok(contactResponse);
        }

        /// <summary>
        /// Cria um novo contato com base nos dados fornecidos.
        /// Retorna uma resposta com os dados do contato criado ou uma mensagem de erro se a criação falhar.
        /// </summary>
        [HttpPost]
        public IActionResult Save([FromBody] CreateContactRequestDTO contactDto)
        {
            if (contactDto == null)
            {
                // Retorna uma resposta 400 se os dados do contato não forem fornecidos.
                return BadRequest("Contact data is required.");
            }

            var createdContactResponse = _contactService.Save(contactDto);

            if (createdContactResponse == null)
            {
                // Retorna uma resposta 400 se a criação do contato falhar.
                return BadRequest(new SingleContactResponse
                {
                    Message = "Contact could not be created.",
                    Code = "400",
                    Data = null
                });
            }

            // Retorna uma resposta 201 com a localização do novo contato.
            return CreatedAtAction(
                nameof(GetById),
                new { id = createdContactResponse.Data?.Id },
                createdContactResponse
            );
        }

        /// <summary>
        /// Atualiza um contato existente com base nos dados fornecidos.
        /// Retorna uma resposta 204 se a atualização for bem-sucedida ou uma mensagem de erro se a atualização falhar.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateContactRequestDTO contactDto)
        {
            if (contactDto == null)
            {
                // Retorna uma resposta 400 se os dados do contato não forem fornecidos.
                return BadRequest("Contact data is required.");
            }

            var existingContactResponse = _contactService.GetById(id);

            if (existingContactResponse == null)
            {
                // Retorna uma resposta 404 se o contato não for encontrado.
                return NotFound(new SingleContactResponse
                {
                    Message = "Contact not found.",
                    Code = "404",
                    Data = null
                });
            }

            var updatedContactResponse = _contactService.Update(id, contactDto);
            if (updatedContactResponse == null)
            {
                // Retorna uma resposta 400 se a atualização falhar.
                return BadRequest(new SingleContactResponse
                {
                    Message = "Contact could not be updated.",
                    Code = "400",
                    Data = null
                });
            }

            // Retorna uma resposta 204 indicando que a atualização foi bem-sucedida.
            return NoContent();
        }

        /// <summary>
        /// Deleta um contato pelo ID.
        /// Retorna uma resposta 204 se a exclusão for bem-sucedida ou uma mensagem de erro se o contato não for encontrado.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contactResponse = _contactService.GetById(id);

            if (contactResponse == null)
            {
                // Retorna uma resposta 404 se o contato não for encontrado.
                return NotFound(new SingleContactResponse
                {
                    Message = "Contact not found.",
                    Code = "404",
                    Data = null
                });
            }

            _contactService.Delete(id);
            // Retorna uma resposta 204 indicando que a exclusão foi bem-sucedida.
            return NoContent();
        }
    }
}
