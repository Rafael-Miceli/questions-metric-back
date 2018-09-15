using System;
using question_metrics_domain;
using System.Collections.Generic;


namespace question_metrics_api.ViewModels
{
    public static class UserViewModelExtensions
    {
        public static UserViewModel ToViewModel(this User user) =>
            new UserViewModel {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Birth = user.Birth,
                TookedExams = user.TookedExams
            };

    }

    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birth { get; set; }

        public IEnumerable<Exam> TookedExams { get; set; }

    }
}