
/*
 * Modelo del login, con esto el usuario va a ser capaz de ingresar sus datos para realizar el logeo
 * correcto al sistema.
 * */

namespace APIHospiTEC.Models
{
    public class LoginModel
    {
        public string? Correo { get; set; }
        public string? Password { get; set; }
    }
}
