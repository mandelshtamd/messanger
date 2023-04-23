using System.Data;
using MessengerServer.Model;

namespace MessengerServer.DAL.Mappers
{
    internal sealed class ContactInfoMapper : IMapper<ContactInfo>
    {
        public ContactInfo ReadItem(IDataReader reader)
        {
            return new ContactInfo(
                (Guid)reader["Id"],
                (Guid)reader["OwnerId"],
                (Guid)reader["UserId"],
                (Boolean)reader["IsBlocked"]
            );
        }
    }
}