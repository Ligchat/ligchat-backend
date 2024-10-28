using LigChat.Backend.Application.Common.Mappings.WebhookActionResults;
using LigChat.Backend.Domain.DTOs.WebhookDto;

namespace LigChat.Backend.Application.Interface.WebhookInterface
{
    public interface IWebhookServiceInterface
    {
        // Retorna uma coleção de webhooks encapsulada em um DTO com mensagem e código.
        // O DTO WebhookListResponse contém a lista de webhooks e informações adicionais, como mensagens de status.
        WebhookListResponse GetAll();

        // Retorna um único webhook com base no ID.
        // O retorno é encapsulado em um DTO SingleWebhookResponse com mensagem e código de status.
        // Se o webhook não for encontrado, pode retornar nulo.
        SingleWebhookResponse? GetById(int id);

        // Cria um novo webhook com base nos dados fornecidos no DTO CreateWebhookRequestDTO.
        // Retorna um DTO SingleWebhookResponse com mensagem e código de status, indicando sucesso ou falha.
        SingleWebhookResponse? Save(CreateWebhookRequestDTO webhookDto);

        // Atualiza um webhook existente com base no ID e nos dados fornecidos no DTO UpdateWebhookRequestDTO.
        // Retorna um DTO SingleWebhookResponse com mensagem e código de status, indicando sucesso ou falha.
        SingleWebhookResponse? Update(int id, UpdateWebhookRequestDTO webhookDto);

        // Deleta um webhook com base no ID.
        // Retorna um DTO SingleWebhookResponse com mensagem e código de status, indicando sucesso ou falha.
        // Se o webhook não for encontrado, pode retornar nulo.
        SingleWebhookResponse? Delete(int id);
    }
}
