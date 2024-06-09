using System.ComponentModel.DataAnnotations;

namespace item.Web.ViewModels.Product;

public class UpdateProductViewModel
{
    public int id { get; set; }
    [Required]
    public string Title { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; }
}
