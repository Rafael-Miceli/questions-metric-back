using System;
using System.Collections.Generic;
using System.Linq;
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
            _exams.Add(
                new Exam("TJ Rio Grand do Sul", 
                new DateTime(2018, 04, 29),
                new List<Question>{
                    new Question(1, new CorrectAnswer()),
                    new Question(2, new CorrectAnswer()),
                    new Question(3, new WrongAnswer(ReasonIsWrong.NotAnswered))
                })
            );
        }

        private static List<Exam> _exams = new List<Exam>();

        public async Task<Result> Delete(string examName, DateTime examDate) => Result.Fail("Não implementado");

        public async Task<IEnumerable<Exam>> GetAll() => _exams;

        public async Task<Result<Exam>> Insert(Exam exam)
        {
            _exams.Add(exam);

            return Result.Ok(exam);
        }

        public async Task<Exam> GetExamById(string examId) => _exams.FirstOrDefault(e => e.Id == examId);
    }
}
