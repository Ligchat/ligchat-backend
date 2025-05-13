using LigChat.Backend.Application.Interface.ContactInterface;
using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Web.Extensions.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace LigChat.Backend.Application.Repositories
{
    /// <summary>
    /// Implementação do repositório para gerenciamento de contatos.
    /// </summary>
    public class ContactRepository : IContactRepositoryInterface, IDisposable
    {
        private readonly DatabaseConfiguration _context;
        private readonly string _logFilePath = "contact_error_log.txt";

        private void LogToFile(string message)
        {
            try
            {
                File.AppendAllText(_logFilePath, $"[{DateTime.Now}] {message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados.
        /// </summary>
        /// <param name="context">O contexto do banco de dados.</param>
        public ContactRepository(DatabaseConfiguration context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); // Garante que o contexto não seja nulo

            // Iniciar arquivo de log
            LogToFile("----------------------------------------------------------------");
            LogToFile("Contact Repository Log Started");
            LogToFile("----------------------------------------------------------------");
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
                LogToFile($"Iniciando GetBySectorId com sectorId: {sectorId}");

                // First try to get the raw data without mapping to identify possible nullable issues
                LogToFile("Attempting to get raw data from database first...");
                
                // Antes da query normal, vamos corrigir quaisquer registros com Order NULL
                try
                {
                    LogToFile("Checking for contacts with NULL order values...");
                    
                    // Abordagem segura usando SQL direto para identificar contatos com Order NULL
                    var connection = _context.Database.GetDbConnection();
                    if (connection.State != System.Data.ConnectionState.Open)
                        connection.Open();
                    
                    List<int> contactsWithNullOrder = new List<int>();
                    
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT id FROM contacts WHERE sector_id = @sectorId AND `order` IS NULL";
                        var param = command.CreateParameter();
                        param.ParameterName = "@sectorId";
                        param.Value = sectorId;
                        command.Parameters.Add(param);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                contactsWithNullOrder.Add(reader.GetInt32(0));
                            }
                        }
                    }
                    
                    if (contactsWithNullOrder.Count > 0)
                    {
                        LogToFile($"Found {contactsWithNullOrder.Count} contacts with NULL order values. Fixing them...");
                        
                        // Primeiro, verificamos se há contatos com order já definido
                        bool hasExistingOrders = false;
                        int minOrder = 0;
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "SELECT COUNT(*) FROM contacts WHERE sector_id = @sectorId AND `order` IS NOT NULL";
                            var param = command.CreateParameter();
                            param.ParameterName = "@sectorId";
                            param.Value = sectorId;
                            command.Parameters.Add(param);
                            
                            var countResult = command.ExecuteScalar();
                            hasExistingOrders = Convert.ToInt32(countResult) > 0;
                            
                            if (hasExistingOrders)
                            {
                                // Se há contatos com order já definido, primeiro abrimos espaço para os novos
                                command.CommandText = "SELECT MIN(`order`) FROM contacts WHERE sector_id = @sectorId AND `order` IS NOT NULL";
                                var minResult = command.ExecuteScalar();
                                if (minResult != null && minResult != DBNull.Value)
                                {
                                    minOrder = Convert.ToInt32(minResult);
                                }
                                
                                LogToFile($"Current minimum order value: {minOrder}");
                                
                                // Desloque todos os contatos existentes para abrir espaço
                                command.CommandText = "UPDATE contacts SET `order` = `order` + @shift WHERE sector_id = @sectorId AND `order` IS NOT NULL";
                                var shiftParam = command.CreateParameter();
                                shiftParam.ParameterName = "@shift";
                                shiftParam.Value = contactsWithNullOrder.Count;
                                command.Parameters.Add(shiftParam);
                                
                                int rowsShifted = command.ExecuteNonQuery();
                                LogToFile($"Shifted {rowsShifted} existing contacts to make room for NULL order contacts");
                            }
                        }
                        
                        // Agora, atribua os valores de Order para os contatos NULL
                        int orderValue = minOrder;
                        foreach (var contactId in contactsWithNullOrder)
                        {
                            using (var command = connection.CreateCommand())
                            {
                                command.CommandText = "UPDATE contacts SET `order` = @newOrder WHERE id = @contactId";
                                
                                var orderParam = command.CreateParameter();
                                orderParam.ParameterName = "@newOrder";
                                orderParam.Value = orderValue;
                                command.Parameters.Add(orderParam);
                                
                                var idParam = command.CreateParameter();
                                idParam.ParameterName = "@contactId";
                                idParam.Value = contactId;
                                command.Parameters.Add(idParam);
                                
                                int rowsAffected = command.ExecuteNonQuery();
                                LogToFile($"Updated contact ID {contactId} with order value {orderValue} (beginning of queue). Rows affected: {rowsAffected}");
                                
                                orderValue++;
                            }
                        }
                    }
                    else
                    {
                        LogToFile("No contacts with NULL order values found.");
                    }
                }
                catch (Exception ex)
                {
                    LogToFile($"Error checking/fixing NULL order values: {ex.Message}");
                    LogToFile($"StackTrace: {ex.StackTrace}");
                }
                
                // Agora execute a query Entity Framework normalmente
                var query = _context.Contacts
                    .IgnoreAutoIncludes()
                    .Where(c => c.SectorId == sectorId)
                    .AsNoTracking();

                // Log the SQL query
                var sql = query.ToQueryString();
                LogToFile($"Generated SQL: {sql}");

                try
                {
                    LogToFile("Executing query with manual Select and mapping...");
                    // Use ToList to materialize in memory before mapping
                    var entityList = query.ToList();
                    LogToFile($"Successfully fetched {entityList.Count} entities from database");
                    
                    var result = new List<Contact>();
                    foreach (var entity in entityList)
                    {
                        try {
                            LogToFile($"Mapping contact: Id={entity.Id}, Name={entity.Name}");
                            LogToFile($"  TagId={entity.TagId?.ToString() ?? "NULL"}");
                            LogToFile($"  AiActive={entity.AiActive?.ToString() ?? "NULL"}");
                            LogToFile($"  Order={entity.Order}");
                            
                            var mappedContact = MapContact(entity);
                            result.Add(mappedContact);
                            LogToFile($"Successfully mapped contact Id={entity.Id}");
                        }
                        catch (Exception ex) {
                            LogToFile($"ERROR mapping contact Id={entity.Id}: {ex.Message}");
                            LogToFile($"Exception Type: {ex.GetType().FullName}");
                            LogToFile($"Stack Trace: {ex.StackTrace}");
                            // Continue with next contact instead of failing whole query
                        }
                    }
                    
                    LogToFile($"Query executed successfully. Number of mapped records: {result.Count}");
                    return result;
                }
                catch (Exception ex)
                {
                    LogToFile("Error when executing the query:");
                    LogToFile($"Exception type: {ex.GetType().FullName}");
                    LogToFile($"Message: {ex.Message}");
                    LogToFile($"StackTrace: {ex.StackTrace}");

                    if (ex.InnerException != null)
                    {
                        LogToFile("Inner Exception:");
                        LogToFile($"Type: {ex.InnerException.GetType().FullName}");
                        LogToFile($"Message: {ex.InnerException.Message}");
                        LogToFile($"StackTrace: {ex.InnerException.StackTrace}");
                    }

                    throw;
                }
            }
            catch (Exception ex)
            {
                LogToFile($"General error in GetBySectorId: {ex.Message}");
                LogToFile($"StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    LogToFile($"Inner Exception: {ex.InnerException.Message}");
                    LogToFile($"Inner StackTrace: {ex.InnerException.StackTrace}");
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
            try 
            {
                // Log individual field values to identify NULL issues
                LogToFile($"Processing Contact: Id={c.Id}, Name={c.Name}");
                LogToFile($"  SectorId={c.SectorId}, TagId={c.TagId?.ToString() ?? "NULL"}");
                LogToFile($"  AiActive raw value: {(c.AiActive.HasValue ? c.AiActive.ToString() : "NULL")}");
                
                return new Contact
                {
                    Id = c.Id,
                    Name = c.Name,
                    Number = c.Number,
                    AvatarUrl = c.AvatarUrl,
                    Email = c.Email,
                    Notes = c.Notes,
                    IsActive = c.IsActive,
                    Priority = c.Priority ?? "normal",
                    ContactStatus = c.ContactStatus ?? "Novo",
                    AssignedTo = c.AssignedTo,
                    TagId = c.TagId,
                    SectorId = c.SectorId,
                    AiActive = c.AiActive, // Use the nullable value directly without default
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    IsOfficial = c.IsOfficial,
                    IsViewed = c.IsViewed,
                    Order = c.Order
                };
            }
            catch (Exception ex)
            {
                LogToFile($"ERROR in MapContact for ID={c.Id}, Name={c.Name}");
                LogToFile($"Exception: {ex.GetType().FullName} - {ex.Message}");
                
                if (ex.Message.Contains("NULL") && ex.Message.Contains("Int32"))
                {
                    LogToFile($"CRITICAL: NULL to Int32 conversion error detected!");
                    LogToFile($"AiActive raw value: {(c.AiActive.HasValue ? c.AiActive.ToString() : "NULL")}");
                }
                
                throw;
            }
        }

        public void MarkAsViewed(int contactId, int sectorId)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.Id == contactId && c.SectorId == sectorId);
            if (contact != null)
            {
                contact.IsViewed = true;
                _context.SaveChanges();
            }
        }
    }
}
