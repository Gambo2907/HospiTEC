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
        [HttpPost]
        [Route("registrar_patologia_por_paciente")]

        public async Task<IActionResult> InsertPatologiaPorPaciente(PatologiaPorPaciente modelo)
        {
            var result = await _postgresql.InsertPatologiaPorPacienteAsync(modelo.id_patologia,modelo.cedulapaciente,modelo.tratamiento);
            return Ok(result);

        }
        [HttpPost]
        [Route("registrar_telefono_paciente")]

        public async Task<IActionResult> InsertTelefonoPaciente(Telefonos modelo)
        {
            var result = await _postgresql.InsertTelefonoPacienteAsync(modelo);
            return Ok(result);

        }
        [HttpGet]
        [Route("telefonos_paciente/{cedula}")]
        public async Task<IActionResult> GetTelefonosPorCedulaPaciente(int cedula)
        {
            var data = await _postgresql.GetTelefonosPorPacienteAsync(cedula);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [HttpGet]
        [Route("patologias/{cedula}")]
        public async Task<IActionResult> GetPatologiasPorCedula(int cedula)
        {
            var data = await _postgresql.GetPatologiasPorPacienteAsync(cedula);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
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
        public async Task<IActionResult> GetPacientePorCedula(int cedula)
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
