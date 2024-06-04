using APIHospiTEC.Models;
using APIHospiTEC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIHospiTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        private readonly PostgreSqlService _postgresql;

        public PersonalController(PostgreSqlService postgresql)
        {
            _postgresql = postgresql;
        }
        [HttpPost]
        [Route("registrar_personal")]

        public async Task<IActionResult> InsertPersonal(Personal modelo)
        {
            var result = await _postgresql.InsertPersonalAsync(modelo);
            return Ok(result);

        }
        [HttpPost]
        [Route("registrar_telefono_personal")]

        public async Task<IActionResult> InsertTelefonoPersonal(Telefonos modelo)
        {
            var result = await _postgresql.InsertTelefonoPersonalAsync(modelo);
            return Ok(result);

        }
        [HttpGet]
        [Route("telefonos_personal/{cedula}")]
        public async Task<IActionResult> GetTelefonosPorCedulaPersonal(int cedula)
        {
            var data = await _postgresql.GetTelefonosPorPersonalAsync(cedula);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [HttpGet]
        [Route("personal_registrado")]
        public async Task<IActionResult> GetPersonal()
        {
            var data = await _postgresql.GetPersonalAsync();
            return Ok(data);
        }
        [HttpGet]
        [Route("personal/{cedula}")]
        public async Task<IActionResult> GetPersonalPorCedula(int cedula)
        {
            var data = await _postgresql.GetPersonalPorCedulaAsync(cedula);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPut]
        [Route("actualizar_personal/{cedula}")]

        public async Task<IActionResult> UpdateSalon([FromBody] Personal modelo, int cedula)
        {
            modelo.cedula = cedula;
            var result = await _postgresql.UpdatePersonalAsync(modelo);
            if (result == 0)
            {
                return NotFound();
            }
            else
                return Ok(result);
        }

        [HttpDelete]
        [Route("eliminar_personal/{cedula}")]
        public async Task<IActionResult> DeletePersonal(int cedula)
        {
            var result = await _postgresql.DeletePersonalAsync(cedula);
            if (result == 0)
            {
                return NotFound();
            }
            else
                return Ok();
        }
    }
}
