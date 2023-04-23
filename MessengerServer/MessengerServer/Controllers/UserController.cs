using System.Web.Http;
using System.Web.Http.Description;
using MessengerServer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MessengerServer.Controllers
{
    public sealed class UserController : ApiController
    {

        public UserController()
        {
        }

        [Microsoft.AspNetCore.Mvc.Route("api/user/{userId}/data")]
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult GetUserData([FromUri]Guid userId)
        {
            return Ok();
        }
    }
}
