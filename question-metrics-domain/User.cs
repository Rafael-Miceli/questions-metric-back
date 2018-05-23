using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace question_metrics_domain
{
    public class User
    {
        public User(string name, string password, string email)
        {
            Name = name;
            Password = password;
            Email = email;
        }

        public string Name { get; }
        public string Password { get; }
        public string Email { get; }

        public List<Exam> Exams { get; }

        public async Task AddExam(Exam exam)
        {
            Exams.Add(exam);
        }
    }
}
