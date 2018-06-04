using System;
using NullGuard;

namespace question_metrics_domain
{
    public abstract class Answer
    {
        public abstract bool IsAnswerWrong { get; }
        public bool IsAnswerCorrect { get => !IsAnswerWrong; }

        [AllowNull]
        public abstract ReasonIsWrong WhyIsWrong { get; protected set; }
    }

    public class WrongAnswer : Answer
    {
        public WrongAnswer(ReasonIsWrong whyIsWrong) =>
            WhyIsWrong = whyIsWrong;

        public override bool IsAnswerWrong => true;

        private ReasonIsWrong whyIsWrong;
        public override ReasonIsWrong WhyIsWrong { get => whyIsWrong; protected set => whyIsWrong = value; }
    }

    public class CorrectAnswer : Answer
    {
        public override bool IsAnswerWrong => false;

        [AllowNull]
        public override ReasonIsWrong WhyIsWrong { get => null; protected set => throw new NotImplementedException(); }
    }
}


