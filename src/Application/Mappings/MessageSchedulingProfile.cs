using AutoMapper;
using LigChat.Backend.Domain.DTOs.MessageSchedulingDto;
using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Domain.ViewModels;

namespace LigChat.Backend.Application.Mappings
{
    public class MessageSchedulingProfile : Profile
    {
        public MessageSchedulingProfile()
        {
            CreateMap<MessageScheduling, MessageSchedulingViewModel>();
            CreateMap<MessageAttachment, MessageAttachmentViewModel>();
            CreateMap<CreateMessageSchedulingRequestDTO, MessageScheduling>();
            CreateMap<UpdateMessageSchedulingRequestDTO, MessageScheduling>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
} 