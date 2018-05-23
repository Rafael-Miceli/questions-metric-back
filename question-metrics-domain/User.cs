using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace question_metrics_domain
{
    public class User
    {
        public User()
        {
        }

        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public List<Exam> Exams { get; }

        public async Task AddExam(Exam exam)
        {
            Exams.Add(exam);
        }
    }
}
