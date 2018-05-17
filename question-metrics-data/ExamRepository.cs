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
            _exams = new List<Exam>();
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
