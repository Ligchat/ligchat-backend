using LigChat.Backend.Data.Interfaces.IRepositories;
using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Web.Extensions.Database;

namespace LigChat.Backend.Application.Repositories
{
    /// <summary>
    /// Implementação do repositório para gerenciamento de eventos de webhook.
    /// </summary>
    public class WebhookEventRepository : IWebhookEventRepositoryInterface, IDisposable
    {
        private readonly DatabaseConfiguration _context;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados.
        /// </summary>
        /// <param name="context">O contexto do banco de dados.</param>
        public WebhookEventRepository(DatabaseConfiguration context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); // Garante que o contexto não seja nulo
        }

        /// <summary>
        /// Adiciona um novo evento de webhook ao banco de dados.
        /// </summary>
        /// <param name="webhookEvent">O evento de webhook a ser adicionado.</param>
        /// <returns>O evento de webhook adicionado.</returns>
        public WebhookEvent Save(WebhookEvent webhookEvent)
        {
            if (webhookEvent == null) throw new ArgumentNullException(nameof(webhookEvent)); // Garante que o evento de webhook não seja nulo

            _context.WebhookEvents.Add(webhookEvent); // Adiciona o evento ao DbSet
            _context.SaveChanges(); // Salva as alterações no banco de dados
            return webhookEvent; // Retorna o evento salvo
        }

        /// <summary>
        /// Atualiza um evento de webhook existente no banco de dados.
        /// </summary>
        /// <param name="webhookEvent">O evento de webhook com as atualizações.</param>
        /// <returns>O evento de webhook atualizado.</returns>
        public WebhookEvent Update(int id, WebhookEvent webhookEvent)
        {
            if (webhookEvent == null) throw new ArgumentNullException(nameof(webhookEvent)); // Garante que o evento de webhook não seja nulo

            var existingWebhookEvent = _context.WebhookEvents.Find(id);

            if (existingWebhookEvent != null)
            {
                // Atualiza as propriedades do evento existente
                existingWebhookEvent.Name = webhookEvent.Name;
                existingWebhookEvent.WebhookId = webhookEvent.WebhookId;
                existingWebhookEvent.EventType = webhookEvent.EventType;
                existingWebhookEvent.Payload = webhookEvent.Payload;
                existingWebhookEvent.Status = webhookEvent.Status;
                existingWebhookEvent.SectorId = webhookEvent.SectorId;
                existingWebhookEvent.UpdatedAt = DateTime.UtcNow; // Atualiza a data de atualização

                _context.SaveChanges(); // Salva as alterações no banco de dados
                return existingWebhookEvent; // Retorna o evento atualizado
            }

            throw new ArgumentException("Webhook event not found."); // Lança uma exceção se o evento não for encontrado
        }

        /// <summary>
        /// Deleta um evento de webhook do banco de dados pelo ID.
        /// </summary>
        /// <param name="id">O ID do evento de webhook a ser deletado.</param>
        /// <returns>O evento de webhook deletado.</returns>
        public WebhookEvent Delete(int id)
        {
            var webhookEvent = _context.WebhookEvents.Find(id);

            if (webhookEvent != null)
            {
                _context.WebhookEvents.Remove(webhookEvent); // Remove o evento do DbSet
                _context.SaveChanges(); // Salva as alterações no banco de dados
                return webhookEvent; // Retorna o evento deletado
            }

            throw new ArgumentException("Webhook event not found."); // Lança uma exceção se o evento não for encontrado
        }

        /// <summary>
        /// Obtém um evento de webhook pelo ID.
        /// </summary>
        /// <param name="id">O ID do evento de webhook.</param>
        /// <returns>O evento de webhook com o ID especificado ou null se não encontrado.</returns>
        public WebhookEvent? GetById(int id)
        {
            return _context.WebhookEvents.Find(id);
        }

        /// <summary>
        /// Obtém todos os eventos de webhook.
        /// </summary>
        /// <returns>Uma coleção de todos os eventos de webhook.</returns>
        public IEnumerable<WebhookEvent> GetAll()
        {
            return _context.WebhookEvents.ToList();
        }

        /// <summary>
        /// Libera os recursos do contexto do banco de dados.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose(); // Libera os recursos do contexto do banco de dados
        }
    }
}
