using LigChat.Backend.Application.Common.Mappings.ContactActionResults;

namespace Application.Interface
{
    public interface IContactServiceInterface
    {
        ContactListResponse GetAll();
        SingleContactResponse? GetById(int id);
        SingleContactResponse Save(CreateContactRequestDTO contactDto);
        SingleContactResponse Update(int id, UpdateContactRequestDTO contactDto);
        SingleContactResponse Delete(int id);
        ContactListResponse GetBySector(int sectorId);
    }

    public class CreateContactRequestDTO
    {
        public string Name { get; set; }
        public int? TagId { get; set; }
        public string PhoneWhatsapp { get; set; }
        public string ProfilePicUrl { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
        public int SectorId { get; set; }
    }

    public class UpdateContactRequestDTO
    {
        public string Name { get; set; }
        public int? TagId { get; set; }
        public string PhoneWhatsapp { get; set; }
        public string ProfilePicUrl { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
        public int SectorId { get; set; }
    }
} 