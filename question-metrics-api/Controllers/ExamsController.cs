using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using question_metrics_api.Dtos;
using question_metrics_domain;
using question_metrics_domain.Interfaces;

namespace question_metrics_api.Controllers
{
    [Route("api/[controller]")]
    public class ExamsController : Controller
    {
        private readonly IExamRepo _examRepo;

        public ExamsController(IExamRepo examRepo)
        {
            _examRepo = examRepo;
        } 

        // GET api/values
        [HttpGet]
        [Route("GetAllExamsNamesAndDates")]
        public async Task<IActionResult> GetAllExamsNamesAndDates()
        {
            var allExams = await _examRepo.GetAll();

            if (!allExams.Any())
            {                
                return NotFound("Nenhum exame encontrado");        
            }  

            return Ok(allExams
                        .Select(e => new { 
                            Id = e.Id,
                            Title = $"{e.Date} - {e.Name}",
                            Name = e.Name,
                            Date = e.Date
            }));            
        }

        [HttpGet]
        [Route("GetExamByName")]
        public async Task<IActionResult> GetExamByName([FromQuery]string examName, [FromQuery]DateTime examDate)
        {
            return Ok((await _examRepo.GetAll()).FirstOrDefault(e => e.Date == examDate && e.Name == examName));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody]CreateExamDto examDto)
        {
            var questionsInExam = new List<Question>();

            for (int questionNum = 0; questionNum < examDto.TotalNumberOfQuestions; questionNum++)
                questionsInExam.Add(new Question(questionNum + 1, new WrongAnswer(ReasonIsWrong.NotSpecified)));
            
            var newExam = new Exam(examDto.Name, examDto.Date, questionsInExam);
            var result = await _examRepo.Insert(newExam);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created("", newExam.Id);            
        }

        // DELETE api/values/5
        [HttpDelete("{examName}/{examDate}")]
        public async Task<IActionResult> DeleteExam(string examName, DateTime examDate)
        {
            var result = await _examRepo.Delete(examName, examDate);
            if (result.IsSuccess)
                return NoContent();

            return BadRequest();
        }
    }
}
