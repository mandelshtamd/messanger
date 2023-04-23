using System;
using System.Web.Http;
using System.Web.Http.Description;
using MessengerServer.DTO;

namespace MessengerServer.Controllers
{
    public sealed class ChatController : ApiController
    {
       
        public ChatController()
        {
        }
        
        [Microsoft.AspNetCore.Mvc.Route("api/{userId}/chats/{chatId}/messages")]
        [ResponseType(typeof(IEnumerable<MessageDTO>))]
        public IHttpActionResult GetAllMessages([FromUri]Guid userId, [FromUri]Guid chatId, [FromUri]Int32 limit = 50, [FromUri]Int32 offset = 0)
        {
            return Ok();
        }

        [Microsoft.AspNetCore.Mvc.Route("api/{userId}/chats/{chatId}/participants")]
        [ResponseType(typeof(IEnumerable<MessageDTO>))]
        public IHttpActionResult GetChatParticipants([FromUri]Guid userId, [FromUri]Guid chatId)
        {
            return Ok();
        }
    }
}
