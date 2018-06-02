using System;
using System.ComponentModel.DataAnnotations;

namespace question_metrics_api.Dtos
{
    public class CreateExamDto
    {
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Nome do exame é obrigatório")]
        public string Name { get; set; }
        
        public int TotalNumberOfQuestions { get; set; }
    }
}