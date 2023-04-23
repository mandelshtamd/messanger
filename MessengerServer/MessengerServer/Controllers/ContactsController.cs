
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Web.Http.Description;
using MessengerServer.DTO;

namespace MessengerServer.Controllers
{
    public sealed class ContactsController : ApiController
    {

        public ContactsController()
        {
        }

        [Microsoft.AspNetCore.Mvc.Route("api/{ownerId}/contacts")]
        [ResponseType(typeof(IEnumerable<ContactDTO>))]
        public IHttpActionResult GetContacts([FromUri] Guid ownerId)
        {
            return Ok();
        }

        [Microsoft.AspNetCore.Mvc.Route("api/contacts/add")]
        [ResponseType(typeof(String))]
        public IHttpActionResult PostCreateContact([Microsoft.AspNetCore.Mvc.FromBody] JObject data)
        {
            return Ok("Contact created");
        }
    
        [Microsoft.AspNetCore.Mvc.Route("api/contacts/delete")]
        [ResponseType(typeof(String))]
        public IHttpActionResult DeleteContact([Microsoft.AspNetCore.Mvc.FromBody] JObject data)
        {
            return Ok("Contact deleted");
        }
    }
}
