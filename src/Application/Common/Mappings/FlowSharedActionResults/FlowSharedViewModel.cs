namespace LigChat.Backend.Application.Common.Mappings.FlowSharedResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados do fluxo compartilhado na API.
    /// </summary>
    public class FlowSharedViewModel
    {
        // Identificador único do fluxo compartilhado
        public int Id { get; }

        // Identificador do fluxo associado
        public int FlowId { get; }

        // Identificador do usuário associado
        public int UserId { get; }

        // Link associado ao fluxo compartilhado
        public string Link { get; }

        // Status do fluxo compartilhado
        public bool Status { get; }

        // Identificador do setor associado (opcional)
        public int? SectorId { get; }

        // Data e hora de criação do fluxo compartilhado
        public DateTime CreatedAt { get; }

        // Data e hora da última atualização do fluxo compartilhado
        public DateTime UpdatedAt { get; }

        /// <summary>
        /// Construtor padrão que inicializa as propriedades com valores padrão.
        /// </summary>
        public FlowSharedViewModel()
        {
            Id = 0;
            FlowId = 0;
            UserId = 0;
            Link = string.Empty;
            Status = true;
            SectorId = 0;
            CreatedAt = DateTime.MinValue;
            UpdatedAt = DateTime.MinValue;
        }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        public FlowSharedViewModel(int id, int flowId, int userId, string link, bool status, int? sectorId)
        {
            Id = id;
            FlowId = flowId;
            UserId = userId;
            Link = link;
            Status = status;
            SectorId = sectorId;
        }
    }
}
