using LigChat.Data.Interfaces.IRepositories;
using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Web.Extensions.Database;
using Microsoft.EntityFrameworkCore;
using tests_.src.Application.Repositories;

namespace LigChat.Data.Repositories
{
    /// <summary>
    /// Implementação do repositório para gerenciamento de fluxos.
    /// </summary>
    public class FlowRepository : IFlowRepositoryInterface, IDisposable
    {
        private readonly DatabaseConfiguration _context;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados.
        /// </summary>
        /// <param name="context">O contexto do banco de dados.</param>
        public FlowRepository(DatabaseConfiguration context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Salva um novo fluxo no banco de dados.
        /// </summary>
        /// <param name="flow">O fluxo a ser salvo.</param>
        /// <returns>O fluxo salvo.</returns>
        public Flow Save(Flow flow)
        {
            if (flow == null) throw new ArgumentNullException(nameof(flow));

            // Atribuir um novo ObjectId em formato de string
            flow.Id = ObjectIdGenerator.GenerateRandomObjectId();
            _context.Flows.Add(flow);
            _context.SaveChanges();
            return flow;
        }

        /// <summary>
        /// Atualiza um fluxo existente no banco de dados.
        /// </summary>
        /// <param name="id">O ID do fluxo a ser atualizado.</param>
        /// <param name="flow">O fluxo com as atualizações.</param>
        /// <returns>O fluxo atualizado.</returns>
        public Flow Update(string id, Flow flow)
        {
            if (flow == null) throw new ArgumentNullException(nameof(flow));

            var existingFlow = _context.Flows.Find(id);

            if (existingFlow != null)
            {
                existingFlow.Name = flow.Name;
                existingFlow.FolderId = flow.FolderId;
                existingFlow.UpdatedAt = DateTime.UtcNow; // Atualiza a data de atualização

                _context.SaveChanges();
                return existingFlow;
            }

            throw new ArgumentException("Flow not found.");
        }

        /// <summary>
        /// Deleta um fluxo do banco de dados pelo ID.
        /// </summary>
        /// <param name="id">O ID do fluxo a ser deletado.</param>
        /// <returns>O fluxo deletado.</returns>
        public Flow Delete(string id)
        {
            var flow = _context.Flows.Find(id);

            if (flow != null)
            {
                _context.Flows.Remove(flow);
                _context.SaveChanges();
                return flow;
            }

            throw new ArgumentException("Flow not found.");
        }

        /// <summary>
        /// Obtém um fluxo pelo ID.
        /// </summary>
        /// <param name="id">O ID do fluxo.</param>
        /// <returns>O fluxo com o ID especificado ou null se não encontrado.</returns>
        public Flow? GetById(string id)
        {
            // Inclui as entidades relacionadas Folder e Sector
            return _context.Flows
                .Include(f => f.Folder) // Inclui a pasta associada
                .FirstOrDefault(f => f.Id == id);
        }

        /// <summary>
        /// Obtém todos os fluxos.
        /// </summary>
        /// <returns>Uma coleção de todos os fluxos.</returns>
        public IEnumerable<Flow> GetAll()
        {
            return _context.Flows.ToList();
        }

        /// <summary>
        /// Obtém todos os fluxos de um setor específico dentro de uma pasta específica.
        /// </summary>
        /// <param name="sectorId">O ID do setor.</param>
        /// <param name="folderId">O ID da pasta.</param>
        /// <returns>Uma coleção de fluxos do setor na pasta especificada.</returns>
        public IEnumerable<Flow> GetAllBySectorIdAndFolderId(int sectorId, int folderId)
        {
            if (folderId != 0)
            {
                return _context.Flows
                    .Where(f => f.SectorId == sectorId && f.FolderId == folderId)
                    .ToList();
            }
            else
            {
                return _context.Flows
                    .Where(f => f.SectorId == sectorId)
                    .ToList();
            }
        }

        /// <summary>
        /// Libera os recursos do contexto do banco de dados.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
