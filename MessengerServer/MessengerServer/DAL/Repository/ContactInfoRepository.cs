using MessengerServer.Model;

namespace MessengerServer.DAL.Repository
{
    public class ContactInfoRepository : IContactInfoRepository
    {
        public ContactInfoRepository(DataStorageSettings dbSettings)
        {
        }

        public IList<ContactInfo> GetAllContactsByOwnerId(Guid ownerId)
        {
            return null;
        }

        public Int32 CreateContact(Guid ownerId, Guid userId)
        {
            return 0;
        }

        public Int32 DeleteContact(Guid ownerId, Guid userId)
        {
            return 0;
        }
    }
}
