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

        public async Task<DataTable> ExecuteQueryAsync(string query)
        {
            using var connection = await GetConnectionAsync();
            using var command = new NpgsqlCommand(query, connection);
            using var adapter = new NpgsqlDataAdapter(command);

            var dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable;
        }

        public async Task<int> ExecuteNonQueryAsync(string query)
        {
            using var connection = await GetConnectionAsync();
            using var command = new NpgsqlCommand(query, connection);
            return await command.ExecuteNonQueryAsync();
        }
    }
}
