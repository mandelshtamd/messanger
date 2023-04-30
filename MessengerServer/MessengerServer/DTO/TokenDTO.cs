using System;

namespace MessengerServer.DTO
{
    public sealed class TokenDTO
    {
        public Guid Id { get; }

        public TokenDTO(Guid id)
        {
            Id = id;
        }
    }
}