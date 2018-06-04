using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using question_metrics_api.Controllers.Dtos;
using question_metrics_api.Dtos;
using question_metrics_domain;
using question_metrics_domain.Interfaces;

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

        //Cenários de teste
        //* Identificador de usuário é o e-mail */
        //?Se usuário ja existir na base com mesmo e-mail
        [HttpPut]
        [Route("AddTookedExam/{userId}/{examId}")]
        public async Task<IActionResult> AddExamTooked(string userId, string examId)
        {
            User userInDatabase = await _userRepository.FindById(userId);

            if (userInDatabase == null)
                return NotFound("Usuário não encontrado");

            Exam examInDb = await _examRepo.GetExamById(examId);

            if (examInDb == null)
                return NotFound("Exame para adicionar ao usuário não encontrado");

            await userInDatabase.AddExam(examInDb);

            await _userRepository.UpdateUser(userInDatabase);

            return NoContent();
        }

        //Cenários de teste
        //* Identificador de usuário é o e-mail */
        //?Se usuário ja existir na base com mesmo e-mail
        [HttpPut]
        [Route("UpdateAnswer/{userId}/{examId}")]
        public async Task<IActionResult> UpdateAnswer(string userId, string examId, AnswerToUpdateDto answerToUpdate)
        {
            User userInDatabase = await _userRepository.FindById(userId);

            if (userInDatabase == null)
                return NotFound("Usuário não encontrado");

            var examToUpdateAnswer = userInDatabase.TookedExams.FirstOrDefault(e => e.Id == examId);

            if (examToUpdateAnswer == null)
                return NotFound("Usuário não fez este exame");

            var questionToUpdate = examToUpdateAnswer.Questions.FirstOrDefault(q => q.Number == answerToUpdate.QuestionNumber);

            if (questionToUpdate == null)
                return NotFound("Questão não existe neste exame");

            if (!answerToUpdate.IsWrong)
            {
                questionToUpdate.UpdateAnswer(new CorrectAnswer());
                return NoContent();                    
            }  

            questionToUpdate.UpdateAnswer(new WrongAnswer(answerToUpdate.ReasonIsWrong.MapToReason()));
            
            await _userRepository.UpdateUser(userInDatabase);

            return NoContent();
        }

        [HttpGet]
        [Route("id/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            User userInDatabase = await _userRepository.FindById(id);

            return Ok(userInDatabase);
        }

        [HttpGet]
        [Route("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            User userInDatabase = await _userRepository.FindByEmail(email);

            return Ok(userInDatabase);
        }
    }
}