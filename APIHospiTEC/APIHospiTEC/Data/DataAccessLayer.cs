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
        public async Task BulkInsertAsync(string tableName, DataTable dataTable)
        {

            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();
            try
            {
                using var writer = connection.BeginBinaryImport($"COPY {tableName} FROM STDIN (FORMAT BINARY)");

                foreach (DataRow row in dataTable.Rows)
                {
                    writer.StartRow();
                    foreach (var item in row.ItemArray)
                    {
                        writer.Write(item);
                    }
                }

                await writer.CompleteAsync();
                await transaction.CommitAsync();

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
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
        public async Task<T> ExecuteScalarAsync<T>(string query, params NpgsqlParameter[] parameters)
        {
            using var connection = await GetConnectionAsync();
            using var command = new NpgsqlCommand(query, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            var result = await command.ExecuteScalarAsync();
            return (T)Convert.ChangeType(result, typeof(T));
        }
        public async Task<int> CallStoredProcedureAsync(string procedureName, params NpgsqlParameter[] parameters)
        {
            using var connection = await GetConnectionAsync();
            using var command = new NpgsqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            return await command.ExecuteNonQueryAsync();
        }
        public async Task ExecuteStoredProcedureAsync(string procedureName)
        {
            using var connection = await GetConnectionAsync();
            using var command = new NpgsqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            await command.ExecuteNonQueryAsync();
        }


    }
}
