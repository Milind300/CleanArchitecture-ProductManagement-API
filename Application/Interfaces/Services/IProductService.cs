using ProductManagement.Application.DTOs;
namespace ProductManagement.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(int id);
        Task<ProductDTO?> AddProductAsync(CreateProductDTO productDto);
        Task UpdateProductAsync(UpdateProductDTO productDto);
        Task DeleteProductAsync(int id);
    }
}