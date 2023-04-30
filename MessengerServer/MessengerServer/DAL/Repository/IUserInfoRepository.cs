using MessengerServer.Model;

namespace MessengerServer.DAL.Repository
{
    public interface IUserInfoRepository
    {
        UserInfo? GetUserByAuthData(String login, String password);
        Int32 UpdateUserLastActiveDate(Guid userId, DateTimeOffset lastActiveDate);
        UserInfo? GetUserData(Guid userId);
        Guid CreateUser(UserInfo user);
        Int32 DeleteUser(Guid userId);
    }
}