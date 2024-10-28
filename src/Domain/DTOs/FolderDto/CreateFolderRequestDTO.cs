using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.FolderDto
{
    /// <summary>
    /// DTO para a criação de uma nova pasta.
    /// </summary>
    public class CreateFolderRequestDTO
    {
        /// <summary>
        /// Nome da pasta. Este campo é obrigatório.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Identificador do setor associado à pasta. Este campo é obrigatório.
        /// </summary>
        public int SectorId { get; set; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="name">Nome da pasta.</param>
        /// <param name="sectorId">Identificador do setor.</param>
        [JsonConstructor]
        public CreateFolderRequestDTO(
            string name,
            int sectorId)
        {
            Name = name;
            SectorId = sectorId; // Inicializa o ID do setor
        }
    }
}
