using System;
using System.ComponentModel.DataAnnotations;

namespace question_metrics_api.Controllers.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage="Nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage="Senha é obrigatória")]        
        public string Password { get; set; }

        [Required(ErrorMessage="Email é obrigatório")]
        [EmailAddress(ErrorMessage="Email com formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage="Data de Nascimento é obrigatório")]
        public DateTime Birth { get; set; }
    }
}