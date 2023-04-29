using System.Web.Http;
using System.Web.Http.Description;
using MessengerServer.DAL.Repository;
using MessengerServer.DTO;

namespace MessengerServer.Controllers
{
    public sealed class ChatController : ApiController
    {
        private readonly IMessageInfoRepository _messageInfoRepository;
       
        public ChatController(IMessageInfoRepository messageInfoRepository)
        {
            _messageInfoRepository = messageInfoRepository;
        }
        
        [Microsoft.AspNetCore.Mvc.Route("api/{userId}/chats/{chatId}/messages")]
        [ResponseType(typeof(IEnumerable<MessageDTO>))]
        public IHttpActionResult GetAllMessages([FromUri]Guid userId, [FromUri]Guid chatId, [FromUri]Int32 limit = 50, [FromUri]Int32 offset = 0)
        {
            if (chatId == Guid.Empty)
            {
                ModelState.AddModelError($"{nameof(chatId)}", "Incoming data is null");
                return BadRequest(ModelState);
            }

            var messageInfos = _messageInfoRepository.GetAllMessagesFromChat(userId, chatId, limit, offset);

            if (messageInfos == null)
            {
                return NotFound();
            }

            return Ok(messageInfos.Select(x => new MessageDTO(x.Id, x.DispatchDate, x.MessageText, x.Type, x.ContentUri, x.FromUserId == userId, x.IsRead, x.Login, x.Usn)));
        }

        [Microsoft.AspNetCore.Mvc.Route("api/{userId}/chats/{chatId}/participants")]
        [ResponseType(typeof(IEnumerable<MessageDTO>))]
        public IHttpActionResult GetChatParticipants([FromUri]Guid userId, [FromUri]Guid chatId)
        {
            return Ok();
        }
    }
}
