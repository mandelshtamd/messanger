using MessengerServer.Model;

namespace MessengerServer.DAL.Repository
{
    public interface IContactInfoRepository
    {
        IList<ContactInfo> GetAllContactsByOwnerId(Guid ownerId);
        Int32 CreateContact(Guid ownerId, Guid userId);
        Int32 DeleteContact(Guid ownerId, Guid userId);
    }
}