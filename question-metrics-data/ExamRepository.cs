using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using MongoDB.Driver;

using question_metrics_domain;
using question_metrics_domain.Common;
using question_metrics_domain.Interfaces;

namespace question_metrics_data {
    public class ExamRepository : IExamRepo {

        private IMongoDatabase _mongoDb;
        private IMongoCollection<ExamDataDto> _exams { get; set; }
        private readonly string _connectionString = string.Empty;

        public ExamRepository(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("QuestionMetrics");
            InitializeMongoDatabase();
            _exams = _mongoDb.GetCollection<ExamDataDto>("exams");
        }

        private void InitializeMongoDatabase() {
            try {
                //var client = new MongoClient("mongodb://mongo:27017");
                //var client = new MongoClient("mongodb://localhost:27017");

                MongoClientSettings settings = MongoClientSettings.FromUrl(
                    new MongoUrl(_connectionString)
                );

                settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

                var client = new MongoClient(settings);
                _mongoDb = client.GetDatabase("QuestionMetrics");
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                _mongoDb = null;
            }
        }

        public async Task<Result> Delete(string examName, DateTime examDate)=> Result.Fail("Não implementado");

        public async Task<IEnumerable<Exam>> GetAll()=> (await _exams.Find(_ => true).ToListAsync()).ToExams();

        public async Task<Result<Exam>> Insert(Exam exam) {

            await _exams.InsertOneAsync(exam.ToExamDataDto());

            return Result.Ok(exam);
        }

        public async Task<Exam> GetExamById(string examId)=> (await _exams.Find(e => e.Id == examId).FirstOrDefaultAsync()).ToExam();
    }

    public static class DataUtils {
        public static IEnumerable<Exam> ToExams(this IEnumerable<ExamDataDto> examsData) {
            return examsData.Select(e => e.ToExam());
        }

        public static Exam ToExam(this ExamDataDto examData) {
            return new Exam(
                examData.Name,
                examData.Date,
                examData.Questions.ToQuestions(),
                examData.Id
            );
        }

        public static IEnumerable<Question> ToQuestions(this IEnumerable<QuestionDataDto> questionDataDto) {
            return questionDataDto.Select(e => e.ToQuestion());
        }

        public static Question ToQuestion(this QuestionDataDto questionData) {
            return new Question(
                questionData.Number,
                questionData.Answer.ToAnswer()
            );
        }

        public static Answer ToAnswer(this AnswerDataDto answerData) {
            if (answerData.IsAnswerWrong)
                return new WrongAnswer(ReasonIsWrong.GetReasonIsWrongById(answerData.ReasonIsWrongId));
            
            return new CorrectAnswer();
        }



        public static ExamDataDto ToExamDataDto(this Exam exam) {
            return new ExamDataDto{
                Name = exam.Name,
                Date = exam.Date,
                Questions = exam.Questions.ToQuestionsDataDto(),
                Id = exam.Id
            };
        }

        public static IEnumerable<QuestionDataDto> ToQuestionsDataDto(this IEnumerable<Question> question) {
            return question.Select(e => e.ToQuestionDataDto());
        }

        public static QuestionDataDto ToQuestionDataDto(this Question questionData) {
            return new QuestionDataDto {
                Number = questionData.Number,
                Answer = questionData.Answer.ToAnswerDataDto()
            };
        }

        public static AnswerDataDto ToAnswerDataDto(this Answer answerData) {
            return new AnswerDataDto {
                IsAnswerWrong = answerData.IsAnswerWrong,
                ReasonIsWrongId = answerData.WhyIsWrong == null ? 0 : answerData.WhyIsWrong.Id
            };
        }
    }
}