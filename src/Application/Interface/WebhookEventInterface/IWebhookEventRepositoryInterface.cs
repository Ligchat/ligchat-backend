using LigChat.Backend.Domain.Entities;

namespace LigChat.Backend.Data.Interfaces.IRepositories
{
    public interface IWebhookEventRepositoryInterface
    {
        // Obtém todos os eventos de webhook. Retorna uma coleção de eventos de webhook.
        IEnumerable<WebhookEvent> GetAll();

        // Obtém um evento de webhook pelo ID. Retorna um evento de webhook ou null se não encontrado.
        WebhookEvent? GetById(int id);

        // Salva um novo evento de webhook no repositório. Retorna o evento de webhook salvo.
        WebhookEvent Save(WebhookEvent webhookEvent);

        // Atualiza um evento de webhook existente pelo ID. Retorna o evento de webhook atualizado.
        WebhookEvent Update(int id, WebhookEvent webhookEvent);

        // Deleta um evento de webhook pelo ID. Retorna o evento de webhook deletado.
        WebhookEvent Delete(int id);
    }
}
