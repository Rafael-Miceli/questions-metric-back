using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using question_metrics_api.Controllers.Dtos;
using question_metrics_domain;
using question_metrics_domain.Interfaces;

namespace question_metrics_api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //Cenários de teste
        //* Identificador de usuário é o e-mail */
        //?Se usuário ja existir na base com mesmo e-mail
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
        {
            var newUser = new User(
                userDto.Name,
                userDto.Password,
                userDto.Email,
                userDto.Birth
            );

            User userInDatabase = await _userRepository.FindByEmail(newUser.Email);

            if (userInDatabase != null)
                return StatusCode(409, "Usuário já cadastrado");

            await _userRepository.Insert(newUser);

            return Created("", newUser.Id);
        }

        [HttpGet]
        [Route("{id: string}")]
        public async Task<IActionResult> Get(string id)
        {
            User userInDatabase = await _userRepository.FindById(id);

            return Ok(userInDatabase);
        }
    }
}