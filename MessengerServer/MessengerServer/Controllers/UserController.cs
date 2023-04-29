using System.Web.Http;
using System.Web.Http.Description;
using MessengerServer.DAL.Repository;
using MessengerServer.DTO;

namespace MessengerServer.Controllers
{
    public sealed class UserController : ApiController
    {
        
        private readonly IUserInfoRepository _userInfoRepository;

        public UserController(IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }

        [Microsoft.AspNetCore.Mvc.Route("api/user/{userId}/data")]
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult GetUserData([FromUri]Guid userId)
        {
            if (userId == default(Guid))
            {
                ModelState.AddModelError($"{nameof(userId)}", "Incoming data is null");
                return BadRequest(ModelState);
            }

            var userInfo = _userInfoRepository.GetUserData(userId);
           
            if (userInfo == null)
            {
                return NotFound();
            }

            return Ok(new UserDTO(userInfo.Login, userInfo.LastActiveDate, userInfo.ActivityStatus));
        }
    }
}
