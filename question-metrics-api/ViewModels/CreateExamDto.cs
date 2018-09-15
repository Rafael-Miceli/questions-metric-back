using System;
using System.ComponentModel.DataAnnotations;

namespace question_metrics_api.ViewModels
{
    public class CreateExamViewModel
    {
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Nome do exame é obrigatório")]
        public string Name { get; set; }
        
        public int TotalNumberOfQuestions { get; set; }
    }
}