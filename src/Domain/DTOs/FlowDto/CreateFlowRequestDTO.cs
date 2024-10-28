using System;
using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.FlowDto
{
    /// <summary>
    /// DTO para a criação de um novo fluxo.
    /// </summary>
    public class CreateFlowRequestDTO
    {
        /// <summary>
        /// Nome do fluxo. Este campo é obrigatório.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Identificador da pasta associada ao fluxo. Este campo é obrigatório.
        /// </summary>
        public int FolderId { get; set; }

        public int SectorId { get; set; }

        /// <summary>
        /// Contador de execuções do fluxo.
        /// </summary>
        public int Executions { get; set; }

        /// <summary>
        /// Taxa de conversão do fluxo.
        /// </summary>
        public double Ctr { get; set; }

        /// <summary>
        /// Data de criação do fluxo.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Data da última atualização do fluxo.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Status do fluxo (ex: ativo, inativo). Este campo é obrigatório.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="name">Nome do fluxo.</param>
        /// <param name="folderId">Identificador da pasta associada ao fluxo.</param>
        /// <param name="status">Status do fluxo.</param>
        [JsonConstructor]
        public CreateFlowRequestDTO(
            string name,
            int folderId,
            int sectorId,
            int executions,
            double ctr,
            DateTime createdAt,
            DateTime updatedAt,
            bool status)
        {
            Name = name;
            FolderId = folderId;
            SectorId = sectorId;
            Executions = executions;
            Ctr = ctr;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Status = status; 
        }
    }
}
