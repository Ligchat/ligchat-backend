using System.Text.Json.Serialization;
using LigChat.Backend.Domain.Entities;

namespace LigChat.Backend.Domain.DTOs.FlowDto
{
    /// <summary>
    /// DTO para a atualização das informações de um fluxo.
    /// </summary>
    public class UpdateFlowRequestDTO
    {
        /// <summary>
        /// Nome do fluxo. Este campo é opcional.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Identificador da pasta associada ao fluxo. Este campo é opcional.
        /// </summary> 
        public int? UserId { get; set; }

        public int FolderId { get; set; }

        /// <summary>
        /// Contador de execuções do fluxo. Este campo é opcional.
        /// </summary>
        public int? Executions { get; set; }

        /// <summary>
        /// Taxa de conversão do fluxo. Este campo é opcional.
        /// </summary>
        public double? Ctr { get; set; }

        /// <summary>
        /// Data de criação do fluxo. Este campo é opcional.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Data da última atualização do fluxo. Este campo é opcional.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Status do fluxo. Este campo é opcional.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="name">Nome do fluxo.</param>
        /// <param name="folderId">Identificador da pasta associada ao fluxo.</param>
        /// <param name="userId">Identificador do usuário associado ao fluxo.</param>
        /// <param name="status">Status do fluxo.</param>
        [JsonConstructor]
        public UpdateFlowRequestDTO(
            int folderId,
            bool status,
            string? name = null,
            int? userId = null,
            int? executions = null,
            double? ctr = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null
        )
        {
            Name = name;
            UserId = userId;
            FolderId = folderId;
            Executions = executions;
            Ctr = ctr;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Status = status;
        }
    }
}
