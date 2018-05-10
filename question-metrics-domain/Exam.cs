using System;
using System.Linq;
using System.Collections.Generic;

namespace question_metrics_domain
{
    public class Exam
    {
        public string Name { get; }
        public IEnumerable<Question> Questions { get; }
        public ExamMetrics ExamMetrics => new ExamMetrics(Questions);
    }

    public class ExamMetrics
    {
        public ExamMetrics(IEnumerable<Question> questions)
        {
            Questions = questions;    
        }

        public IEnumerable<Question> Questions { get; }

        public int TotalQuestions => Questions.Count();
        public int TotalWrongQuestions => Questions.Count(q => q.IsAnswerWrong);
        public int TotalCorrectQuestions => Questions.Count(q => q.IsAnswerCorrect);
        public IEnumerable<(string reason, int total)> ReasonsMissedQuestions => Questions.GroupBy(q => q.WhyIsWrong)
                                                                                            .OrderByDescending(q => q.Count())
                                                                                            .Select(q => (q.Key, q.Count()));
    }
}
