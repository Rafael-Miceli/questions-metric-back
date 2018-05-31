using System;

namespace question_metrics_domain
{
    public class Question
    {
        public Question(int number, Answer answer)
        {
            Number = number;
            Answer = answer;
        }
        public int Number { get; }
        public Answer Answer { get; }
    }

    public class WrongAnswer : Answer
    {
        public WrongAnswer(string whyIsWrong) =>
            WhyIsWrong = whyIsWrong;

        public override bool IsAnswerWrong => true;

        private string whyIsWrong;
        public override string WhyIsWrong { get => whyIsWrong; protected set => whyIsWrong = value; }
    }

    public class CorrectAnswer : Answer
    {
        public override bool IsAnswerWrong => false;

        public override string WhyIsWrong { get => ""; protected set => throw new NotImplementedException(); }
    }

    public abstract class Answer
    {
        public abstract bool IsAnswerWrong { get; }
        public bool IsAnswerCorrect { get => !IsAnswerWrong; }
        
        public abstract string WhyIsWrong { get; protected set; }
    }
}


