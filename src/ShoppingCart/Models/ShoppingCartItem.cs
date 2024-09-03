namespace ShoppingCart.Models;

class ShoppingCartItem(Product product, int quantity)
{
  public Product Product { get; set; } = product;
  public int Quantity { get; set; } = quantity;
}