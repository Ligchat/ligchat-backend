using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.TagDto
{
    /// <summary>
    /// DTO para a criação de uma nova etiqueta.
    /// </summary>
    public class CreateTagRequestDTO
    {
        /// <summary>
        /// Nome da etiqueta. Este campo é obrigatório.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descrição da etiqueta. Este campo é obrigatório.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Identificador do setor associado à etiqueta. Este campo é obrigatório.
        /// </summary>
        public int SectorId { get; set; }

        /// <summary>
        /// Status da etiqueta. Este campo é opcional.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="name">Nome da etiqueta.</param>
        /// <param name="description">Descrição da etiqueta.</param>
        /// <param name="sectorId">Identificador do setor associado à etiqueta.</param>
        /// <param name="status">Status da etiqueta.</param>
        [JsonConstructor]
        public CreateTagRequestDTO(
            string name,
            string description,
            int sectorId,
            bool status) // Adicionado o novo parâmetro com valor padrão
        {
            Name = name;
            Description = description;
            SectorId = sectorId; // Inicializa o ID do setor
            Status = status; // Inicializa o novo campo
        }
    }
}
