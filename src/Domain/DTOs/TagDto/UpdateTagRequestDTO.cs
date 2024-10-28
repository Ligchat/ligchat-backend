using System.Text.Json.Serialization;

namespace LigChat.Backend.Domain.DTOs.TagDto
{
    /// <summary>
    /// DTO para a atualização das informações de uma etiqueta.
    /// </summary>
    public class UpdateTagRequestDTO
    {
        /// <summary>
        /// Nome da etiqueta. Este campo é opcional.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Descrição da etiqueta. Este campo é opcional.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Identificador do setor associado à etiqueta. Este campo é opcional.
        /// </summary>
        public int? SectorId { get; set; }

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
        public UpdateTagRequestDTO(
            string? name = null,
            string? description = null,
            int? sectorId = null,
            bool status = false) // Adicionado o novo parâmetro com valor padrão
        {
            Name = name;
            Description = description;
            SectorId = sectorId;
            Status = status; // Inicializando o novo campo
        }
    }
}
