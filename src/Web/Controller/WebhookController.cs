using Microsoft.AspNetCore.Mvc;
using LigChat.Backend.Application.Common.Mappings.WebhookActionResults;
using LigChat.Backend.Application.Interface.WebhookInterface;
using LigChat.Backend.Domain.DTOs.WebhookDto;

namespace LigChat.Backend.Web.Controller
{
    [ApiController]
    [Route("api/webhooks")]
    public class WebhookController : ControllerBase, IWebhookControllerInterface
    {
        private readonly IWebhookServiceInterface _webhookService;

        // Construtor que injeta o serviço de webhook.
        // Recebe uma instância de IWebhookServiceInterface que será usada para operações de CRUD.
        public WebhookController(IWebhookServiceInterface webhookService)
        {
            _webhookService = webhookService ?? throw new ArgumentNullException(nameof(webhookService));
        }

        /// <summary>
        /// Obtém todos os webhooks.
        /// Retorna uma resposta com uma lista de webhooks.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll(int sectorId)
        {
            var webhookListResponse = _webhookService.GetAll(sectorId);
            if (webhookListResponse == null || !webhookListResponse.Data.Any())
            {
                // Retorna uma resposta 404 se nenhum webhook for encontrado.
                return NotFound(new WebhookListResponse
                {
                    Message = "No webhooks found.",
                    Code = "404",
                    Data = new List<WebhookViewModel>()
                });
            }

            return Ok(webhookListResponse);
        }

        /// <summary>
        /// Obtém um webhook pelo ID.
        /// Retorna uma resposta com os dados do webhook ou uma mensagem de erro se o webhook não for encontrado.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var webhookResponse = _webhookService.GetById(id);

            if (webhookResponse == null)
            {
                // Retorna uma resposta 404 se o webhook não for encontrado.
                return NotFound(new SingleWebhookResponse
                {
                    Message = "Webhook not found.",
                    Code = "404",
                    Data = null
                });
            }

            return Ok(webhookResponse);
        }

        /// <summary>
        /// Cria um novo webhook com base nos dados fornecidos.
        /// Retorna uma resposta com os dados do webhook criado ou uma mensagem de erro se a criação falhar.
        /// </summary>
        [HttpPost]
        public IActionResult Save([FromBody] CreateWebhookRequestDTO webhookDto)
        {
            if (webhookDto == null)
            {
                // Retorna uma resposta 400 se os dados do webhook não forem fornecidos.
                return BadRequest("Webhook data is required.");
            }

            var createdWebhookResponse = _webhookService.Save(webhookDto);

            if (createdWebhookResponse == null)
            {
                // Retorna uma resposta 400 se a criação do webhook falhar.
                return BadRequest(new SingleWebhookResponse
                {
                    Message = "Webhook could not be created.",
                    Code = "400",
                    Data = null
                });
            }

            // Retorna uma resposta 201 com a localização do novo webhook.
            return CreatedAtAction(
                nameof(GetById),
                new { id = createdWebhookResponse.Data?.Id },
                createdWebhookResponse
            );
        }

        /// <summary>
        /// Atualiza um webhook existente com base nos dados fornecidos.
        /// Retorna uma resposta 204 se a atualização for bem-sucedida ou uma mensagem de erro se a atualização falhar.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateWebhookRequestDTO webhookDto)
        {
            if (webhookDto == null)
            {
                // Retorna uma resposta 400 se os dados do webhook não forem fornecidos.
                return BadRequest("Webhook data is required.");
            }

            var existingWebhookResponse = _webhookService.GetById(id);

            if (existingWebhookResponse == null)
            {
                // Retorna uma resposta 404 se o webhook não for encontrado.
                return NotFound(new SingleWebhookResponse
                {
                    Message = "Webhook not found.",
                    Code = "404",
                    Data = null
                });
            }

            var updatedWebhookResponse = _webhookService.Update(id, webhookDto);
            if (updatedWebhookResponse == null)
            {
                // Retorna uma resposta 400 se a atualização falhar.
                return BadRequest(new SingleWebhookResponse
                {
                    Message = "Webhook could not be updated.",
                    Code = "400",
                    Data = null
                });
            }

            // Retorna uma resposta 204 indicando que a atualização foi bem-sucedida.
            return NoContent();
        }

        /// <summary>
        /// Deleta um webhook pelo ID.
        /// Retorna uma resposta 204 se a exclusão for bem-sucedida ou uma mensagem de erro se o webhook não for encontrado.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var webhookResponse = _webhookService.GetById(id);

            if (webhookResponse == null)
            {
                // Retorna uma resposta 404 se o webhook não for encontrado.
                return NotFound(new SingleWebhookResponse
                {
                    Message = "Webhook not found.",
                    Code = "404",
                    Data = null
                });
            }

            _webhookService.Delete(id);
            // Retorna uma resposta 204 indicando que a exclusão foi bem-sucedida.
            return NoContent();
        }
    }
}
