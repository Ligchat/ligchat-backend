using LigChat.Backend.Data.Interfaces;

namespace LigChat.Backend.Domain.Entities.Interfaces
{
    /// <summary>
    /// Interface para definir a estrutura da entidade de equipe.
    /// </summary>
    public interface ITeamEntityInterface : IBaseEntityInterface
    {
        /// <summary>
        /// Nome da equipe.
        /// Representa o nome pelo qual a equipe é conhecida.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Permissões da equipe.
        /// Lista de permissões que a equipe possui, definindo o que os membros da equipe podem ou não fazer.
        /// </summary>
        string[] Permissions { get; set; }
    }
}
