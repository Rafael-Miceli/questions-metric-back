﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using question_metrics_domain;
using question_metrics_domain.Interfaces;

namespace question_metrics_api.Controllers
{
    [Route("api/[controller]")]
    public class ExamsController : Controller
    {
        private readonly IExamRepo _examRepo;

        public ExamsController()
        {
            _examRepo = null;
        } 

        // GET api/values
        [HttpGet]
        [Route("GetAllExamsNamesAndDates")]
        public async Task<IActionResult> GetAllExamsNamesAndDates()
        {
            return Ok((await _examRepo.GetAll())
                .Select(e => new { 
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
        public async Task<IActionResult> CreateExam([FromBody]Exam exam)
        {
            var result = await _examRepo.Insert(exam);
            if (result.IsSuccess)
                return Created("", result.Value);

            return BadRequest();
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
