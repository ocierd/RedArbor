using System.Data;

namespace RedArbor.Application.Common.Interfaces;

/// <summary>
/// Provides an IDbConnection instance for Dapper queries.
/// </summary>
public interface IDapperContext
{
    /// <summary>
    /// Creates and returns a new database connection.
    /// </summary>
    /// <returns>Connection instance</returns>
    IDbConnection CreateConnection();
}