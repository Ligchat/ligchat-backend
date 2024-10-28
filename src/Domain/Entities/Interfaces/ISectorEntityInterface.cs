using LigChat.Backend.Data.Interfaces;

namespace LigChat.Backend.Domain.Entities.Interfaces
{
    /// <summary>
    /// Interface para definir a estrutura da entidade de setor.
    /// </summary>
    public interface ISectorEntityInterface : IBaseEntityInterface
    {
        /// <summary>
        /// Nome do setor.
        /// Representa o nome pelo qual o setor é conhecido, usado para identificá-lo e diferenciá-lo de outros setores.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Descrição do setor.
        /// Fornece detalhes adicionais sobre o setor para uma melhor compreensão e contexto.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Identificador do usuário responsável pelo setor.
        /// Referencia o ID do usuário associado ao setor, permitindo a vinculação ao responsável.
        /// </summary>
        int? UserBusinessId { get; set; }
    }
}
