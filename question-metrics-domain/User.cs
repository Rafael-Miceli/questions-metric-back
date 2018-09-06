using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NullGuard;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace question_metrics_domain {
    public class User {

        public User (
            string name, 
            string password, 
            string email,
            DateTime birth,
            [AllowNull] string id = "",
            [AllowNull] List<Exam> tookedExams = null) {
                
            Name = name;
            Password = password.ToHash();
            Email = email;
            Birth = birth;
            TookedExams = tookedExams;
            Id = id;

            if(tookedExams == null)
                TookedExams = new List<Exam> ();
            
            if (string.IsNullOrEmpty(id))
                id = Guid.NewGuid().ToString();            
        }

        public string Id { get; }
        public string Name { get; }
        public string Password { get; }
        public string Email { get; }
        public DateTime Birth { get; }

        public IList<Exam> TookedExams { get; }

        public async Task AddExam (Exam exam) => TookedExams.Add (exam);
    }

    public static class Hasher
    {
        public static string ToHash(this string value)
        {
            var hm = new HMACSHA1(Encoding.ASCII.GetBytes("chave"));
            return Convert.ToBase64String(hm.ComputeHash(Encoding.ASCII.GetBytes(value)));
            // return Convert.ToBase64String(KeyDerivation.Pbkdf2(value, Encoding.ASCII.GetBytes("teste"), KeyDerivationPrf.HMACSHA1, 100, 256/8));
        }
    }
}