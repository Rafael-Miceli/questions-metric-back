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
}


