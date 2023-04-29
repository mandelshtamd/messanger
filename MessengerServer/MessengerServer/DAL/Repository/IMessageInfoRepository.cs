using MessengerServer.Model;

namespace MessengerServer.DAL.Repository
{
    public interface IMessageInfoRepository
    {
        IList<MessageInfo>? GetAllMessagesFromChat(Guid userId, Guid chatId, Int32 limit, Int32 offset);
        MessageInfo? CreateMessage(MessageInfo message);
        MessageInfo? GetLastMessageFromChat(Guid chatId);
        IList<MessageInfo>? GetNewMessagesFromChat(Guid userId, Guid chatId, DateTimeOffset lastRequestDate, Int32 limit, Int32 offset, Int64 usn);
    }   
}