using APIHospiTEC.Data;
using APIHospiTEC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;

namespace APIHospiTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly HospiTECcontext _context;
        /*
         *Constructor de la clase con un contexto de base de datos 
         */
        public PacienteController(HospiTECcontext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("crear_paciente")]
        public async Task<IActionResult> CrearPaciente(Paciente modelo)
        {
            await _context.paciente.AddAsync(modelo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("paciente/{cedula}")]
        public async Task<ActionResult<Paciente>> ObtenerPacientePorCedula(int cedula)
        {
            var pac = await _context.paciente.FromSqlRaw("SELECT * FROM paciente WHERE cedula = @cedula",
                new NpgsqlParameter("cedula" , cedula)).FirstOrDefaultAsync();
            
            if (pac == null)
            {
                return NotFound();
            }

            return Ok(pac);
        }

        [HttpDelete]
        [Route("eliminar_paciente")]
        public async Task<IActionResult> EliminarPaciente(int cedula)
        {
            var paciente_borrado = await _context.paciente.FindAsync(cedula);
            _context.paciente.Remove(paciente_borrado!);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
