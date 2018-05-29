using System;
using System.Linq;
using System.Collections.Generic;

namespace question_metrics_domain
{
    public class Exam
    {
        public Exam(string name, DateTime date, IEnumerable<Question> questions)
        {
            Name = name;
            Date = date;
            Questions = questions;
        }

        public string Name { get; }
        public DateTime Date { get; }
        public IEnumerable<Question> Questions { get; }
        public ExamMetrics ExamMetrics => new ExamMetrics(new List<Exam>{this});
    }

    public class ExamMetrics
    {
        public ExamMetrics(IEnumerable<Exam> exams)
        {
            Questions = exams.SelectMany(e => e.Questions);    
        }

        public IEnumerable<Question> Questions { get; }

        public int TotalQuestions => Questions.Count();
        public int TotalWrongQuestions => Questions.Count(q => q.Answer.IsAnswerWrong);
        public int TotalCorrectQuestions => Questions.Count(q => q.Answer.IsAnswerCorrect);
        public IEnumerable<(string Reason, int Total)> ReasonsMissedQuestions => Questions.GroupBy(q => q.Answer.WhyIsWrong)
                                                                                            .OrderByDescending(q => q.Count())
                                                                                            .Select(q => (q.Key, q.Count()));

    }
}
