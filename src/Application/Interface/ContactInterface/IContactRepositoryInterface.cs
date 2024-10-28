using LigChat.Backend.Domain.Entities;

namespace LigChat.Backend.Application.Interface.ContactInterface
{
    public interface IContactRepositoryInterface
    {
        // Retorna todos os contatos.
        IEnumerable<Contact> GetAll();

        // Retorna o contato correspondente ao ID ou null se não encontrado.
        Contact? GetById(int id);

        // Retorna todos os contatos associados a uma etiqueta específica.
        IEnumerable<Contact> GetByTagId(int tagId);

        // Retorna todos os contatos cujo nome corresponde ao parâmetro.
        IEnumerable<Contact> GetByName(string name);

        // Retorna o contato correspondente ao número de telefone/WhatsApp ou null se não encontrado.
        Contact? GetByPhoneWhatsapp(string phoneWhatsapp);

        // Retorna todos os contatos associados a um setor específico.
        IEnumerable<Contact> GetBySectorId(int sectorId);

        // Salva um novo contato e retorna o contato salvo.
        Contact Save(Contact contact);

        // Atualiza um contato existente e retorna o contato atualizado.
        Contact Update(int id, Contact contact);

        // Deleta o contato correspondente ao ID e retorna o contato deletado.
        Contact Delete(int id);
    }
}
