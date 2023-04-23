using System;
using System.Data;
using MessengerServer.Model;

namespace MessengerServer.DAL.Mappers
{
    internal sealed class UserInfoMapper : IMapper<UserInfo>
    {
        public UserInfo ReadItem(IDataReader reader)
        {
            return new UserInfo(
                (Guid)reader["Id"],
                (String)reader["Login"],
                (String)reader["Password"],
                (DateTimeOffset)reader["LastActiveDate"],
                (Int32)(Byte)reader["ActivityStatus"],
                (String)reader["Avatar"]
            );
        }
    }
}