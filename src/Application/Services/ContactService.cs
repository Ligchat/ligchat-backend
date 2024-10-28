using LigChat.Backend.Application.Common.Mappings.ContactActionResults;
using LigChat.Backend.Application.Interface.ContactInterface;
using LigChat.Backend.Domain.DTOs.ContactDto;
using LigChat.Backend.Domain.Entities;
using System.Linq;

namespace LigChat.Com.Api.Mvc.ContactMvc.Service
{
    public class ContactService : IContactServiceInterface
    {
        private readonly IContactRepositoryInterface _contactRepository;

        // Construtor que injeta as dependências necessárias
        public ContactService(IContactRepositoryInterface contactRepository)
        {
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// Obtém todos os contatos e retorna uma resposta com uma lista de contatos.
        /// </summary>
        public ContactListResponse GetAll()
        {
            var contacts = _contactRepository.GetAll();
            var contactDtos = contacts.Select(c => new ContactViewModel(
                c.Id,
                c.Name,
                c.TagId,
                c.Number,
                c.ProfilePicUrl, // Incluído
                c.Email,
                c.Notes,
                c.Status,
                c.SectorId
            )).ToList(); // Convert to List for better performance in case of large datasets

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

            var contactDto = new ContactViewModel(
                contact.Id,
                contact.Name,
                contact.TagId,
                contact.Number,
                contact.ProfilePicUrl, // Incluído
                contact.Email,
                contact.Notes,
                contact.Status,
                contact.SectorId
            );

            return new SingleContactResponse("Success", "200", contactDto);
        }

        /// <summary>
        /// Cria um novo contato com base nos dados fornecidos e retorna uma resposta com o contato criado.
        /// </summary>
        public SingleContactResponse Save(CreateContactRequestDTO contactDto)
        {
            // Validação dos dados do contato
            if (string.IsNullOrWhiteSpace(contactDto.Name) || string.IsNullOrWhiteSpace(contactDto.Number))
            {
                return new SingleContactResponse("Invalid request", "400", null);
            }

            // Criação do novo contato a partir do DTO
            var contact = new Contact
            {
                Name = contactDto.Name,
                TagId = contactDto.TagId,
                Number = contactDto.Number,
                Email = contactDto.Email,
                Notes = contactDto.Notes,
                Status = contactDto.Status,
                SectorId = contactDto.SectorId,
                ProfilePicUrl = contactDto.ProfilePicUrl // Incluído
            };

            // Salva o contato no repositório
            var savedContact = _contactRepository.Save(contact);

            // Cria e retorna a resposta
            var responseDto = new ContactViewModel(
                savedContact.Id,
                savedContact.Name,
                savedContact.TagId,
                savedContact.Number,
                savedContact.ProfilePicUrl, // Incluído
                savedContact.Email,
                savedContact.Notes,
                savedContact.Status,
                savedContact.SectorId
            );

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
            existingContact.Status = contactDto.Status;
            existingContact.SectorId = contactDto.SectorId;
            existingContact.ProfilePicUrl = contactDto.ProfilePicUrl; // Incluído

            // Salva o contato atualizado no repositório
            var savedContact = _contactRepository.Update(id, existingContact);

            // Cria e retorna a resposta
            var responseDto = new ContactViewModel(
                savedContact.Id,
                savedContact.Name,
                savedContact.TagId,
                savedContact.Number,
                savedContact.ProfilePicUrl, // Incluído
                savedContact.Email,
                savedContact.Notes,
                savedContact.Status,
                savedContact.SectorId
            );

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
            var responseDto = new ContactViewModel(
                deletedContact.Id,
                deletedContact.Name,
                deletedContact.TagId,
                deletedContact.Number,
                deletedContact.ProfilePicUrl, // Incluído
                deletedContact.Email,
                deletedContact.Notes,
                deletedContact.Status,
                deletedContact.SectorId
            );

            return new SingleContactResponse("Contact deleted successfully", "200", responseDto);
        }
    }
}
