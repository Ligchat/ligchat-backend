using LigChat.Backend.Domain.Entities;
using System.Collections.Generic;
using tests_.src.Domain.Entities;

namespace LigChat.Backend.Application.Interface.UserInterface
{
    public interface IUserRepositoryInterface
    {
        // Obtém um usuário pelo e-mail.
        // Retorna um objeto User ou null se não encontrado.
        User? GetByEmail(string email);

        // Obtém um usuário pelo ID.
        // Retorna um objeto User ou null se não encontrado.
        User? GetById(int id);

        // Obtém todos os usuários cadastrados.
        // Retorna uma coleção de objetos User.
        IEnumerable<User> GetAll();

        // Salva um novo usuário no repositório.
        // Retorna o objeto User que foi salvo.
        User Add(User user);

        // Atualiza um usuário existente no repositório.
        // Retorna o objeto User atualizado.
        User Update(User user);

        // Deleta um usuário do repositório pelo ID.
        // Retorna o objeto User que foi deletado.
        User Delete(int id);

        // Adiciona uma associação de permissão ao usuário.
        void AddUserPermission(UserPermission userPermission);

        // Remove todas as associações de permissões de um usuário.
        void RemoveUserPermissions(int userId);
    }
}
