using System.Collections;
using System.Web.Http.Results;
using MessengerServer.Controllers;
using MessengerServer.DAL.Repository;
using MessengerServer.DTO;
using MessengerServer.Model;
using Moq;

namespace MessengerServerTest.Controllers;

public class ChatControllerTest
{
    private Mock<IMessageInfoRepository> mockMessageRepository;
    private ChatController _chatController;

    [SetUp]
    public void Setup()
    {
        mockMessageRepository = new Mock<IMessageInfoRepository>();
        _chatController = new ChatController(mockMessageRepository.Object);
    }

    [Test]
    public void TestGetAllMessagesIfBadRequest()
    {
        Guid chatId = Guid.Empty;
        Guid userId = Guid.NewGuid();

        var result = _chatController.GetAllMessages(userId, chatId) as InvalidModelStateResult;
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestGetAllMessagesIfNoMessages()
    {
        Guid chatId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        mockMessageRepository.Setup(a => a.GetAllMessagesFromChat(userId, chatId, 50, 0))
            .Returns(null as IList<MessageInfo>);

        var result = _chatController.GetAllMessages(userId, chatId) as NotFoundResult;
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestGetAllMessagesIfFoundMessages()
    {
        Guid chatId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();

        List<MessageInfo> messages = new List<MessageInfo>();
        var messageInfo = new MessageInfo(Guid.NewGuid(), DateTimeOffset.Now, String.Empty, 0, String.Empty,
            Guid.NewGuid(), true, String.Empty, 23);
        messages.Add(messageInfo);

        mockMessageRepository.Setup(a => a.GetAllMessagesFromChat(userId, chatId, 50, 0))
            .Returns(messages);

        var result = _chatController.GetAllMessages(userId, chatId) as OkNegotiatedContentResult<IEnumerable<MessageDTO>>;
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Content.Count());
    }
}