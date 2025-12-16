using System.Threading.Tasks;
using Dapper;
using RedArbor.Application.Common.Interfaces;
using RedArbor.Application.Common.Interfaces.Repository;
using RedArbor.Application.Common.Dto;

namespace RedArbor.Infrastructure.Data.Repository;

/// <summary>
/// Implementation of IProductRepository using Dapper
/// </summary>
/// <param name="context">Dapper context for database connection</param>
public class ProductRepository(IDapperContext context) : IProductRepository
{
    /// <summary>
    /// Adds a new product and returns its Id
    /// </summary>
    /// <param name="productDto">Product data transfer object</param>
    /// <returns>Id of the newly added product</returns>
    public async Task<int> AddProductAsync(ProductDto productDto)
    {
        using var connection = context.CreateConnection();
        string sql = "INSERT INTO Products (name, description, price, category_id) " +
                     "VALUES (@Name, @Description, @Price, @CategoryId); " +
                     "SELECT CAST(SCOPE_IDENTITY() as int);";
        var parameters = new
        {
            productDto.Name,
            productDto.Description,
            productDto.Price,
            productDto.CategoryId
        };
        var productId = await connection.QuerySingleAsync<int>(sql, parameters);
        return productId;
    }

    /// <summary>
    /// Deletes a product by its Id
    /// </summary>
    /// <param name="productId">Id of the product to delete</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public async Task DeleteProductAsync(int productId)
    {
        using var connection = context.CreateConnection();
        string sql = "DELETE FROM products WHERE product_id = @ProductId;";
        var parameters = new { ProductId = productId };
        var affected = await connection.ExecuteAsync(sql, parameters);
    }

    /// <summary>
    /// Updates an existing product
    /// </summary>
    /// <param name="productDto">Product data transfer object</param>
    /// <returns>True if the update was successful, otherwise false</returns>
    public async Task<bool> UpdateProductAsync(ProductDto productDto)
    {
        using var connection = context.CreateConnection();
        string sql = "UPDATE products SET name = @Name, description = @Description, " +
                     "price = @Price, category_id = @CategoryId " +
                     "WHERE product_id = @ProductId;";
        var parameters = new
        {
            productDto.Name,
            productDto.Description,
            productDto.Price,
            productDto.CategoryId,
            productDto.ProductId
        };
        var affected = await connection.ExecuteAsync(sql, parameters);
        return affected > 0;
    }
}
