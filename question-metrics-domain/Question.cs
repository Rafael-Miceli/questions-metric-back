using System;

namespace question_metrics_domain
{
    public class Question
    {
        public int Number { get; }
        public bool IsAnswerCorrect { get; }
        public bool IsAnswerWrong => ! IsAnswerCorrect;
        public string WhyIsWrong { get; }
    }
}
