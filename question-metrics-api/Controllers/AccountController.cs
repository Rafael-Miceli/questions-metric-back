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
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IExamRepo _examRepo;

        public AccountController(IUserRepository userRepository, IExamRepo examRepo)
        {
            _userRepository = userRepository;
            _examRepo = examRepo;
        }
        //Cenários de teste
        //* Identificador de usuário é o e-mail */
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            User userInDatabase = await _userRepository.FindByLoginAndPassword(loginDto.Login, loginDto.Password);

            if (userInDatabase != null)
                return Ok(userInDatabase);

            return Unauthorized();
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