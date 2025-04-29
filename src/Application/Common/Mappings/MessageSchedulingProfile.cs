using AutoMapper;
using LigChat.Backend.Domain.DTOs.MessageSchedulingDto;
using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Domain.ViewModels;

namespace LigChat.Backend.Application.Common.Mappings
{
    public class MessageSchedulingProfile : Profile
    {
        public MessageSchedulingProfile()
        {
            CreateMap<MessageScheduling, MessageSchedulingViewModel>()
                .ForMember(dest => dest.SendDate, opt => opt.MapFrom(src => src.SendDate != null ? src.SendDate.ToString() : ""));
            CreateMap<MessageAttachment, MessageAttachmentViewModel>();

            CreateMap<CreateMessageSchedulingRequestDTO, MessageScheduling>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Attachments, opt => opt.Ignore());

            CreateMap<UpdateMessageSchedulingRequestDTO, MessageScheduling>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Attachments, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
} 