using System.Collections.Generic;
using System.Linq;

using question_metrics_domain;

namespace question_metrics_data
{
    public static class DataUtils {
        public static IEnumerable<Exam> ToExams(this IEnumerable<ExamDataDto> examsData) {
            return examsData.Select(e => e.ToExam());
        }

        public static Exam ToExam(this ExamDataDto examData) {
            return new Exam(
                examData.Name,
                examData.Date,
                examData.Questions.ToQuestions().ToList(),
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

        public static User ToUser(this UserDataDto userDataDto) {
            return new User(
                userDataDto.Name,
                userDataDto.Password,
                userDataDto.Email,
                userDataDto.Birth,
                userDataDto.Id,
                userDataDto.TookedExams.ToExams().ToList()
            );
        }


        /// ---- ToDataDtos


        public static IEnumerable<ExamDataDto> ToExamsDataDto(this IEnumerable<Exam> exams) {
            return exams.Select(e => e.ToExamDataDto());
        }

        public static ExamDataDto ToExamDataDto(this Exam exam) {
            return new ExamDataDto{
                Name = exam.Name,
                Date = exam.Date,
                Questions = exam.Questions.ToQuestionsDataDto(),
                Id = exam.Id
            };
        }

        public static IEnumerable<QuestionDataDto> ToQuestionsDataDto(this IEnumerable<Question> questions) {
            return questions.Select(e => e.ToQuestionDataDto());
        }

        public static QuestionDataDto ToQuestionDataDto(this Question question) {
            return new QuestionDataDto {
                Number = question.Number,
                Answer = question.Answer.ToAnswerDataDto()
            };
        }

        public static AnswerDataDto ToAnswerDataDto(this Answer answer) {
            return new AnswerDataDto {
                IsAnswerWrong = answer.IsAnswerWrong,
                ReasonIsWrongId = answer.WhyIsWrong == null ? 0 : answer.WhyIsWrong.Id
            };
        }

        public static UserDataDto ToUserDataDto(this User user) {
            return new UserDataDto {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Email = user.Email,
                Birth = user.Birth,
                TookedExams = user.TookedExams.ToExamsDataDto()
            };
        }
    }
}