using System.Linq;
using System.Threading.Tasks;
using Force.DeepCloner;
using Microsoft.AspNetCore.Mvc;
using question_metrics_api.ViewModels;
using question_metrics_domain;
using question_metrics_domain.Interfaces;

namespace question_metrics_api.Controllers
{
    public class TookedExamController: Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IExamRepo _examRepo;

        public TookedExamController(IUserRepository userRepository, IExamRepo examRepo)
        {
            _userRepository = userRepository;
            _examRepo = examRepo;
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

            await userInDatabase.AddExam(examInDb.DeepClone());

            await _userRepository.UpdateUser(userInDatabase);

            return NoContent();
        }

        //Cenários de teste
        //* Identificador de usuário é o e-mail */
        //?Se usuário ja existir na base com mesmo e-mail
        [HttpPut]
        [Route("{userId}/{examId}")]
        public async Task<IActionResult> UpdateAnswer(string userId, string examId, [FromBody]AnswerToUpdateViewModel answerToUpdate)
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
                await _userRepository.UpdateUser(userInDatabase);
                return NoContent();                    
            }  

            questionToUpdate.UpdateAnswer(new WrongAnswer(answerToUpdate.ReasonIsWrong.MapToReason()));            
            await _userRepository.UpdateUser(userInDatabase);
            return NoContent();
        }
        
    }
}