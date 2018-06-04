using question_metrics_domain;

namespace question_metrics_api.Dtos
{
    public class AnswerToUpdateDto
    {
        public int QuestionNumber { get; set; }
        public bool IsWrong { get; set; }
        public ReasonIsWrongEnum ReasonIsWrong { get; set; }
    }

    public enum ReasonIsWrongEnum
    {
        DidntReadProperlyAnswer,
        DidntReadProperlyQuestion,
        NotAnswered,
        NotSpecified,
        ForgetSubject
    }

    public static class EnumMapHelper
    {
        public static ReasonIsWrong MapToReason(this ReasonIsWrongEnum reasonIsWrongEnum)
        {
            switch (reasonIsWrongEnum)
            {
                case ReasonIsWrongEnum.DidntReadProperlyAnswer:
                    return ReasonIsWrong.DidntReadProperlyAnswer;
                case ReasonIsWrongEnum.DidntReadProperlyQuestion:
                    return ReasonIsWrong.DidntReadProperlyQuestion;
                case ReasonIsWrongEnum.NotAnswered:
                    return ReasonIsWrong.NotAnswered;
                case ReasonIsWrongEnum.ForgetSubject:
                    return ReasonIsWrong.ForgetSubject;
                default:
                    return ReasonIsWrong.NotSpecified;
            }
        }

    }
}