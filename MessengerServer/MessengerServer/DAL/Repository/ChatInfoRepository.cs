
using MessengerServer.DAL.Repository;
using MessengerServer.Model;

namespace MessengerServer.DAL.Repository
{
    // Реализуется Open-Closed принцип - зафиксировали интерфейс и эсли захотим поменять поведение, то можем завести новую реализацию интерфейса
    public class ChatInfoRepository : IChatInfoRepository
    {
        private readonly DataStorageSettings _dbSettings;

        public ChatInfoRepository(DataStorageSettings dbSettings)
        {
            _dbSettings = dbSettings ?? throw new ArgumentNullException(nameof(dbSettings));
        }


        public IList<ChatInfo> GetChatsByParticipantId(Guid userId)
        {
            return null;
        }
        
        public ChatInfo GetChatById(Guid chatId)
        {
            return null;
        }

        public ChatInfo GetDialog(Guid userId, Guid participantId)
        {
            return null;
        }

        public ChatInfo CreateChat(ChatInfo chatInfo)
        {
            return null;
        }
    }
}