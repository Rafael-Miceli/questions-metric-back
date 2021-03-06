using System;
using System.Collections.Generic;
using System.Linq;

using NullGuard;

namespace question_metrics_domain {
    public class Exam {
        public Exam(string name, DateTime date, IList<Question> questions, [AllowNull] string id = "") {
            Name = name;
            Date = date;
            Questions = questions;

            if (string.IsNullOrEmpty(id))
                id = Guid.NewGuid().ToString();

            Id = id;
        }

        public string Id { get; set; }
        public string Name { get; }
        public DateTime Date { get; }
        public IList<Question> Questions { get; }
        public ExamMetrics ExamMetrics => new ExamMetrics(new List<Exam> { this });
    }

    public class ExamMetrics {
        public ExamMetrics(IEnumerable<Exam> exams) {
            Questions = exams.SelectMany(e => e.Questions);
        }

        public IEnumerable<Question> Questions { get; }

        public int TotalQuestions => Questions.Count();
        public int TotalWrongQuestions => Questions.Count(q => q.Answer.IsAnswerWrong);
        public int TotalCorrectQuestions => Questions.Count(q => q.Answer.IsAnswerCorrect);
        public IEnumerable<ReasonMissedQuestion> ReasonsMissedQuestions => Questions.Where(q => q.Answer is WrongAnswer)
            .GroupBy(q => q.Answer.WhyIsWrong.Reason)
            .OrderByDescending(q => q.Count())
            .Select(q => new ReasonMissedQuestion(q.Key, q.Count()));

    }

    public class ReasonMissedQuestion {
        public ReasonMissedQuestion(string reason, int total) {
            Reason = reason;
            Total = total;

        }
        public string Reason { get; set; }
        public int Total { get; set; }
    }

}