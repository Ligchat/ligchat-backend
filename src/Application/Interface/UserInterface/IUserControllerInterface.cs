using LigChat.Backend.Domain.DTOs.UserDto;
using Microsoft.AspNetCore.Mvc;

namespace LigChat.Backend.Application.Interface.UserInterface
{
    public interface IUserControllerInterface
    {
        // Retorna todos os usuários. Deve retornar um IActionResult que pode incluir um status code e os dados dos usuários.
        IActionResult GetAll(int? invitedBy);

        // Retorna um usuário específico baseado no ID. Deve retornar um IActionResult com o status e os dados do usuário.
        IActionResult GetById(int id);

        // Cria um novo usuário com base no CreateUserRequestDTO. Deve retornar um IActionResult com o status da criação.
        IActionResult Save(CreateUserRequestDTO user);

        // Atualiza um usuário existente com base no ID e no UpdateUserRequestDTO. Deve retornar um IActionResult com o status da atualização.
        IActionResult Update(int id, UpdateUserRequestDTO user);

        // Retorna os dados do perfil do usuário atual, identificado pelo token JWT.
        IActionResult GetCurrentUserProfile();

        // Atualiza apenas o perfil básico do usuário atual (identificado pelo token JWT). Deve retornar um IActionResult com o status da atualização.
        IActionResult UpdateCurrentUserProfile(UpdateProfileRequestDTO profileDto);

        // Deleta um usuário baseado no ID. Deve retornar um IActionResult com o status da exclusão.
        IActionResult Delete(int id);
    }
}
