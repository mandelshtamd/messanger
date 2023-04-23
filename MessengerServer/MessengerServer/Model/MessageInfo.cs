using System;

namespace MessengerServer.Model
{
    public sealed class MessageInfo
    {
        public Guid Id { get; }
        public Int64 Usn { get; }        
        public DateTimeOffset DispatchDate { get; }
        public String MessageText { get; }
        public Int32 Type { get; }
        public String ContentUri { get; }
        public Guid FromUserId { get; }
        public Boolean IsRead { get; }
        public String Login { get; }

        public MessageInfo(Guid id, DateTimeOffset dispatchDate, String messageText, Int32 type, String contentUri, Guid fromUserId, Boolean isRead, String login, Int64 usn)
        {
            Id = id;
            DispatchDate = dispatchDate;
            MessageText = messageText;
            Type = type;
            ContentUri = contentUri;
            FromUserId = fromUserId;
            IsRead = isRead;
            Login = login;
            Usn = usn;
        }
    }
}