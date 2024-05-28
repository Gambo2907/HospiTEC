using APIHospiTEC.Data;
using APIHospiTEC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            await _context.Paciente.AddAsync(modelo);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete]
        [Route("eliminar_paciente")]
        public async Task<IActionResult> EliminarPaciente(string cedula)
        {
            var paciente_borrado = await _context.Paciente.FindAsync(cedula);
            _context.Paciente.Remove(paciente_borrado!);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
