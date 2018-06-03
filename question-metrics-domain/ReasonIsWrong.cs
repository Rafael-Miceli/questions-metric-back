namespace question_metrics_domain
{
    public class ReasonIsWrong
    {
        public static ReasonIsWrong DidntReadProperlyAnswer = new ReasonIsWrong("Me Confundi na leitura da resposta.");
        public static ReasonIsWrong DidntReadProperlyQuestion = new ReasonIsWrong("Me Confundi na leitura da pergunta.");
        public static ReasonIsWrong NotAnswered = new ReasonIsWrong("Não respondida.");
        public static ReasonIsWrong ForgetSubject = new ReasonIsWrong("Esqueci o asunto da matéria.");

        //public static ReasonIsWrong CorrectAnswer = new ReasonIsWrong("");

        private ReasonIsWrong(string reason)
        {
            Reason = reason;
        }

        public string Reason {get;}
    }
}


