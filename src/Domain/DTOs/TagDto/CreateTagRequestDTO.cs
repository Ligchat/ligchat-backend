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
        /// Cor da etiqueta. Este campo é opcional.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="name">Nome da etiqueta.</param>
        /// <param name="description">Descrição da etiqueta.</param>
        /// <param name="sectorId">Identificador do setor associado à etiqueta.</param>
        /// <param name="color">Cor da etiqueta.</param>
        /// <param name="status">Status da etiqueta.</param>
        [JsonConstructor]
        public CreateTagRequestDTO(
            string name,
            string description,
            int sectorId,
            string color,
            bool status)
        {
            Name = name;
            Description = description;
            SectorId = sectorId;
            Color = color;
            Status = status;
        }
    }
}
