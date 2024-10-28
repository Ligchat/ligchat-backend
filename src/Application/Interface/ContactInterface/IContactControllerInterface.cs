using LigChat.Backend.Domain.DTOs.ContactDto;
using Microsoft.AspNetCore.Mvc;

namespace LigChat.Backend.Application.Interface.ContactInterface
{
    public interface IContactControllerInterface
    {
        // Retorna todos os contatos. Deve retornar um IActionResult que pode incluir um status code e os dados dos contatos.
        IActionResult GetAll();

        // Retorna um contato específico baseado no ID. Deve retornar um IActionResult com o status e os dados do contato.
        IActionResult GetById(int id);

        // Cria um novo contato com base no CreateContactRequestDTO. Deve retornar um IActionResult com o status da criação.
        IActionResult Save(CreateContactRequestDTO contact);

        // Atualiza um contato existente com base no ID e no UpdateContactRequestDTO. Deve retornar um IActionResult com o status da atualização.
        IActionResult Update(int id, UpdateContactRequestDTO contact);

        // Deleta um contato baseado no ID. Deve retornar um IActionResult com o status da exclusão.
        IActionResult Delete(int id);
    }
}
