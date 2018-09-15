using System;
using System.ComponentModel.DataAnnotations;

namespace question_metrics_api.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage="Nome é obrigatório", AllowEmptyStrings=false)]
        public string Name { get; set; }

        [Required(ErrorMessage="Senha é obrigatória", AllowEmptyStrings=false)]
        public string Password { get; set; }

        [Required(ErrorMessage="Email é obrigatório", AllowEmptyStrings=false)]
        [EmailAddress(ErrorMessage="Email com formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage="Data de Nascimento é obrigatório")]
        public DateTime Birth { get; set; }
    }
}