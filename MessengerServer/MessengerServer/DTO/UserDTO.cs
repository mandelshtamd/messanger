using System;

namespace MessengerServer.DTO
{
    public sealed class UserDTO
    {
        public String Login { get; }
        public DateTimeOffset LastActiveTime { get; }
        public Int32 ActivityStatus { get; }

        public UserDTO(String login, DateTimeOffset lastActiveTime, Int32 activityStatus)
        {
            Login = login;
            LastActiveTime = lastActiveTime;
            ActivityStatus = activityStatus;
        }
    }
}