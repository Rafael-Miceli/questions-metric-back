using System.ComponentModel.DataAnnotations;

namespace question_metrics_api.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="Username é obrigatório", AllowEmptyStrings=false)]
        public string Username { get; set; }
        [Required(ErrorMessage="Senha é obrigatório", AllowEmptyStrings=false)]
        public string Password { get; set; }
    }
}