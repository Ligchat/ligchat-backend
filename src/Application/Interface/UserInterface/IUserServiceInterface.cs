using LigChat.Backend.Application.Common.Mappings.UserActionResults;
using LigChat.Backend.Domain.DTOs.UserDto;

namespace LigChat.Backend.Application.Interface.UserInterface
{
    public interface IUserServiceInterface
    {
        // Retorna uma coleção de usuários encapsulada em um DTO com mensagem e código.
        UserListResponse GetAll();

        // Retorna um único usuário com base no ID.
        SingleUserResponse? GetById(int id);

        // Retorna um único usuário com base no e-mail.
        SingleUserResponse? GetByEmail(string email);

        // Cria um novo usuário com base nos dados fornecidos no DTO CreateUserRequestDTO.
        SingleUserResponse? Save(CreateUserRequestDTO userDto);

        // Atualiza um usuário existente com base no ID e nos dados fornecidos no DTO UpdateUserRequestDTO.
        SingleUserResponse? Update(int id, UpdateUserRequestDTO userDto);

        // Deleta um usuário com base no ID.
        SingleUserResponse? Delete(int id);

        // Salva um código de verificação associado ao usuário.
        Task SaveVerificationCode(int userId, string verificationCode);

        // Valida o código de verificação do usuário.
        Task<bool> ValidateVerificationCode(string userId, string code);
    }
}
