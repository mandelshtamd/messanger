using System.Web.Http.Results;
using MessengerServer.Controllers;
using MessengerServer.DAL.Repository;
using MessengerServer.DTO;
using MessengerServer.Model;
using Moq;

namespace MessengerServerTest.Controllers;

public class AuthControllerTest
{
    private readonly string testLogin = "Test login";
    private readonly string testPassword = "Test password";
    private Mock<IUserInfoRepository> mockUserRepository;
    private AuthController _authController;

    [SetUp]
    public void Setup()
    {
        mockUserRepository = new Mock<IUserInfoRepository>();
        _authController = new AuthController(mockUserRepository.Object);
    }

    [Test]
    public void TestGetUserIsNotNull()
    {
        Guid id = Guid.NewGuid();
        var userInfo = new UserInfo(id, testLogin, testPassword, DateTimeOffset.UtcNow, 0,
            null);
        var credentials = new CredentialsDTO(testLogin, testPassword);
        mockUserRepository.Setup(a => a.GetUserByAuthData(testLogin, testPassword))
            .Returns(userInfo);

        var result = _authController.GetUser(credentials);
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestGetUserIsOkResult()
    {
        Guid id = Guid.NewGuid();
        var userInfo = new UserInfo(id, testLogin, testPassword, DateTimeOffset.UtcNow, 0,
            null);
        var credentials = new CredentialsDTO(testLogin, testPassword);
        mockUserRepository.Setup(a => a.GetUserByAuthData(testLogin, testPassword))
            .Returns(userInfo);

        var result = _authController.GetUser(credentials) as OkNegotiatedContentResult<TokenDTO>;
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestGetUserIsCorrect()
    {
        Guid id = Guid.NewGuid();
        var userInfo = new UserInfo(id, testLogin, testPassword, DateTimeOffset.UtcNow, 0,
            null);
        var credentials = new CredentialsDTO(testLogin, testPassword);
        mockUserRepository.Setup(a => a.GetUserByAuthData(testLogin, testPassword))
            .Returns(userInfo);

        var result = _authController.GetUser(credentials) as OkNegotiatedContentResult<TokenDTO>;
        Assert.AreEqual(id, result.Content.Id);
    }
}