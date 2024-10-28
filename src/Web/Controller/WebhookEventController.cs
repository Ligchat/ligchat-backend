using Microsoft.AspNetCore.Mvc;
using LigChat.Backend.Application.Interface.WebhookEventInterface;
using LigChat.Backend.Application.Common.Mappings.WebhookActionResults;
using LigChat.Backend.Domain.DTOs.WebhookEventDto;

namespace LigChat.Backend.Web.Controller
{
    [ApiController]
    [Route("api/webhook-events")]
    public class WebhookEventController : ControllerBase, IWebhookEventControllerInterface
    {
        private readonly IWebhookEventServiceInterface _webhookEventService;

        // Construtor que injeta o serviço de eventos de webhook.
        // Recebe uma instância de IWebhookEventServiceInterface que será usada para operações de CRUD.
        public WebhookEventController(IWebhookEventServiceInterface webhookEventService)
        {
            _webhookEventService = webhookEventService ?? throw new ArgumentNullException(nameof(webhookEventService));
        }

        /// <summary>
        /// Obtém todos os eventos de webhook.
        /// Retorna uma resposta com uma lista de eventos de webhook.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var webhookEventListResponse = _webhookEventService.GetAll();
            if (webhookEventListResponse == null || !webhookEventListResponse.Data.Any())
            {
                // Retorna uma resposta 404 se nenhum evento de webhook for encontrado.
                return NotFound(new WebhookEventListResponse
                {
                    Message = "No webhook events found.",
                    Code = "404",
                    Data = new List<WebhookEventViewModel>()
                });
            }

            return Ok(webhookEventListResponse);
        }

        /// <summary>
        /// Obtém um evento de webhook pelo ID.
        /// Retorna uma resposta com os dados do evento ou uma mensagem de erro se o evento não for encontrado.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var webhookEventResponse = _webhookEventService.GetById(id);

            if (webhookEventResponse == null)
            {
                // Retorna uma resposta 404 se o evento de webhook não for encontrado.
                return NotFound(new SingleWebhookEventResponse
                {
                    Message = "Webhook event not found.",
                    Code = "404",
                    Data = null
                });
            }

            return Ok(webhookEventResponse);
        }

        /// <summary>
        /// Cria um novo evento de webhook com base nos dados fornecidos.
        /// Retorna uma resposta com os dados do evento criado ou uma mensagem de erro se a criação falhar.
        /// </summary>
        [HttpPost]
        public IActionResult Save([FromBody] CreateWebhookEventRequestDTO webhookEventDto)
        {
            if (webhookEventDto == null)
            {
                // Retorna uma resposta 400 se os dados do evento não forem fornecidos.
                return BadRequest("Webhook event data is required.");
            }

            var createdWebhookEventResponse = _webhookEventService.Save(webhookEventDto);

            if (createdWebhookEventResponse == null)
            {
                // Retorna uma resposta 400 se a criação do evento falhar.
                return BadRequest(new SingleWebhookEventResponse
                {
                    Message = "Webhook event could not be created.",
                    Code = "400",
                    Data = null
                });
            }

            // Retorna uma resposta 201 com a localização do novo evento.
            return CreatedAtAction(
                nameof(GetById),
                new { id = createdWebhookEventResponse.Data?.Id },
                createdWebhookEventResponse
            );
        }

        /// <summary>
        /// Atualiza um evento de webhook existente com base nos dados fornecidos.
        /// Retorna uma resposta 204 se a atualização for bem-sucedida ou uma mensagem de erro se a atualização falhar.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateWebhookEventRequestDTO webhookEventDto)
        {
            if (webhookEventDto == null)
            {
                // Retorna uma resposta 400 se os dados do evento não forem fornecidos.
                return BadRequest("Webhook event data is required.");
            }

            var existingWebhookEventResponse = _webhookEventService.GetById(id);

            if (existingWebhookEventResponse == null)
            {
                // Retorna uma resposta 404 se o evento não for encontrado.
                return NotFound(new SingleWebhookEventResponse
                {
                    Message = "Webhook event not found.",
                    Code = "404",
                    Data = null
                });
            }

            var updatedWebhookEventResponse = _webhookEventService.Update(id, webhookEventDto);
            if (updatedWebhookEventResponse == null)
            {
                // Retorna uma resposta 400 se a atualização falhar.
                return BadRequest(new SingleWebhookEventResponse
                {
                    Message = "Webhook event could not be updated.",
                    Code = "400",
                    Data = null
                });
            }

            // Retorna uma resposta 204 indicando que a atualização foi bem-sucedida.
            return NoContent();
        }

        /// <summary>
        /// Deleta um evento de webhook pelo ID.
        /// Retorna uma resposta 204 se a exclusão for bem-sucedida ou uma mensagem de erro se o evento não for encontrado.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var webhookEventResponse = _webhookEventService.GetById(id);

            if (webhookEventResponse == null)
            {
                // Retorna uma resposta 404 se o evento não for encontrado.
                return NotFound(new SingleWebhookEventResponse
                {
                    Message = "Webhook event not found.",
                    Code = "404",
                    Data = null
                });
            }

            _webhookEventService.Delete(id);
            // Retorna uma resposta 204 indicando que a exclusão foi bem-sucedida.
            return NoContent();
        }
    }
}
