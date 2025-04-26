using LigChat.Backend.Application.Interface.ContactInterface;
using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Web.Extensions.Database;
using Microsoft.EntityFrameworkCore;
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

            if (existingContact == null)
                throw new ArgumentException($"Contact with ID {id} not found.");

            // Atualiza as propriedades do contato existente
            existingContact.Name = contact.Name;
            existingContact.TagId = contact.TagId;
            existingContact.Number = contact.Number;
            existingContact.Email = contact.Email;
            existingContact.Notes = contact.Notes;
            existingContact.IsActive = contact.IsActive;
            existingContact.Priority = contact.Priority;
            existingContact.ContactStatus = contact.ContactStatus;
            existingContact.AssignedTo = contact.AssignedTo;
            existingContact.SectorId = contact.SectorId;
            existingContact.AvatarUrl = contact.AvatarUrl;
            existingContact.AiActive = contact.AiActive;
            existingContact.UpdatedAt = DateTime.UtcNow; // Atualiza a data de atualização

            _context.SaveChanges(); // Salva as alterações no banco de dados
            return existingContact; // Retorna o contato atualizado
        }

        /// <summary>
        /// Deleta um contato do banco de dados pelo ID.
        /// </summary>
        /// <param name="id">O ID do contato a ser deletado.</param>
        /// <returns>O contato deletado.</returns>
        public Contact Delete(int id)
        {
            var contact = _context.Contacts.Find(id);

            if (contact == null)
                throw new ArgumentException($"Contact with ID {id} not found.");

            _context.Contacts.Remove(contact); // Remove o contato do DbSet
            _context.SaveChanges(); // Salva as alterações no banco de dados
            return contact; // Retorna o contato deletado
        }

        /// <summary>
        /// Obtém um contato pelo ID.
        /// </summary>
        /// <param name="id">O ID do contato.</param>
        /// <returns>O contato com o ID especificado ou null se não encontrado.</returns>
        public Contact? GetById(int id)
        {
            var contact = _context.Contacts
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);

            return contact != null ? MapContact(contact) : null;
        }

        /// <summary>
        /// Obtém todos os contatos.
        /// </summary>
        /// <returns>Uma coleção de todos os contatos.</returns>
        public IEnumerable<Contact> GetAll()
        {
            return _context.Contacts
                .AsNoTracking()
                .Select(MapContact)
                .ToList();
        }

        /// <summary>
        /// Obtém todos os contatos associados a uma etiqueta específica.
        /// </summary>
        /// <param name="tagId">O ID da etiqueta.</param>
        /// <returns>Uma coleção de contatos com a etiqueta especificada.</returns>
        public IEnumerable<Contact> GetByTagId(int tagId)
        {
            return _context.Contacts
                .Where(c => c.TagId == tagId)
                .AsNoTracking()
                .Select(MapContact)
                .ToList();
        }

        /// <summary>
        /// Obtém todos os contatos associados a um setor específico.
        /// </summary>
        /// <param name="sectorId">O ID do setor.</param>
        /// <returns>Uma coleção de contatos com o setor especificado.</returns>
        public IEnumerable<Contact> GetBySectorId(int sectorId)
        {
            try
            {
                Console.WriteLine($"Iniciando GetBySectorId com sectorId: {sectorId}");

                var query = _context.Contacts
                    .IgnoreAutoIncludes()
                    .Where(c => c.SectorId == sectorId)
                    .AsNoTracking();

                // Log da query SQL
                var sql = query.ToQueryString();
                Console.WriteLine($"Query SQL gerada: {sql}");

                // Log dos parâmetros da query
                Console.WriteLine("Parâmetros da query:");
                foreach (var parameter in _context.Database.GetDbConnection().ConnectionString.Split(';'))
                {
                    Console.WriteLine(parameter);
                }

                try
                {
                    var result = query.Select(MapContact).ToList();
                    Console.WriteLine($"Query executada com sucesso. Número de registros: {result.Count}");
                    
                    // Log detalhado dos resultados
                    foreach (var contact in result)
                    {
                        Console.WriteLine($"Contact: Id={contact.Id}, Name={contact.Name}, SectorId={contact.SectorId}, TagId={contact.TagId}, AiActive={contact.AiActive}");
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao executar a query:");
                    Console.WriteLine($"Tipo de exceção: {ex.GetType().FullName}");
                    Console.WriteLine($"Mensagem: {ex.Message}");
                    Console.WriteLine($"StackTrace: {ex.StackTrace}");

                    if (ex.InnerException != null)
                    {
                        Console.WriteLine("Inner Exception:");
                        Console.WriteLine($"Tipo: {ex.InnerException.GetType().FullName}");
                        Console.WriteLine($"Mensagem: {ex.InnerException.Message}");
                        Console.WriteLine($"StackTrace: {ex.InnerException.StackTrace}");
                    }

                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral em GetBySectorId: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner StackTrace: {ex.InnerException.StackTrace}");
                }
                throw;
            }
        }

        /// <summary>
        /// Obtém um contato pelo nome.
        /// </summary>
        /// <param name="name">O nome do contato.</param>
        /// <returns>Uma coleção de contatos com o nome especificado.</returns>
        public IEnumerable<Contact> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or whitespace.", nameof(name)); // Garante que o nome não seja nulo ou em branco
            return _context.Contacts
                .Where(c => c.Name.Contains(name))
                .AsNoTracking()
                .Select(MapContact)
                .ToList();
        }

        /// <summary>
        /// Obtém um contato pelo número de telefone/WhatsApp.
        /// </summary>
        /// <param name="phoneWhatsapp">O número de telefone/WhatsApp do contato.</param>
        /// <returns>O contato com o número especificado ou null se não encontrado.</returns>
        public Contact? GetByPhoneWhatsapp(string phoneWhatsapp)
        {
            if (string.IsNullOrWhiteSpace(phoneWhatsapp)) throw new ArgumentException("Phone/WhatsApp cannot be null or whitespace.", nameof(phoneWhatsapp)); // Garante que o número não seja nulo ou em branco
            return _context.Contacts
                .Where(c => c.Number == phoneWhatsapp)
                .AsNoTracking()
                .Select(MapContact)
                .FirstOrDefault();
        }

        /// <summary>
        /// Libera os recursos do contexto do banco de dados.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose(); // Libera os recursos do contexto do banco de dados
        }

        private Contact MapContact(Contact c)
        {
            return new Contact
            {
                Id = c.Id,
                Name = c.Name,
                Number = c.Number,
                AvatarUrl = c.AvatarUrl,
                Email = c.Email,
                Notes = c.Notes,
                IsActive = c.IsActive,
                Priority = c.Priority,
                ContactStatus = c.ContactStatus,
                AssignedTo = c.AssignedTo,
                TagId = c.TagId,
                SectorId = c.SectorId,
                AiActive = c.AiActive,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                IsOfficial = c.IsOfficial
            };
        }
    }
}
