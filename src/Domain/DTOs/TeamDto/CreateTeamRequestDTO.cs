using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.TeamDto
{
    /// <summary>
    /// DTO para a criação de um novo time.
    /// </summary>
    public class CreateTeamRequestDTO
    {
        /// <summary>
        /// Nome do time. Este campo é obrigatório.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Identificador do setor associado ao time. Este campo é obrigatório.
        /// </summary>
        public int SectorId { get; set; }

        /// <summary>
        /// Status do time. Este campo é obrigatório.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Permissões associadas ao time. Este campo é obrigatório.
        /// </summary>
        public string[] Permissions { get; set; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="name">Nome do time.</param>
        /// <param name="description">Descrição do time.</param>
        /// <param name="sectorId">Identificador do setor associado ao time.</param>
        /// <param name="status">Status do time.</param>
        /// <param name="permissions">Permissões associadas ao time.</param>
        [JsonConstructor]
        public CreateTeamRequestDTO(
            string name, 
            int sectorId,
            bool status,
            string[] permissions)
        {
            Name = name;
            SectorId = sectorId;
            Status = status;
            Permissions = permissions;
        }
    }
}
