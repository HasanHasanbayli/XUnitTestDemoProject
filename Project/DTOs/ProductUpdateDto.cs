namespace Project.DTOs;

public class ProductUpdateDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
}