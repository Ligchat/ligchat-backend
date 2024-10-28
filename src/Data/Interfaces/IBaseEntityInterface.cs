namespace LigChat.Backend.Data.Interfaces
{
    /// <summary>
    /// Interface base para entidades que requerem identificador, informações de criação e atualização, e associação a um setor.
    /// </summary>
    public interface IBaseEntityInterface
    {
        /// <summary>
        /// Identificador do setor associado.
        /// Representa o ID do setor ao qual a entidade pertence, facilitando a categorização e organização por setor.
        /// </summary>
        public static int? SectorId { get; set; }

        /// <summary>
        /// Status da entidade.
        /// Indica se a entidade está ativa ou inativa.
        /// </summary>
        public static bool Status { get; set; }

         /// <summary>
        /// Data e hora em que a entidade foi criada.
        /// Representa o momento exato em que a entidade foi registrada no sistema.
        /// </summary>
        public static DateTime CreatedAt { get; set; }

        /// <summary>
        /// Data e hora da última atualização da entidade.
        /// Indica o momento mais recente em que a entidade foi modificada.
        /// </summary>
        public static DateTime UpdatedAt { get; set; }
    }
}
