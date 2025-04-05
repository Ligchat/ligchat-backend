using LigChat.Backend.Application.Common.Mappings.ContactActionResults;
using LigChat.Backend.Domain.DTOs.ContactDto;

namespace LigChat.Backend.Application.Interface.ContactInterface
{
    public interface IContactServiceInterface
    {
        // Retorna uma coleção de contatos encapsulada em um DTO com mensagem e código.
        // O DTO ContactListResponse contém a lista de contatos e informações adicionais, como mensagens de status.
        ContactListResponse GetAll();

        // Retorna uma coleção de contatos encapsulada em um DTO com mensagem e código.
        // O DTO ContactListResponse contém a lista de contatos e informações adicionais, como mensagens de status.
        ContactListResponse GetBySector(int sectorId);

        // Retorna um único contato com base no ID.
        // O retorno é encapsulado em um DTO SingleContactResponse com mensagem e código de status.
        // Se o contato não for encontrado, pode retornar nulo.
        SingleContactResponse? GetById(int id);

        // Cria um novo contato com base nos dados fornecidos no DTO CreateContactRequestDTO.
        // Retorna um DTO SingleContactResponse com mensagem e código de status, indicando sucesso ou falha.
        SingleContactResponse Save(CreateContactRequestDTO contactDto);

        // Atualiza um contato existente com base no ID e nos dados fornecidos no DTO UpdateContactRequestDTO.
        // Retorna um DTO SingleContactResponse com mensagem e código de status, indicando sucesso ou falha.
        SingleContactResponse Update(int id, UpdateContactRequestDTO contactDto);

        // Deleta um contato com base no ID.
        // Retorna um DTO SingleContactResponse com mensagem e código de status, indicando sucesso ou falha.
        // Se o contato não for encontrado, pode retornar nulo.
        SingleContactResponse Delete(int id);
    }
}
