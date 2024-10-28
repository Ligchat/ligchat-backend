using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.TeamDto
{
    /// <summary>
    /// DTO para a atualização das informações de um time.
    /// </summary>
    public class UpdateTeamRequestDTO
    {
        /// <summary>
        /// Nome do time. Este campo é opcional.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Descrição do time. Este campo é opcional.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Identificador do setor associado ao time. Este campo é opcional.
        /// </summary>
        public int? SectorId { get; set; }

        /// <summary>
        /// Status do time. Este campo é opcional.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Permissões associadas ao time. Este campo é opcional.
        /// </summary>
        public string[]? Permissions { get; set; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="name">Nome do time.</param>
        /// <param name="description">Descrição do time.</param>
        /// <param name="sectorId">Identificador do setor associado ao time.</param>
        /// <param name="status">Status do time.</param>
        /// <param name="permissions">Permissões associadas ao time.</param>
        [JsonConstructor]
        public UpdateTeamRequestDTO(
               bool status,
            string? name = null, 
            string? description = null, 
            int? sectorId = null,
            string[]? permissions = null)
        {
            Name = name;
            Description = description;
            SectorId = sectorId;
            Status = status;
            Permissions = permissions;
        }
    }
}
