using LigChat.Backend.Application.Common.Mappings.ContactActionResults;
using LigChat.Backend.Application.Interface.ContactInterface;
using LigChat.Backend.Domain.DTOs.ContactDto;
using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Web.Extensions.Database;
using System.Collections.Generic;
using System.Linq;
using tests_.src.Domain.Entities;

namespace LigChat.Backend.Application.Services
{
    public class ContactService : IContactServiceInterface
    {
        private readonly IContactRepositoryInterface _contactRepository;
        private readonly DatabaseConfiguration _context;

        // Construtor que injeta as dependências necessárias
        public ContactService(IContactRepositoryInterface contactRepository, DatabaseConfiguration context)
        {
            _contactRepository = contactRepository;
            _context = context;
        }

        private void EnsureContactHasCard(Contact contact)
        {
            var existingCard = _context.Cards
                .FirstOrDefault(c => c.ContactId == contact.Id);

            if (existingCard == null)
            {
                var firstColumn = _context.Colunas
                    .Where(c => c.SectorId == contact.SectorId)
                    .OrderBy(c => c.Position)
                    .FirstOrDefault();

                if (firstColumn != null)
                {
                    var lastPosition = _context.Cards
                        .Where(c => c.ColumnId == firstColumn.Id)
                        .Max(c => (int?)c.Position) ?? 0;

                    var newCard = new Card
                    {
                        ContactId = contact.Id,
                        ColumnId = firstColumn.Id,
                        Position = lastPosition + 1,
                        SectorId = contact.SectorId,
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.Cards.Add(newCard);
                    _context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Obtém todos os contatos e retorna uma resposta com uma lista de contatos.
        /// </summary>
        public ContactListResponse GetAll()
        {
            var contacts = _contactRepository.GetAll();
            var contactDtos = contacts.Select(c => new ContactViewModel
            {
                Id = c.Id,
                Name = c.Name,
                TagId = c.TagId,
                Number = c.Number,
                AvatarUrl = c.AvatarUrl,
                Email = c.Email ?? string.Empty,
                Notes = c.Notes,
                IsActive = c.IsActive,
                Priority = c.Priority,
                ContactStatus = c.ContactStatus,
                SectorId = c.SectorId,
                AiActive = c.AiActive,
                AssignedTo = c.AssignedTo,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                IsOfficial = c.IsOfficial,
                IsViewed = c.IsViewed
            }).ToList();

            return new ContactListResponse("Success", "200", contactDtos);
        }

        /// <summary>
        /// Obtém um contato pelo ID e retorna uma resposta com os dados do contato.
        /// </summary>
        public SingleContactResponse? GetById(int id)
        {
            var contact = _contactRepository.GetById(id);
            if (contact == null)
            {
                return new SingleContactResponse("Contact not found", "404", null);
            }

            var contactDto = new ContactViewModel
            {
                Id = contact.Id,
                Name = contact.Name,
                TagId = contact.TagId,
                Number = contact.Number,
                AvatarUrl = contact.AvatarUrl,
                Email = contact.Email ?? string.Empty,
                Notes = contact.Notes,
                IsActive = contact.IsActive,
                Priority = contact.Priority,
                ContactStatus = contact.ContactStatus,
                SectorId = contact.SectorId,
                AiActive = contact.AiActive,
                AssignedTo = contact.AssignedTo,
                CreatedAt = contact.CreatedAt,
                UpdatedAt = contact.UpdatedAt,
                IsOfficial = contact.IsOfficial,
                IsViewed = contact.IsViewed
            };

            return new SingleContactResponse("Success", "200", contactDto);
        }

        /// <summary>
        /// Cria um novo contato com base nos dados fornecidos e retorna uma resposta com o contato criado.
        /// </summary>
        public SingleContactResponse Save(CreateContactRequestDTO contactDto)
        {
            if (string.IsNullOrWhiteSpace(contactDto.Name) || string.IsNullOrWhiteSpace(contactDto.Number))
            {
                return new SingleContactResponse("Invalid request", "400", null);
            }

            var contact = new Contact
            {
                Name = contactDto.Name,
                TagId = contactDto.TagId,
                Number = contactDto.Number,
                Email = contactDto.Email,
                Notes = contactDto.Notes,
                IsActive = contactDto.IsActive,
                SectorId = contactDto.SectorId,
                AvatarUrl = contactDto.AvatarUrl,
                Priority = contactDto.Priority ?? "normal",
                ContactStatus = "Novo",
                AiActive = contactDto.AiActive,
                AssignedTo = contactDto.AssignedTo,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsOfficial = contactDto.IsOfficial
            };

            var savedContact = _contactRepository.Save(contact);
            EnsureContactHasCard(savedContact);

            var responseDto = new ContactViewModel
            {
                Id = savedContact.Id,
                Name = savedContact.Name,
                TagId = savedContact.TagId,
                Number = savedContact.Number,
                AvatarUrl = savedContact.AvatarUrl,
                Email = savedContact.Email ?? string.Empty,
                Notes = savedContact.Notes,
                IsActive = savedContact.IsActive,
                Priority = savedContact.Priority,
                ContactStatus = savedContact.ContactStatus,
                SectorId = savedContact.SectorId,
                AiActive = savedContact.AiActive,
                AssignedTo = savedContact.AssignedTo,
                CreatedAt = savedContact.CreatedAt,
                UpdatedAt = savedContact.UpdatedAt,
                IsOfficial = savedContact.IsOfficial,
                IsViewed = savedContact.IsViewed
            };

            return new SingleContactResponse("Contact created successfully", "201", responseDto);
        }

        /// <summary>
        /// Atualiza um contato existente com base nos dados fornecidos e retorna uma resposta com o contato atualizado.
        /// </summary>
        public SingleContactResponse Update(int id, UpdateContactRequestDTO contactDto)
        {
            // Validação dos dados do contato
            if (string.IsNullOrWhiteSpace(contactDto.Name) || string.IsNullOrWhiteSpace(contactDto.PhoneWhatsapp))
            {
                return new SingleContactResponse("Invalid request", "400", null);
            }

            // Recupera o contato existente
            var existingContact = _contactRepository.GetById(id);
            if (existingContact == null)
            {
                return new SingleContactResponse("Contact not found", "404", null);
            }

            // Atualiza o contato com os dados do DTO
            existingContact.Name = contactDto.Name;
            existingContact.TagId = contactDto.TagId;
            existingContact.Number = contactDto.PhoneWhatsapp;
            existingContact.Email = contactDto.Email;
            existingContact.Notes = contactDto.Notes;
            existingContact.IsActive = contactDto.IsActive;
            existingContact.SectorId = contactDto.SectorId ?? existingContact.SectorId;
            existingContact.AvatarUrl = contactDto.AvatarUrl;
            existingContact.Priority = contactDto.Priority;
            existingContact.AiActive = contactDto.AiActive;
            existingContact.AssignedTo = contactDto.AssignedTo ?? existingContact.AssignedTo;
            existingContact.UpdatedAt = DateTime.UtcNow;
            existingContact.IsOfficial = contactDto.IsOfficial;

            // Salva o contato atualizado no repositório
            var savedContact = _contactRepository.Update(id, existingContact);

            // Cria e retorna a resposta
            var responseDto = new ContactViewModel
            {
                Id = savedContact.Id,
                Name = savedContact.Name,
                TagId = savedContact.TagId,
                Number = savedContact.Number,
                AvatarUrl = savedContact.AvatarUrl,
                Email = savedContact.Email ?? string.Empty,
                Notes = savedContact.Notes,
                IsActive = savedContact.IsActive,
                Priority = savedContact.Priority,
                ContactStatus = savedContact.ContactStatus,
                SectorId = savedContact.SectorId,
                AiActive = savedContact.AiActive,
                AssignedTo = savedContact.AssignedTo,
                CreatedAt = savedContact.CreatedAt,
                UpdatedAt = savedContact.UpdatedAt,
                IsOfficial = savedContact.IsOfficial,
                IsViewed = savedContact.IsViewed
            };

            return new SingleContactResponse("Contact updated successfully", "200", responseDto);
        }

        /// <summary>
        /// Deleta um contato pelo ID e retorna uma resposta com o contato deletado.
        /// </summary>
        public SingleContactResponse Delete(int id)
        {
            // Deleta o contato do repositório
            var deletedContact = _contactRepository.Delete(id);
            if (deletedContact == null)
            {
                return new SingleContactResponse("Contact not found", "404", null);
            }

            // Cria e retorna a resposta
            var responseDto = new ContactViewModel
            {
                Id = deletedContact.Id,
                Name = deletedContact.Name,
                TagId = deletedContact.TagId,
                Number = deletedContact.Number,
                AvatarUrl = deletedContact.AvatarUrl,
                Email = deletedContact.Email ?? string.Empty,
                Notes = deletedContact.Notes,
                IsActive = deletedContact.IsActive,
                Priority = deletedContact.Priority,
                ContactStatus = deletedContact.ContactStatus,
                SectorId = deletedContact.SectorId,
                AiActive = deletedContact.AiActive,
                AssignedTo = deletedContact.AssignedTo,
                CreatedAt = deletedContact.CreatedAt,
                UpdatedAt = deletedContact.UpdatedAt,
                IsOfficial = deletedContact.IsOfficial,
                IsViewed = deletedContact.IsViewed
            };

            return new SingleContactResponse("Contact deleted successfully", "200", responseDto);
        }

        public ContactListResponse GetBySector(int sectorId)
        {
            var contacts = _contactRepository.GetBySectorId(sectorId);
            if (!contacts.Any())
            {
                return new ContactListResponse($"No contacts found for sector with ID {sectorId}.", "404", new List<ContactViewModel>());
            }

            var contactDtos = contacts.Select(c => new ContactViewModel
            {
                Id = c.Id,
                Name = c.Name,
                TagId = c.TagId,
                Number = c.Number,
                AvatarUrl = c.AvatarUrl,
                Email = c.Email ?? string.Empty,
                Notes = c.Notes,
                IsActive = c.IsActive,
                Priority = c.Priority,
                ContactStatus = c.ContactStatus,
                SectorId = c.SectorId,
                AiActive = c.AiActive,
                AssignedTo = c.AssignedTo,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                IsOfficial = c.IsOfficial,
                IsViewed = c.IsViewed
            }).ToList();

            return new ContactListResponse("Success", "200", contactDtos);
        }

        public void MarkAsViewed(int contactId, int sectorId)
        {
            _contactRepository.MarkAsViewed(contactId, sectorId);
        }

        public void UpdateName(int contactId, string newName)
        {
            var contact = _contactRepository.GetById(contactId);
            if (contact != null && !string.IsNullOrWhiteSpace(newName))
            {
                contact.Name = newName;
                _contactRepository.Update(contactId, contact);
            }
        }
    }
}
