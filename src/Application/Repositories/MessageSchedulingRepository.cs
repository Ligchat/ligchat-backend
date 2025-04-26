using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Web.Extensions.Database;
using LigChat.Data.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigChat.Backend.Application.Repositories
{
    public class MessageSchedulingRepository : IMessageSchedulingRepositoryInterface, IDisposable
    {
        private readonly DatabaseConfiguration _context;

        public MessageSchedulingRepository(DatabaseConfiguration context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<MessageScheduling>> GetAll(int sectorId)
        {
            return await _context.MessageSchedulings
                .Include(m => m.Attachments)
                .Where(m => m.SectorId == sectorId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<MessageScheduling?> GetById(int id)
        {
            return await _context.MessageSchedulings
                .Include(m => m.Attachments)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<MessageScheduling> Save(MessageScheduling messageScheduling)
        {
            if (messageScheduling == null)
                throw new ArgumentNullException(nameof(messageScheduling));

            await _context.MessageSchedulings.AddAsync(messageScheduling);
            await _context.SaveChangesAsync();
            return messageScheduling;
        }

        public async Task<MessageScheduling> Update(MessageScheduling messageScheduling)
        {
            if (messageScheduling == null)
                throw new ArgumentNullException(nameof(messageScheduling));

            var existingMessage = await _context.MessageSchedulings
                .Include(m => m.Attachments)
                .FirstOrDefaultAsync(m => m.Id == messageScheduling.Id);

            if (existingMessage == null)
                throw new ArgumentException($"Message with ID {messageScheduling.Id} not found.");

            existingMessage.Name = messageScheduling.Name;
            existingMessage.MessageText = messageScheduling.MessageText;
            existingMessage.SendDate = messageScheduling.SendDate;
            existingMessage.ContactId = messageScheduling.ContactId;
            existingMessage.SectorId = messageScheduling.SectorId;
            existingMessage.Status = messageScheduling.Status;
            existingMessage.TagIds = messageScheduling.TagIds;
            existingMessage.UpdatedAt = DateTime.UtcNow;

            if (messageScheduling.Attachments != null && messageScheduling.Attachments.Any())
            {
                _context.MessageAttachments.RemoveRange(existingMessage.Attachments);
                foreach (var attachment in messageScheduling.Attachments)
                {
                    attachment.MessageId = existingMessage.Id;
                    await _context.MessageAttachments.AddAsync(attachment);
                }
            }

            await _context.SaveChangesAsync();
            return existingMessage;
        }

        public async Task<MessageScheduling?> Delete(int id)
        {
            var messageScheduling = await _context.MessageSchedulings
                .Include(m => m.Attachments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (messageScheduling == null)
                return null;

            _context.MessageAttachments.RemoveRange(messageScheduling.Attachments);
            _context.MessageSchedulings.Remove(messageScheduling);
            await _context.SaveChangesAsync();

            return messageScheduling;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
} 