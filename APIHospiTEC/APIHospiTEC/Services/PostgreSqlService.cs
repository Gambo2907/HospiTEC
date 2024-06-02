using APIHospiTEC.Data;
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
                $"FROM procedimiento_medico WHERE cedula = @cedula";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@cedula", cedula)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return dataTable.Rows.Count > 0 ? ConvertDataRowToDictionary(dataTable.Rows[0]) : null;
        }


        private Dictionary<string, object> ConvertDataRowToDictionary(DataRow dataRow)
        {
            throw new NotImplementedException();
        }

        public async Task<int> InsertHistorialAsync(int id_historial, DateOnly fecha, string Tratamiento, int cedula, int id_procedimiento)
        {
            var query = $"INSERT INTO " +
                $"historial " +
                $"VALUES ('{id_historial}','{fecha}','{Tratamiento}','{cedula}','{id_procedimiento}')";
            return await _dataAccess.ExecuteNonQueryAsync(query);
        }

        //Obtiene el historial de pacientes que hay en la db 
        public async Task<List<Dictionary<string, object>>> GetHistorialAsync()
        {
            var query = "SELECT id_historial,fecha,Tratamiento,cedula,id_procedimiento " +
                "FROM historial";
            var dataTable = await _dataAccess.ExecuteQueryAsync(query);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<Dictionary<string, object>> GetHistorialPorCedulaAsync(int cedula)
        {
            var query = $"SELECT id_historial,fecha,Tratamiento,cedula,id_procedimiento " +
                $"FROM procedimiento_medico WHERE cedula = @cedula";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@cedula", cedula)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return dataTable.Rows.Count > 0 ? ConvertDataRowToDictionary(dataTable.Rows[0]) : null;
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
