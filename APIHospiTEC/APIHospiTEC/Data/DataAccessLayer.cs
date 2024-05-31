using APIHospiTEC.Services;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace APIHospiTEC.Data
{
    public class DataAccessLayer
    {

        private readonly string _connectionString;

        public DataAccessLayer(string connectionString)
        {
            _connectionString = connectionString;
        }

        private async Task<NpgsqlConnection> GetConnectionAsync()
        {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        public async Task<DataTable> ExecuteQueryAsync(string query, params NpgsqlParameter[] parameters)
        {
            using var connection = await GetConnectionAsync();
            using var command = new NpgsqlCommand(query, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
                using var adapter = new NpgsqlDataAdapter(command);

                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
            else
            {
                using var adapter = new NpgsqlDataAdapter(command);

                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
            
        }
        public async Task<int> ExecuteNonQueryAsync(string query, params NpgsqlParameter[] parameters)
        {
            using var connection = await GetConnectionAsync();
            using var command = new NpgsqlCommand(query, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
                return await command.ExecuteNonQueryAsync();
            }
            else
            {
                return await command.ExecuteNonQueryAsync();
            }
        }

        
    }
}
