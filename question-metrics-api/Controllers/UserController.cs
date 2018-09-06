using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using question_metrics_api.Controllers.Dtos;
using question_metrics_api.Dtos;
using question_metrics_domain;
using question_metrics_domain.Interfaces;
using Force.DeepCloner;
using Serilog;

namespace question_metrics_api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IExamRepo _examRepo;

        public UserController(IUserRepository userRepository, IExamRepo examRepo)
        {
            _userRepository = userRepository;
            _examRepo = examRepo;
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
        [Route("id/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            User userInDatabase = await _userRepository.FindById(id);

            if (userInDatabase == null)
                return NotFound();

            return Ok(userInDatabase);
        }

        [HttpGet]
        [Route("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            User userInDatabase = await _userRepository.FindByEmail(email);

            if (userInDatabase == null)
                return NotFound();

            return Ok(userInDatabase);
        }
    }

    // public static class CloneUtils
    // {
    //     public static T CloneJson<T>(this T source)
    //     {            
    //         // Don't serialize a null object, simply return the default for that object
    //         if (Object.ReferenceEquals(source, null))
    //         {
    //             return default(T);
    //         }

    //         // initialize inner objects individually
    //         // for example in default constructor some list property initialized with some values,
    //         // but in 'source' these items are cleaned -
    //         // without ObjectCreationHandling.Replace default constructor values will be added to result
    //         var deserializeSettings = new JsonSerializerSettings {ObjectCreationHandling = ObjectCreationHandling.Replace};

    //         return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
    //     }
    // }
    
}