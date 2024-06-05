using APIHospiTEC.Models;
using APIHospiTEC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIHospiTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservacionController : ControllerBase
    {
        private readonly PostgreSqlService _postgresql;

        public ReservacionController(PostgreSqlService postgresql)
        {
            _postgresql = postgresql;
        }
        [HttpPost]
        [Route("registrar_reservacion")]

        public async Task<IActionResult> InsertReservacion(Reservacion modelo)
        {
            var result = await _postgresql.InsertReservacionAsync(modelo);
            return Ok(result);

        }
        [HttpGet]
        [Route("reservaciones/{cedula}")]
        public async Task<IActionResult> GetReservacionesPorCedula(int cedula)
        {
            var data = await _postgresql.GetReservacionesPorCedulaAsync(cedula);
            return Ok(data);
        }
        [HttpGet]
        [Route("reservaciones/{fecha}/{cedula}")]
        public async Task<IActionResult> GetReservacionesPorCedulaYFecha(DateOnly fecha, int cedula)
        {
            var data = await _postgresql.GetReservacionesPorFechaAsync(fecha,cedula);
            return Ok(data);
        }
        [HttpPut]
        [Route("actualizar_reservacion/{id}")]
        public async Task<IActionResult> UpdateReservacion([FromBody] ReservacionParaPut modelo, int id)
        {
            var result = await _postgresql.UpdateReservacionAsync(modelo, id);
            if (result == 0)
            {
                return NotFound();
            }
            else
                return Ok(result);
        }
        [HttpDelete]
        [Route("eliminar_reservacion/{id}")]
        public async Task<IActionResult> DeleteReservacion(int id)
        {
            var result = await _postgresql.DeleteReservacionAsync(id);
            if (result == 0)
            {
                return NotFound();
            }
            else
                return Ok();
        }
    }
}
