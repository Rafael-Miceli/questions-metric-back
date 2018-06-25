using System;
using System.Collections.Generic;

namespace question_metrics_data {
    public class ExamDataDto {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<QuestionDataDto> Questions { get; set; }
    }

    public class QuestionDataDto {

        public int Number { get; set; }
        public AnswerDataDto Answer { get; set; }

    }

    public class AnswerDataDto {
        public bool IsAnswerWrong { get; set; }
        public int ReasonIsWrongId { get; set; }
    }
}