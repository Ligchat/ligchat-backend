using LigChat.Backend.Domain.Entities;

namespace LigChat.Data.Interfaces.IRepositories
{
    public interface IFlowRepositoryInterface
    {
        /// <summary>
        /// Obtém todos os fluxos. Retorna uma coleção de fluxos.
        /// </summary>
        /// <returns>Uma coleção de fluxos.</returns>
        IEnumerable<Flow> GetAll();

        /// <summary>
        /// Obtém todos os fluxos de um setor específico em uma pasta específica.
        /// </summary>
        /// <param name="sectorId">O ID do setor.</param>
        /// <param name="folderId">O ID da pasta.</param>
        /// <returns>Uma coleção de fluxos que pertencem ao setor e à pasta especificados.</returns>
        IEnumerable<Flow> GetAllBySectorIdAndFolderId(int sectorId, int folderId);

        /// <summary>
        /// Obtém um fluxo pelo ID. Retorna um fluxo ou null se não encontrado.
        /// </summary>
        /// <param name="id">O ID do fluxo.</param>
        /// <returns>O fluxo encontrado ou null se não encontrado.</returns>
        Flow? GetById(string id);

        /// <summary>
        /// Salva um novo fluxo no repositório. Retorna o fluxo salvo.
        /// </summary>
        /// <param name="flow">O fluxo a ser salvo.</param>
        /// <returns>O fluxo salvo.</returns>
        Flow Save(Flow flow);

        /// <summary>
        /// Atualiza um fluxo existente pelo ID. Retorna o fluxo atualizado.
        /// </summary>
        /// <param name="id">O ID do fluxo.</param>
        /// <param name="flow">Os dados atualizados do fluxo.</param>
        /// <returns>O fluxo atualizado.</returns>
        Flow Update(string id, Flow flow);

        /// <summary>
        /// Deleta um fluxo pelo ID. Retorna o fluxo deletado.
        /// </summary>
        /// <param name="id">O ID do fluxo.</param>
        /// <returns>O fluxo deletado.</returns>
        Flow Delete(string id);
    }
}
