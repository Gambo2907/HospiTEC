using APIHospiTEC.Models;
using APIHospiTEC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIHospiTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly PostgreSqlService _postgresql;

        public PacienteController(PostgreSqlService postgresql)
        {
            _postgresql = postgresql;
        }
        [HttpPost]
        [Route("registrar_paciente")]

        public async Task<IActionResult> InsertPaciente(Paciente modelo)
        {
            var result = await _postgresql.InsertPacienteAsync(modelo.nombre, modelo.ap1, modelo.ap2, modelo.cedula, modelo.nacimiento,
                modelo.direccion, modelo.correo, modelo.password);
            return Ok(result);

        }

        [HttpGet]
        [Route("pacientes_registrados")]
        public async Task<IActionResult> GetPacientes()
        {
            var data = await _postgresql.GetPacientesAsync();
            return Ok(data);
        }

        [HttpGet]
        [Route("paciente/{cedula}")]
        public async Task<IActionResult> GetPaciente(int cedula)
        {
            var data = await _postgresql.GetPacientePorCedulaAsync(cedula);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

    }
}
