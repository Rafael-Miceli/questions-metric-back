using System;
using System.Collections.Generic;

namespace question_metrics_data {
    public class UserDataDto {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Birth { get; set; }

        public IEnumerable<ExamDataDto> TookedExams { get; set; }
    }
}