using APIHospiTEC.Models;
using APIHospiTEC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIHospiTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly PostgreSqlService _postgresql;

        public LoginController(PostgreSqlService postgresql)
        {
            _postgresql = postgresql;
        }
        [HttpPost]
        [Route("login_personal")]
        public async Task<IActionResult> LoginPersonal(Login modelo)
        {
            var passwordencrypt = await _postgresql.EncryptPassword(modelo.password);
            var user = await _postgresql.LoginPersonalAsync(modelo.correo,passwordencrypt);
            if (user == null)
            {
                return NotFound();
            }
            else
                return Ok(user);
        }
        [HttpPost]
        [Route("login_pacientes")]
        public async Task<IActionResult> LoginPaciente(Login modelo)
        {
            var passwordencrypt = await _postgresql.EncryptPassword(modelo.password);
            var user = await _postgresql.LoginPacienteAsync(modelo.correo,passwordencrypt);
            if (user == null)
            {
                return NotFound();
            }
            else
                return Ok(user);
        }
        [HttpPost]
        [Route("login_admin")]
        public async Task<IActionResult> LoginAdmin(Login modelo)
        {
            var passwordencrypt = await _postgresql.EncryptPassword(modelo.password);
            var user = await _postgresql.LoginAdminAsync(modelo.correo,passwordencrypt);
            if (user == null)
            {
                return NotFound();
            }
            else
                return Ok(user);
        }
    }
}
