using System.Collections.Generic;
using System.Linq;

namespace question_metrics_domain
{
    public class ReasonIsWrong
    {
        public static ReasonIsWrong DidntReadProperlyAnswer = new ReasonIsWrong(1, "Me Confundi na leitura da resposta.");
        public static ReasonIsWrong DidntReadProperlyQuestion = new ReasonIsWrong(2, "Me Confundi na leitura da pergunta.");
        public static ReasonIsWrong NotAnswered = new ReasonIsWrong(3, "Não respondida.");
        public static ReasonIsWrong NotSpecified = new ReasonIsWrong(4, "Não especificado porque errei. Isto não ajuda muito.");
        public static ReasonIsWrong ForgetSubject = new ReasonIsWrong(5, "Esqueci o asunto da matéria.");

        //public static ReasonIsWrong CorrectAnswer = new ReasonIsWrong("");

        private static List<ReasonIsWrong> _reasonsCanBeWrong = new List<ReasonIsWrong> {
            DidntReadProperlyAnswer,
            DidntReadProperlyQuestion,
            NotAnswered,
            NotSpecified,
            ForgetSubject
        };

        public static ReasonIsWrong GetReasonIsWrongById(int id) => _reasonsCanBeWrong.FirstOrDefault(r => r.Id == id);

        private ReasonIsWrong(int id, string reason)
        {
            Id = id;
            Reason = reason;
        }

        public int Id { get; }
        public string Reason {get;}


    }
}


