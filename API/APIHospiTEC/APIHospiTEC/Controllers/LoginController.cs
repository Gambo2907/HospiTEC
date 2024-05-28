using APIHospiTEC.Data;
using APIHospiTEC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIHospiTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //Obtiene el contexto para así poder mostrar y añadir datos a la DB
        private readonly HospiTECcontext _context;

        /*
         *Constructor de la clase con un contexto de base de datos 
         */
        public LoginController(HospiTECcontext context)
        {
            _context = context;
        }
        /*
        *LoginPaciente: Se encarga de corroborar los datos de logeo de un paciente  
        */

        [HttpPost]
        [Route("login_paciente")]
        public async Task<IActionResult> LoginPaciente(LoginModel modelo)
        {

            Paciente? pac = await _context.paciente.Where(o => o.correo == modelo.Correo && o.password == modelo.Password).FirstOrDefaultAsync();
            if (pac == null)
            {
                return NotFound();
            }
            return Ok(pac);
        }


    }
}
