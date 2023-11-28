using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SportsStore.Models;

public class Category
{
    public int CategoryID { get; set; }

    [Required]
    [DisplayName("Category")]
    public string CategoryName { get; set; }

    public ICollection<Product> Products { get; set; }
}
