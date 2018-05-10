using System;
using System.Linq;
using Xunit;
using question_metrics_domain;
using System.Collections.Generic;

namespace question_metrics_domain_tests
{
    public class UnitTest1
    {
        [Fact]
        public void Should_Order_Wrong_Questions_With_Its_Counts()
        {
            var questions = new List<Question>{
                new Question(true, "motivo 1"),
                new Question(true, "motivo 1"),
                new Question(true, "motivo 2"),
                new Question(true, "motivo 3"),
                new Question(true, "motivo 3")
            };

            var examMetrics = new ExamMetrics(questions);

            Assert.Equal(3, examMetrics.ReasonsMissedQuestions.Count());
            Assert.Equal("motivo 1", examMetrics.ReasonsMissedQuestions.First().Reason);
            Assert.Equal(2, examMetrics.ReasonsMissedQuestions.First().Total);
            Assert.Equal("motivo 3", examMetrics.ReasonsMissedQuestions.Skip(1).First().Reason);
            Assert.Equal(2, examMetrics.ReasonsMissedQuestions.Skip(1).First().Total);
            Assert.Equal("motivo 2", examMetrics.ReasonsMissedQuestions.Skip(2).First().Reason);
            Assert.Equal(1, examMetrics.ReasonsMissedQuestions.Skip(2).First().Total);
        }
    }
}
