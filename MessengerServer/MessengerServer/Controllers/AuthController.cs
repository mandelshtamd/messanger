using System.Web.Http;
using System.Web.Http.Description;
using MessengerServer.DAL.Repository;
using MessengerServer.DTO;

namespace MessengerServer.Controllers
{
	// Пример единой ответственности - в зоне ответственности контроллера только 1 обязанность - авторизация
    public sealed class AuthController : ApiController
    {
	    // Пример Dependency Inversion - создаем связь через интерфейс
	    // Также тут есть пример Liskov Substitution - UserInfoRepository не нарушает принцип подстановки
        private readonly IUserInfoRepository _userRepository;

        public AuthController(IUserInfoRepository userRepository)
        {
	        _userRepository = userRepository;
        }

        [Route("api/auth/login")]
        [ResponseType(typeof(TokenDTO))]
        public IHttpActionResult GetUser([FromUri]CredentialsDTO credentials)
        {
	        var userInfo = _userRepository.GetUserByAuthData(credentials.Login, credentials.Password);
            var userDto = new TokenDTO(
				userInfo.Id
			);
			return Ok(userDto);
		}
    }
}