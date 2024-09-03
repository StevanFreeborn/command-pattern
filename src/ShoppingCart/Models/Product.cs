namespace ShoppingCart.Models;

public class Product
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public string Name { get; set; } = string.Empty;
  public decimal Price { get; set; }

  public Product()
  {
  }

  public Product(string name, decimal price)
  {
    Name = name;
    Price = price;
  }
}