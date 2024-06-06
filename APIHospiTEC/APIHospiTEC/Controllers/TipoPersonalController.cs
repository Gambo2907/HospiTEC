using APIHospiTEC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIHospiTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPersonalController : ControllerBase
    {
        private readonly PostgreSqlService _postgresql;

        public TipoPersonalController(PostgreSqlService postgresql)
        {
            _postgresql = postgresql;
        }
        [HttpGet]
        [Route("tipo_personal")]
        public async Task<IActionResult> GetTipoPersonal()
        {
            var data = await _postgresql.GetTipoPersonalAsync();
            return Ok(data);
        }
        [HttpGet]
        [Route("tipo_personal/{id}")]
        public async Task<IActionResult> GetTipoPersonalPorID(int id)
        {
            var data = await _postgresql.GetTipoPersonalPoridAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
    }
}
