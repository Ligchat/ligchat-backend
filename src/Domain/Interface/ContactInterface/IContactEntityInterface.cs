namespace LigChat.Backend.Domain.Interface.ContactInterface
{
    public interface IContactEntityInterface
    {
        ulong Id { get; set; }
        string Name { get; set; }
        string Number { get; set; }
        int TagId { get; set; }
    }
} 