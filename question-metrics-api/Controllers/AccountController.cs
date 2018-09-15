using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using question_metrics_api.ViewModels;
using question_metrics_domain;
using question_metrics_domain.Interfaces;
using Force.DeepCloner;
using Serilog;
using System.Collections.Generic;

namespace question_metrics_api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IExamRepo _examRepo;

        public AccountController(IUserRepository userRepository, IExamRepo examRepo)
        {
            _userRepository = userRepository;
            _examRepo = examRepo;
        }
        //Cenários de teste
        //* Identificador de usuário é o e-mail */
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginDto)
        {
            User userInDatabase = await _userRepository.FindByLoginAndPassword(loginDto.Username, loginDto.Password);

            if (userInDatabase != null)
                return Ok(userInDatabase.ToViewModel());

            return Unauthorized();
        }
    }
}