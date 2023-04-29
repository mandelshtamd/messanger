using System.Web.Http.Results;
using MessengerServer.Controllers;
using MessengerServer.DAL.Repository;
using MessengerServer.DTO;
using MessengerServer.Model;
using Moq;

namespace MessengerServerTest.Controllers;

public class UserControllerTest
{
    private readonly string testLogin = "Test login";
    private readonly string testPassword = "Test password";
    private Mock<IUserInfoRepository> mockUserRepository;
    private UserController _userController;

    [SetUp]
    public void Setup()
    {
        mockUserRepository = new Mock<IUserInfoRepository>();
        _userController = new UserController(mockUserRepository.Object);
    }

    [Test]
    public void TestGetUserDataIfBadRequest()
    {
        Guid id = default(Guid);

        var result = _userController.GetUserData(id) as InvalidModelStateResult;
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestGetUserDataIfNoUserFound()
    {
        Guid id = Guid.NewGuid();
        mockUserRepository.Setup(a => a.GetUserData(id))
            .Returns(null as UserInfo);

        var result = _userController.GetUserData(id) as NotFoundResult;
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestGetUserDataIsCorrect()
    {
        var id = Guid.NewGuid();
        var dateTimeOffset = DateTimeOffset.UtcNow;
        var userInfo = new UserInfo(id, testLogin, testPassword, dateTimeOffset, 0,
            null);
        mockUserRepository.Setup(a => a.GetUserData(id))
            .Returns(userInfo);

        var result = _userController.GetUserData(id) as OkNegotiatedContentResult<UserDTO>;
        Assert.AreEqual(testLogin, result.Content.Login);
        Assert.AreEqual(0, result.Content.ActivityStatus);
        Assert.AreEqual(dateTimeOffset, result.Content.LastActiveTime);
    }
}