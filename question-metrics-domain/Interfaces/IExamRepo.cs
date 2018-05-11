using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using question_metrics_domain;
using question_metrics_domain.Common;

namespace question_metrics_domain.Interfaces
{
    public interface IExamRepo
    {
        Task<IEnumerable<Exam>> GetAll();
        Task<Result<Exam>> Insert(Exam exam);
        Task<Result> Delete(string examName, DateTime examDate);
    }
}