using System;

namespace MessengerServer.Model
{
    public sealed class ContactInfo
    {
        public Guid Id { get; }
        public Guid OwnerId { get; }
        public Guid UserId { get; }
        public Boolean IsBlocked { get; }

        public ContactInfo(Guid id, Guid ownerId, Guid userId, Boolean isBlocked)
        {
            Id = id;
            OwnerId = ownerId;
            UserId = userId;
            IsBlocked = isBlocked;
        }
    }
}