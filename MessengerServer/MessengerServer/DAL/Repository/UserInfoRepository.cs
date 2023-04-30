using MessengerServer.Model;

namespace MessengerServer.DAL.Repository
{
    public class UserInfoRepository : IUserInfoRepository
    {

        public UserInfoRepository()
        {
        }

        public UserInfo? GetUserByAuthData(String login, String password)
        {
            return null;
        }

        public Int32 UpdateUserLastActiveDate(Guid userId, DateTimeOffset lastActiveDate)
        {
            return 0;
        }

        public UserInfo? GetUserData(Guid userId)
        {
            return null;
        }
        
        public Guid CreateUser(UserInfo user)
        {
            return new Guid();
        }

        public Int32 DeleteUser(Guid userId)
        {
            return 0;
        }

    }
}
