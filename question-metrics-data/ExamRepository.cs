using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using MongoDB.Driver;

using question_metrics_domain;
using question_metrics_domain.Common;
using question_metrics_domain.Interfaces;

namespace question_metrics_data
{
    public class ExamRepository : IExamRepo {

        private IMongoDatabase _mongoDb;
        private IMongoCollection<ExamDataDto> _exams { get; set; }
        private readonly string _connectionString = string.Empty;

        public ExamRepository(MongoClient client) {
            _mongoDb = client.GetDatabase("QuestionMetrics");
            _exams = _mongoDb.GetCollection<ExamDataDto>("exams");
        }
        public async Task<Result> Delete(string examName, DateTime examDate)=> Result.Fail("Não implementado");

        public async Task<IEnumerable<Exam>> GetAll()=> (await _exams.Find(_ => true).ToListAsync()).ToExams();

        public async Task<Result<Exam>> Insert(Exam exam) {

            await _exams.InsertOneAsync(exam.ToExamDataDto());

            return Result.Ok(exam);
        }

        public async Task<Exam> GetExamById(string examId)=> (await _exams.Find(e => e.Id == examId).FirstOrDefaultAsync())?.ToExam();
    }
}