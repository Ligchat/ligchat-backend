using LigChat.Backend.Application.Interface.ContactInterface;
using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Web.Extensions.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LigChat.Backend.Application.Repositories
{
    /// <summary>
    /// Implementação do repositório para gerenciamento de contatos.
    /// </summary>
    public class ContactRepository : IContactRepositoryInterface, IDisposable
    {
        private readonly DatabaseConfiguration _context;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados.
        /// </summary>
        /// <param name="context">O contexto do banco de dados.</param>
        public ContactRepository(DatabaseConfiguration context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); // Garante que o contexto não seja nulo
        }

        /// <summary>
        /// Adiciona um novo contato ao banco de dados.
        /// </summary>
        /// <param name="contact">O contato a ser adicionado.</param>
        /// <returns>O contato adicionado.</returns>
        public Contact Save(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException(nameof(contact)); // Garante que o contato não seja nulo

            _context.Contacts.Add(contact); // Adiciona o contato ao DbSet
            _context.SaveChanges(); // Salva as alterações no banco de dados
            return contact; // Retorna o contato salvo
        }

        /// <summary>
        /// Atualiza um contato existente no banco de dados.
        /// </summary>
        /// <param name="contact">O contato com as atualizações.</param>
        /// <returns>O contato atualizado.</returns>
        public Contact Update(int id, Contact contact)
        {
            if (contact == null) throw new ArgumentNullException(nameof(contact)); // Garante que o contato não seja nulo

            var existingContact = _context.Contacts.Find(id);

            if (existingContact != null)
            {
                // Atualiza as propriedades do contato existente
                existingContact.Name = contact.Name;
                existingContact.TagId = contact.TagId;
                existingContact.Number = contact.Number;
                existingContact.Email = contact.Email;
                existingContact.Notes = contact.Notes;
                existingContact.Status = contact.Status;
                existingContact.SectorId = contact.SectorId;
                existingContact.ProfilePicUrl = contact.ProfilePicUrl; // Incluído
                existingContact.UpdatedAt = DateTime.UtcNow; // Atualiza a data de atualização

                _context.SaveChanges(); // Salva as alterações no banco de dados
                return existingContact; // Retorna o contato atualizado
            }

            throw new ArgumentException("Contact not found."); // Lança uma exceção se o contato não for encontrado
        }

        /// <summary>
        /// Deleta um contato do banco de dados pelo ID.
        /// </summary>
        /// <param name="id">O ID do contato a ser deletado.</param>
        /// <returns>O contato deletado.</returns>
        public Contact Delete(int id)
        {
            var contact = _context.Contacts.Find(id);

            if (contact != null)
            {
                _context.Contacts.Remove(contact); // Remove o contato do DbSet
                _context.SaveChanges(); // Salva as alterações no banco de dados
                return contact; // Retorna o contato deletado
            }

            throw new ArgumentException("Contact not found."); // Lança uma exceção se o contato não for encontrado
        }

        /// <summary>
        /// Obtém um contato pelo ID.
        /// </summary>
        /// <param name="id">O ID do contato.</param>
        /// <returns>O contato com o ID especificado ou null se não encontrado.</returns>
        public Contact? GetById(int id)
        {
            return _context.Contacts.Find(id);
        }

        /// <summary>
        /// Obtém todos os contatos.
        /// </summary>
        /// <returns>Uma coleção de todos os contatos.</returns>
        public IEnumerable<Contact> GetAll()
        {
            return _context.Contacts.ToList();
        }

        /// <summary>
        /// Obtém todos os contatos associados a uma etiqueta específica.
        /// </summary>
        /// <param name="tagId">O ID da etiqueta.</param>
        /// <returns>Uma coleção de contatos com a etiqueta especificada.</returns>
        public IEnumerable<Contact> GetByTagId(int tagId)
        {
            return _context.Contacts.Where(c => c.TagId == tagId).ToList();
        }

        /// <summary>
        /// Obtém todos os contatos associados a um setor específico.
        /// </summary>
        /// <param name="sectorId">O ID do setor.</param>
        /// <returns>Uma coleção de contatos com o setor especificado.</returns>
        public IEnumerable<Contact> GetBySectorId(int sectorId)
        {
            return _context.Contacts.Where(c => c.SectorId == sectorId).ToList();
        }

        /// <summary>
        /// Obtém um contato pelo nome.
        /// </summary>
        /// <param name="name">O nome do contato.</param>
        /// <returns>Uma coleção de contatos com o nome especificado.</returns>
        public IEnumerable<Contact> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or whitespace.", nameof(name)); // Garante que o nome não seja nulo ou em branco
            return _context.Contacts.Where(c => c.Name.Contains(name)).ToList();
        }

        /// <summary>
        /// Obtém um contato pelo número de telefone/WhatsApp.
        /// </summary>
        /// <param name="phoneWhatsapp">O número de telefone/WhatsApp do contato.</param>
        /// <returns>O contato com o número especificado ou null se não encontrado.</returns>
        public Contact? GetByPhoneWhatsapp(string phoneWhatsapp)
        {
            if (string.IsNullOrWhiteSpace(phoneWhatsapp)) throw new ArgumentException("Phone/WhatsApp cannot be null or whitespace.", nameof(phoneWhatsapp)); // Garante que o número não seja nulo ou em branco
            return _context.Contacts.FirstOrDefault(c => c.Number == phoneWhatsapp);
        }

        /// <summary>
        /// Libera os recursos do contexto do banco de dados.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose(); // Libera os recursos do contexto do banco de dados
        }
    }
}
