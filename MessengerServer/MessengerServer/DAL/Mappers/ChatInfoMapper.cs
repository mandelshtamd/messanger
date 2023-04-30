using System;
using System.Data;
using MessengerServer.Model;

namespace MessengerServer.DAL.Mappers
{
    internal sealed class ChatInfoMapper : IMapper<ChatInfo>
    {
        public ChatInfo ReadItem(IDataReader reader)
        {
            return new ChatInfo(
                (Guid)reader["Id"],
                (String)reader["Title"],
                (Guid)reader["OwnerId"],
                (Boolean)reader["IsPersonal"],
                (Int32)(Byte)reader["Type"]
            );
        }
    }
}