using System.ComponentModel.DataAnnotations;

namespace Project.DTOs;

public class ProductCreateDto
{
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
}