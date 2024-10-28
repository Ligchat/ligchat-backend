using LigChat.Backend.Domain.Entities;

namespace LigChat.Backend.Data.Interfaces.IRepositories
{
    public interface IWebhookRepositoryInterface
    {
        // Obtém todos os webhooks. Retorna uma coleção de webhooks.
        IEnumerable<Webhook> GetAll();

        // Obtém um webhook pelo ID. Retorna um webhook ou null se não encontrado.
        Webhook? GetById(int id);

        // Salva um novo webhook no repositório. Retorna o webhook salvo.
        Webhook Save(Webhook webhook);

        // Atualiza um webhook existente pelo ID. Retorna o webhook atualizado.
        Webhook Update(int id, Webhook webhook);

        // Deleta um webhook pelo ID. Retorna o webhook deletado.
        Webhook Delete(int id);
    }
}
