using APIHospiTEC.Data;
using APIHospiTEC.Models;
using Npgsql;
using System.Data;

namespace APIHospiTEC.Services
{
    public class PostgreSqlService
    {
        private readonly DataAccessLayer _dataAccess;

        public PostgreSqlService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSqlConnection");
            _dataAccess = new DataAccessLayer(connectionString);
        }

        //Obtiene los procedimientos medicos que hay en la db 
        public async Task<List<Dictionary<string, object>>> GetProcedimientosMedicosAsync()
        {
            var query = "SELECT nombre,cantdias FROM procedimiento_medico";
            var dataTable = await _dataAccess.ExecuteQueryAsync(query);
            return ConvertDataTableToList(dataTable);
        }

        //Inserta un procedimiento medico en la tabla procedimientos_medicos en la db 
        public async Task<int> InsertProcedimientoMedicoAsync(string nombre, int cantdias)
        {
            var query = $"INSERT INTO procedimiento_medico (nombre,cantdias) VALUES ('{nombre}','{cantdias}')";
            return  await _dataAccess.ExecuteNonQueryAsync(query);
            
        }

        public async Task<Dictionary<string, object>> GetProcedimientoMedicoPorNombreAsync(string nombre)
        {
            var query = $"SELECT * FROM procedimiento_medico WHERE nombre = @nombre";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@nombre", nombre)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return dataTable.Rows.Count > 0 ? ConvertDataRowToDictionary(dataTable.Rows[0]) : null;
        }


        //Inserta un paciente en la tabla paciente de la db 
        public async Task<int> InsertPacienteAsync(string nombre, string ap1, string ap2, int cedula, DateOnly nacimiento,
            string? direccion, string correo, string password)
        {
            var query = $"INSERT INTO " +
                $"paciente " +
                $"VALUES ('{nombre}','{ap1}','{ap2}','{cedula}','{nacimiento}','{direccion}','{correo}','{password}')";
            return await _dataAccess.ExecuteNonQueryAsync(query);

        }

