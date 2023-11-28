using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SportsStore.Models;

public class Product
{
    public int ProductID { get; set; }

    [Required(ErrorMessage = "Please enter a product name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please enter a description")]
    public string Description { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Please select a category")]
    [DisplayName("Category")]
    public int CategoryID { get; set; }

    public Category Category { get; set; }
}
