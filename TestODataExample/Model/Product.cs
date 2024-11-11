using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestODataExample.Model;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDelete { get; set; }
    public int OrderId { get; set; }
    
    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; }
}

public class Order
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDelete { get; set; }
    public DateTime DateCreated { get; set; }
    
    public ICollection<Product> Products { get; set; } = new List<Product>();
}