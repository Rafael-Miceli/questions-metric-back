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
        public void Should_Order_Wrong_Questions_With_Its_Counts_In_Exam()
        {
            var questions = new List<Question>{
                new Question(1, new WrongAnswer("motivo 1")),
                new Question(1, new WrongAnswer("motivo 1")),
                new Question(1, new WrongAnswer("motivo 1")),
                new Question(1, new WrongAnswer("motivo 1")),
                new Question(1, new WrongAnswer("motivo 1"))
            };

            var exam = new Exam("RS", DateTime.Today, questions);

            //var examMetrics = new ExamMetrics(questions);

            Assert.Equal(3, exam.ExamMetrics.ReasonsMissedQuestions.Count());
            Assert.Equal("motivo 1", exam.ExamMetrics.ReasonsMissedQuestions.First().Reason);
            Assert.Equal(2, exam.ExamMetrics.ReasonsMissedQuestions.First().Total);
            Assert.Equal("motivo 3", exam.ExamMetrics.ReasonsMissedQuestions.Skip(1).First().Reason);
            Assert.Equal(2, exam.ExamMetrics.ReasonsMissedQuestions.Skip(1).First().Total);
            Assert.Equal("motivo 2", exam.ExamMetrics.ReasonsMissedQuestions.Skip(2).First().Reason);
            Assert.Equal(1, exam.ExamMetrics.ReasonsMissedQuestions.Skip(2).First().Total);
        }

        [Fact]
        public void Should_Order_Wrong_Questions_With_Its_Counts_In_ExamsList()
        {
            var questions1 = new List<Question>{
                new Question(1, new WrongAnswer("motivo 1")),
                new Question(1, new WrongAnswer("motivo 1")),
                new Question(1, new WrongAnswer("motivo 1")),
                new Question(1, new WrongAnswer("motivo 1")),
                new Question(1, new WrongAnswer("motivo 1"))
            };

            var questions2 = new List<Question>{
                new Question(2, new WrongAnswer("motivo 1")),
                new Question(2, new WrongAnswer("motivo 1")),
                new Question(2, new WrongAnswer("motivo 1")),
                new Question(2, new WrongAnswer("motivo 1")),
                new Question(2, new WrongAnswer("motivo 1"))
            };

            var exam1 = new Exam("RS", DateTime.Today, questions1);
            var exam2 = new Exam("PR", DateTime.Today, questions2);

            var examMetrics = new ExamMetrics(new List<Exam>{exam1, exam2});

            Assert.Equal(4, examMetrics.ReasonsMissedQuestions.Count());
            Assert.Equal("motivo 1", examMetrics.ReasonsMissedQuestions.First().Reason);
            Assert.Equal(4, examMetrics.ReasonsMissedQuestions.First().Total);
            Assert.Equal("motivo 3", examMetrics.ReasonsMissedQuestions.Skip(1).First().Reason);
            Assert.Equal(3, examMetrics.ReasonsMissedQuestions.Skip(1).First().Total);
            Assert.Equal("motivo 2", examMetrics.ReasonsMissedQuestions.Skip(2).First().Reason);
            Assert.Equal(2, examMetrics.ReasonsMissedQuestions.Skip(2).First().Total);
            Assert.Equal("motivo 4", examMetrics.ReasonsMissedQuestions.Skip(3).First().Reason);
            Assert.Equal(1, examMetrics.ReasonsMissedQuestions.Skip(3).First().Total);
        }
    }
}
