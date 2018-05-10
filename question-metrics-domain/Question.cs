using System;

namespace question_metrics_domain
{
    public class Question
    {
        public Question(bool isAnswerWrong, string whyIsWrong = "")
        {
            IsAnswerWrong = isAnswerWrong;
            WhyIsWrong = whyIsWrong;
        }
        public int Number { get; }
        public bool IsAnswerCorrect => !IsAnswerWrong;
        public bool IsAnswerWrong { get; }
        public string WhyIsWrong { get; }
    }
}
