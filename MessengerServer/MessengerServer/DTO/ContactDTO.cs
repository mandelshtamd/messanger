using System;

namespace MessengerServer.DTO
{
    public sealed class ContactDTO
    {
        public Guid Id { get; }
        public Guid OwnerId { get; }
        public Guid UserId { get; }
        public Boolean IsBlocked { get; }
        public UserDTO UserData { get; }

        public ContactDTO(Guid id, Guid ownerId, Guid userId, Boolean isBlocked, UserDTO userData)
        {
            Id = id;
            OwnerId = ownerId;
            UserId = userId;
            IsBlocked = isBlocked;
            UserData = userData;
        }
    }
}