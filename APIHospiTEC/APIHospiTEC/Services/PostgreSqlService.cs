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
    }
}
