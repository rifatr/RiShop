using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CartItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? ProductName { get; set; }

        [Required]
        [Range(1.0 / double.MaxValue, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }

        [Required]
        public string? PictureUrl { get; set; }

        [Required]
        public string? Brand { get; set; }

        [Required]
        public string? Type { get; set; } 
    }
}