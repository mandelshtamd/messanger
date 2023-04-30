using System;

namespace MessengerServer.DTO
{
    public sealed class ChatDTO
    {
        public Guid Id { get; }
        public String Title { get; }
        public Guid OwnerId { get; }
        public Boolean IsPersonal { get; }
        public Int32 Type { get; }

        public ChatDTO(Guid id, String title, Guid ownerId, Boolean isPersonal, Int32 type)
        {
            Id = id;
            Title = title;
            OwnerId = ownerId;
            IsPersonal = isPersonal;
            Type = type;
        }
    }
}