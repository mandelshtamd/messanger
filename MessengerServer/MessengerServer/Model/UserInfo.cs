using System;

namespace MessengerServer.Model
{
    public sealed class UserInfo
    {
        public Guid Id { get; }
        public String Login { get; }
        public String Password { get; }
        public DateTimeOffset LastActiveDate { get; }
        public Int32 ActivityStatus { get; }
        public String Avatar { get; }

        public UserInfo(Guid id, String login, String password, DateTimeOffset lastActiveDate, Int32 activityStatus, String avatar)
        {
            Id = id;
            Login = login;
            Password = password;
            LastActiveDate = lastActiveDate;
            ActivityStatus = activityStatus;
            Avatar = avatar;
        }
    }
}