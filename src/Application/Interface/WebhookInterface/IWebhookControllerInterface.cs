using LigChat.Backend.Domain.DTOs.WebhookDto;
using Microsoft.AspNetCore.Mvc;

namespace LigChat.Backend.Application.Interface.WebhookInterface
{
    public interface IWebhookControllerInterface
    {
        // Retorna todos os webhooks. Deve retornar um IActionResult que pode incluir um status code e os dados dos webhooks.
        IActionResult GetAll(int sectorId);

        // Retorna um webhook específico baseado no ID. Deve retornar um IActionResult com o status e os dados do webhook.
        IActionResult GetById(int id);

        // Cria um novo webhook com base no CreateWebhookRequestDTO. Deve retornar um IActionResult com o status da criação.
        IActionResult Save(CreateWebhookRequestDTO webhook);

        // Atualiza um webhook existente com base no ID e no UpdateWebhookRequestDTO. Deve retornar um IActionResult com o status da atualização.
        IActionResult Update(int id, UpdateWebhookRequestDTO webhook);

        // Deleta um webhook baseado no ID. Deve retornar um IActionResult com o status da exclusão.
        IActionResult Delete(int id);
    }
}
