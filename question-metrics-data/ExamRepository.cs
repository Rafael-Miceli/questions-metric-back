using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using question_metrics_domain;
using question_metrics_domain.Common;
using question_metrics_domain.Interfaces;

namespace question_metrics_data
{
    public class ExamRepository : IExamRepo
    {
        public ExamRepository()
        {
            _exams = new List<Exam>(){
                new Exam("TJ Rio Grand do Sul", 
                new DateTime(2018, 04, 29),
                new List<Question>{
                    new Question(1, new CorrectAnswer()),
                    new Question(2, new CorrectAnswer()),
                    new Question(3, new WrongAnswer("Motivo 1"))
                })
            };
        }

        private static List<Exam> _exams;

        public async Task<Result> Delete(string examName, DateTime examDate)
        {
            return Result.Fail("Não implementado");
        }

        public async Task<IEnumerable<Exam>> GetAll()
        {
            return _exams;
        }

        public async Task<Result<Exam>> Insert(Exam exam)
        {
            _exams.Add(exam);

            return Result.Ok(exam);
        }
    }
}