        //Obtiene los pacientes que hay en la db 
        public async Task<List<Dictionary<string, object>>> GetPacientesAsync()
        {
            var query = "SELECT nombre,ap1,ap2,cedula,nacimiento,direccion,correo " +
                "FROM paciente";
            var dataTable = await _dataAccess.ExecuteQueryAsync(query);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<Dictionary<string, object>> GetPacientePorCedulaAsync(int cedula)
        {
            var query = $"SELECT nombre,ap1,ap2,cedula,nacimiento,direccion,correo " +
                $"FROM paciente WHERE cedula = @cedula";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@cedula", cedula)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return dataTable.Rows.Count > 0 ? ConvertDataRowToDictionary(dataTable.Rows[0]) : null;
        }


        public async Task<int> InsertHistorialAsync(Historial historial)
        {
            var query = $"INSERT INTO " +
                $"historial_medico " +
                $"VALUES ('{historial.id}','{historial.fecha}','{historial.tratamiento}','{historial.cedulapaciente}','{historial.id_procedimiento}')";
            return await _dataAccess.ExecuteNonQueryAsync(query);
        }

        //Obtiene el historial de pacientes que hay en la db 
        public async Task<List<Dictionary<string, object>>> GetHistorialAsync()
        {
            var query = "SELECT id,fecha,tratamiento,cedulapaciente,id_procedimiento " +
                "FROM historial_medico";
            var dataTable = await _dataAccess.ExecuteQueryAsync(query);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<Dictionary<string, object>> GetHistorialPorCedulaAsync(int cedula)
        {
            var query = $"SELECT id,fecha,tratamiento,cedulapaciente,id_procedimiento " +
                $"FROM historial_medico WHERE cedulapaciente = @cedula";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@cedula", cedula)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return dataTable.Rows.Count > 0 ? ConvertDataRowToDictionary(dataTable.Rows[0]) : null;
        }

        public async Task<int> InsertSalonAsync(int numsalon, string nombre, int? piso, int tipo_de_medicina)
        {
            var query = $"INSERT INTO " +
                $"salon " +
                $"VALUES ('{numsalon}','{nombre}','{piso}','{tipo_de_medicina}')";
            return await _dataAccess.ExecuteNonQueryAsync(query);

        }

        public async Task<List<Dictionary<string, object>>> GetSalonesAsync()
        {
            var query = @"
                SELECT s.numsalon, s.nombre, s.piso, tp.descripcion AS TipoMedicina, COUNT(*) AS CantCamas
                FROM salon AS s
                JOIN tipo_medicina AS tp ON s.id_tipo_medicina = tp.id
                JOIN cama AS c ON s.numsalon = c.num_salon
                GROUP BY s.numsalon, s.nombre, s.piso, tp.descripcion
                ORDER BY s.numsalon;
            ";
            var dataTable = await _dataAccess.ExecuteQueryAsync(query);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<Dictionary<string, object>?> GetSalonPorNumSalonAsync(int numsalon)
        {
            var query = @"
                SELECT s.nombre, s.piso, tp.descripcion AS TipoMedicina, COUNT(*) AS CantCamas
                FROM salon AS s
                JOIN tipo_medicina AS tp ON s.id_tipo_medicina = tp.id
                JOIN cama AS c ON s.numsalon = c.num_salon
                WHERE s.numsalon = @numsalon
                GROUP BY s.nombre, s.piso, tp.descripcion;
            ";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@numsalon", numsalon)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return dataTable.Rows.Count > 0 ? ConvertDataRowToDictionary(dataTable.Rows[0]) : null;
        }

        public async Task<int> UpdateSalonAsync(Salon salon)
        {
            var query = "UPDATE salon " +
                "SET nombre = @nombre, piso = @piso, id_tipo_medicina = @id_tipo_medicina " +
                "WHERE numsalon = @numsalon";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@numsalon", salon.numsalon),
            new NpgsqlParameter("@nombre", salon.nombre),
            new NpgsqlParameter("@piso", salon.piso),
            new NpgsqlParameter("@id_tipo_medicina", salon.id_tipo_medicina)
            };
            return await _dataAccess.ExecuteNonQueryAsync(query, parameters);
        }
        public async Task<int> DeleteSalonAsync(int numsalon)
        {
            var query = @"DELETE FROM salon
                        WHERE numsalon = @numsalon;";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@numsalon", numsalon),
            };
            return await _dataAccess.ExecuteNonQueryAsync(query, parameters);
        }

        //Logins

        public async Task<Dictionary<string, object>?> LoginPacienteAsync(string? correo,string? password)
        {
            var query = @"
                SELECT nombre,ap1,ap2,cedula,nacimiento,direccion,correo
                FROM paciente
                WHERE correo = @correo AND password = @password;
            ";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@correo", correo),
            new NpgsqlParameter("@password", password)

            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return dataTable.Rows.Count > 0 ? ConvertDataRowToDictionary(dataTable.Rows[0]) : null;
        }

        public async Task<Dictionary<string, object>?> LoginAdminAsync(string? correo, string? password)
        {
            var query = @"
                SELECT nombre,ap1,ap2,cedula,direccion,nacimiento,correo,fechaingreso
                FROM personal
                WHERE correo = @correo AND password = @password AND idtipopersonal = 3;
            ";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@correo", correo),
            new NpgsqlParameter("@password", password)

            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return dataTable.Rows.Count > 0 ? ConvertDataRowToDictionary(dataTable.Rows[0]) : null;
        }
        public async Task<Dictionary<string, object>?> LoginDoctorAsync(string? correo, string? password)
        {
            var query = @"
                SELECT nombre,ap1,ap2,cedula,direccion,nacimiento,correo,fechaingreso
                FROM personal
                WHERE correo = @correo AND password = @password AND idtipopersonal = 1 ;
            ";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@correo", correo),
            new NpgsqlParameter("@password", password)

            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return dataTable.Rows.Count > 0 ? ConvertDataRowToDictionary(dataTable.Rows[0]) : null;
        }

        public async Task<int> InsertPersonalAsync(Personal personal)
        {
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@nombre",personal.nombre),
            new NpgsqlParameter("@ap1",personal.ap1),
            new NpgsqlParameter("@ap2",personal.ap2),
            new NpgsqlParameter("@cedula",personal.cedula),
            new NpgsqlParameter("@direccion",personal.direccion),
            new NpgsqlParameter("@nacimiento",personal.nacimiento),
            new NpgsqlParameter("@correo",personal.correo),
            new NpgsqlParameter("@password",personal.password),
            new NpgsqlParameter("@fechaingreso",personal.fechaingreso),
            new NpgsqlParameter("@idtipopersonal",personal.idtipopersonal),
            };
            return await _dataAccess.CallStoredProcedureAsync("crear_personal", parameters);
        }

        public async Task<List<Dictionary<string, object>>> GetPersonalAsync()
        {
            var query = @"
                SELECT p.nombre, p.ap1, p.ap2, p.cedula, p.direccion, p.nacimiento, p.correo, p.fechaingreso,
                tpe.descripcion as TipoPersonal
                FROM personal AS p
                JOIN tipo_personal as tpe ON p.idtipopersonal = tpe.id;
            ";
            
            var dataTable = await _dataAccess.ExecuteQueryAsync(query);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<List<Dictionary<string, object>>> GetPersonalPorCedulaAsync(int cedula)
        {
            var query = @"
                SELECT p.nombre, p.ap1, p.ap2, p.cedula, p.direccion, p.nacimiento, p.correo, p.fechaingreso,
                tpe.descripcion as TipoPersonal
                FROM personal AS p
                JOIN tipo_personal as tpe ON p.idtipopersonal = tpe.id
                WHERE @cedula = p.cedula;
            ";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@cedula", cedula)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query,parameters);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<int> UpdatePersonalAsync(Personal personal)
        {
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@u_nombre",personal.nombre),
            new NpgsqlParameter("@u_ap1",personal.ap1),
            new NpgsqlParameter("@u_ap2",personal.ap2),
            new NpgsqlParameter("@u_cedula",personal.cedula),
            new NpgsqlParameter("@u_direccion",personal.direccion),
            new NpgsqlParameter("@u_nacimiento",personal.nacimiento),
            new NpgsqlParameter("@u_correo",personal.correo),
            new NpgsqlParameter("@u_password",personal.password),
            new NpgsqlParameter("@u_fechaingreso",personal.fechaingreso),
            new NpgsqlParameter("@u_idtipopersonal",personal.idtipopersonal),
            };
            return await _dataAccess.CallStoredProcedureAsync("update_personal", parameters);
        }

        public async Task<int> DeletePersonalAsync(int cedula)
        {
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@ced_elimin",cedula),
            };
            return await _dataAccess.CallStoredProcedureAsync("eliminar_personal", parameters);
        }


        // Método para convertir DataTable a List<Dictionary<string, object>>
        public List<Dictionary<string, object>> ConvertDataTableToList(DataTable dataTable)
        {
            var list = new List<Dictionary<string, object>>();

            foreach (DataRow row in dataTable.Rows)
            {
                var dict = new Dictionary<string, object>();

                foreach (DataColumn column in dataTable.Columns)
                {
                    dict[column.ColumnName] = row[column];
                }

                list.Add(dict);
            }

            return list;
        }

        public async Task<List<Dictionary<string, object>>> GetTipoPersonalAsync()
        {
            var query = @"
                SELECT *
                FROM tipo_personal;
            ";

            var dataTable = await _dataAccess.ExecuteQueryAsync(query);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<List<Dictionary<string, object>>> GetTipoPersonalPoridAsync(int id)
        {
            var query = @"
                SELECT *
                FROM tipo_personal
                WHERE @id = id;
            ";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@id", id)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<string> EncryptPassword(string? password)
        {
            // Llamar a la función en la base de datos para encriptar la contraseña
            var query = "SELECT encriptar_passwords(@password)";
            var parameters = new NpgsqlParameter("@password", password);
            var encryptedPassword = await _dataAccess.ExecuteScalarAsync<string>(query, parameters);
            return encryptedPassword;
        }

        // Método para convertir DataRow a Dictionary<string, object>
        private Dictionary<string, object> ConvertDataRowToDictionary(DataRow row)
        {
            var dict = new Dictionary<string, object>();

            foreach (DataColumn column in row.Table.Columns)
            {
                dict[column.ColumnName] = row[column];
            }

            return dict;
        }
    }
}
