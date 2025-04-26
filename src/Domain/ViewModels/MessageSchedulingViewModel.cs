using System;
using System.Collections.Generic;

namespace LigChat.Backend.Domain.ViewModels
{
    public class MessageSchedulingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MessageText { get; set; }
        public string SendDate { get; set; }
        public int ContactId { get; set; }
        public int SectorId { get; set; }
        public bool Status { get; set; }
        public string TagIds { get; set; }
        public ICollection<MessageAttachmentViewModel> Attachments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public MessageSchedulingViewModel()
        {
            Name = string.Empty;
            MessageText = string.Empty;
            SendDate = string.Empty;
            TagIds = string.Empty;
            Attachments = new List<MessageAttachmentViewModel>();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public MessageSchedulingViewModel(
            int id,
            string name,
            string messageText,
            string sendDate,
            int contactId,
            int sectorId,
            bool status,
            string tagIds,
            ICollection<MessageAttachmentViewModel> attachments,
            DateTime createdAt,
            DateTime updatedAt)
        {
            Id = id;
            Name = name;
            MessageText = messageText;
            SendDate = sendDate;
            ContactId = contactId;
            SectorId = sectorId;
            Status = status;
            TagIds = tagIds;
            Attachments = attachments;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
} 