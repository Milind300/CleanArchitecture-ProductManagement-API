using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Application.Interfaces.Services;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        private ProductDTO MapToDTO(Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            var dtos = new List<ProductDTO>();
            foreach (var product in products)
            {
                dtos.Add(MapToDTO(product));
            }
            return dtos;
        }

        public async Task<ProductDTO?> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return null;
            return MapToDTO(product);
        }

        public async Task<ProductDTO?> AddProductAsync(CreateProductDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock
            };
            product.ValidateBusinessRules();

            var newProduct = await _repository.AddAsync(product);
            if (newProduct != null)
                return MapToDTO(newProduct);

            return null;
        }

        public async Task UpdateProductAsync(UpdateProductDTO productDto)
        {
            var product = await _repository.GetByIdAsync(productDto.Id);
            if (product == null) throw new Exception("Product not found.");
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.Stock = productDto.Stock;
            product.ValidateBusinessRules();

            await _repository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}