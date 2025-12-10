namespace RedArbor.Application.Common.Interfaces.Repository;

/// <summary>
/// Repository interface for product operations
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Adds a new product and returns its Id
    /// </summary>
    /// <param name="productDto">Product data transfer object</param>
    /// <returns>Id of the newly added product</returns>
    Task<int> AddProductAsync(ProductDto productDto);

    /// <summary>
    /// Deletes a product by its Id
    /// </summary>
    /// <param name="productId">Id of the product to delete</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task DeleteProductAsync(int productId);

    /// <summary>
    /// Updates an existing product
    /// </summary>
    /// <param name="productDto">Product data transfer object</param>
    /// <returns>True if the update was successful, otherwise false</returns>
    Task<bool> UpdateProductAsync(ProductDto productDto);
}