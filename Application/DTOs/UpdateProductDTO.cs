using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Application.DTOs
{
    public class UpdateProductDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [MaxLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0.01, 1000000, ErrorMessage = "Price must be positive and less than 10 lakh.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int Stock { get; set; }
    }
}