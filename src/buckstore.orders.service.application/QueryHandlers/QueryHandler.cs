using System;
using Npgsql;
using System.Data;

namespace buckstore.orders.service.application.QueryHandlers
{
    public abstract class QueryHandler
    {
        private readonly string _connectionString = Environment.GetEnvironmentVariable("ConnectionString");

        internal IDbConnection DbConnection => new NpgsqlConnection(_connectionString);
    }
}