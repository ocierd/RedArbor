using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RedArbor.Application.Common.Interfaces;

namespace RedArbor.Infrastructure.Data;

/// <summary>
/// Implementation of IDapperContext for Dapper queries
/// </summary>
public class DapperContext(IConfiguration configuration) : IDapperContext
{
    /// <summary>
    /// Connection string for the database
    /// </summary>
    private readonly string _connectionString = configuration.GetConnectionString("Products")
            ?? throw new InvalidOperationException("Connection string 'Products' not found.");


    /// <summary>
    /// Creates and returns a new database connection.
    /// </summary>
    /// <returns>Connection instance</returns>
    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}