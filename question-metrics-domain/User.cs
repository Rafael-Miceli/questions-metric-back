using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace question_metrics_domain {
    public class User {

        public User (
            string name, 
            string password, 
            string email,
            DateTime birth,
            string id = "") {
                
            Name = name;
            Password = password;
            Email = email;
            Birth = birth;

            TookedExams = new List<Exam> ();
            if (string.IsNullOrEmpty(id))
                id = new Guid().ToString();
        }

        public string Id { get; }
        public string Name { get; }
        public string Password { get; }
        public string Email { get; }
        public DateTime Birth { get; }

        public IList<Exam> TookedExams { get; }

        public async Task AddExam (Exam exam) => TookedExams.Add (exam);
    }
}