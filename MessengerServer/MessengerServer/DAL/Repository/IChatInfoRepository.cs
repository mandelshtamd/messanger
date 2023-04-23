using MessengerServer.Model;

namespace MessengerServer.DAL.Repository
{
    // Пример принципа разделения интерфейсов - логически разделили связь с сущностями в бд на несколько интерфейсов
    public interface IChatInfoRepository
    {
        IList<ChatInfo> GetChatsByParticipantId(Guid userId);
        ChatInfo GetChatById(Guid chatId);
        ChatInfo GetDialog(Guid userId, Guid participantId);
        ChatInfo CreateChat(ChatInfo chatInfo);
    }
}