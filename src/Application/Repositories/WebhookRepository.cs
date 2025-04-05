using LigChat.Backend.Data.Interfaces.IRepositories;
using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Web.Extensions.Database;

namespace LigChat.Backend.Application.Repositories
{
    /// <summary>
    /// Implementação do repositório para gerenciamento de webhooks.
    /// </summary>
    public class WebhookRepository : IWebhookRepositoryInterface, IDisposable
    {
        private readonly DatabaseConfiguration _context;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados.
        /// </summary>
        /// <param name="context">O contexto do banco de dados.</param>
        public WebhookRepository(DatabaseConfiguration context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); // Garante que o contexto não seja nulo
        }

        /// <summary>
        /// Adiciona um novo webhook ao banco de dados.
        /// </summary>
        /// <param name="webhook">O webhook a ser adicionado.</param>
        /// <returns>O webhook adicionado.</returns>
        public Webhook Save(Webhook webhook)
        {
            if (webhook == null) throw new ArgumentNullException(nameof(webhook)); // Garante que o webhook não seja nulo

            _context.Webhooks.Add(webhook); // Adiciona o webhook ao DbSet
            _context.SaveChanges(); // Salva as alterações no banco de dados
            return webhook; // Retorna o webhook salvo
        }

        /// <summary>
        /// Atualiza um webhook existente no banco de dados.
        /// </summary>
        /// <param name="id">O ID do webhook a ser atualizado.</param>
        /// <param name="webhook">O webhook com as atualizações.</param>
        /// <returns>O webhook atualizado.</returns>
        public Webhook Update(int id, Webhook webhook)
        {
            if (webhook == null) throw new ArgumentNullException(nameof(webhook)); // Garante que o webhook não seja nulo

            var existingWebhook = _context.Webhooks.Find(id);

            if (existingWebhook != null)
            {
                // Atualiza as propriedades do webhook existente
                existingWebhook.Name = webhook.Name;
                existingWebhook.CallbackUrl = webhook.CallbackUrl;
                existingWebhook.SectorId = webhook.SectorId;
                existingWebhook.UpdatedAt = DateTime.UtcNow; // Atualiza a data de atualização

                _context.SaveChanges(); // Salva as alterações no banco de dados
                return existingWebhook; // Retorna o webhook atualizado
            }

            throw new ArgumentException("Webhook not found."); // Lança uma exceção se o webhook não for encontrado
        }

        /// <summary>
        /// Deleta um webhook do banco de dados pelo ID.
        /// </summary>
        /// <param name="id">O ID do webhook a ser deletado.</param>
        /// <returns>O webhook deletado.</returns>
        public Webhook Delete(int id)
        {
            var webhook = _context.Webhooks.Find(id);

            if (webhook != null)
            {
                _context.Webhooks.Remove(webhook); // Remove o webhook do DbSet
                _context.SaveChanges(); // Salva as alterações no banco de dados
                return webhook; // Retorna o webhook deletado
            }

            throw new ArgumentException("Webhook not found."); // Lança uma exceção se o webhook não for encontrado
        }

        /// <summary>
        /// Obtém um webhook pelo ID.
        /// </summary>
        /// <param name="id">O ID do webhook.</param>
        /// <returns>O webhook com o ID especificado ou null se não encontrado.</returns>
        public Webhook? GetById(int id)
        {
            return _context.Webhooks.Find(id);
        }

        /// <summary>
        /// Obtém todos os webhooks.
        /// </summary>
        /// <returns>Uma coleção de todos os webhooks.</returns>
        public IEnumerable<Webhook> GetAll(int sectorId)
        {
            return _context.Webhooks
                           .Where(webhook => webhook.SectorId == sectorId)
                           .ToList();
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