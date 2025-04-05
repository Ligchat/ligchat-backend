using LigChat.Backend.Application.Interface.ContactInterface;
using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Web.Extensions.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LigChat.Backend.Infrastructure.Repository
{
    public class ContactRepository : IContactRepositoryInterface
    {
        private readonly DatabaseConfiguration _context;

        public ContactRepository(DatabaseConfiguration context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Contact Save(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException(nameof(contact));

            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return contact;
        }

        public Contact Update(int id, Contact contact)
        {
            if (contact == null) throw new ArgumentNullException(nameof(contact));

            var existingContact = _context.Contacts.Find(id);
            if (existingContact == null)
                throw new ArgumentException($"Contact with ID {id} not found.");

            existingContact.Name = contact.Name;
            existingContact.Number = contact.Number;
            existingContact.AvatarUrl = contact.AvatarUrl;
            existingContact.Email = contact.Email;
            existingContact.Notes = contact.Notes;
            existingContact.Status = contact.Status;
            existingContact.TagId = contact.TagId;
            existingContact.SectorId = contact.SectorId;
            existingContact.AiActive = contact.AiActive;
            existingContact.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
            return existingContact;
        }

        public Contact Delete(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null)
                throw new ArgumentException($"Contact with ID {id} not found.");

            _context.Contacts.Remove(contact);
            _context.SaveChanges();
            return contact;
        }

        public Contact? GetById(int id)
        {
            return _context.Contacts.Find(id);
        }

        public IEnumerable<Contact> GetAll()
        {
            return _context.Contacts
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Contact> GetByTagId(int tagId)
        {
            return _context.Contacts
                .Where(c => c.TagId == tagId)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Contact> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));

            return _context.Contacts
                .Where(c => c.Name.Contains(name))
                .AsNoTracking()
                .ToList();
        }

        public Contact? GetByPhoneWhatsapp(string phoneWhatsapp)
        {
            if (string.IsNullOrWhiteSpace(phoneWhatsapp))
                throw new ArgumentException("Phone/WhatsApp cannot be null or whitespace.", nameof(phoneWhatsapp));

            return _context.Contacts
                .FirstOrDefault(c => c.Number == phoneWhatsapp);
        }

        public IEnumerable<Contact> GetBySectorId(int sectorId)
        {
            return _context.Contacts
                .Where(c => c.SectorId == sectorId)
                .AsNoTracking()
                .ToList();
        }
    }
} 