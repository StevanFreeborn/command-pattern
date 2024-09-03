namespace ShoppingCart.Interfaces;

interface IShoppingCartRepository
{
  Task AddAsync(Product product);
  Task IncreaseQuantityAsync(string productId, int quantity = 1);
  Task DecreaseQuantityAsync(string productId, int quantity = 1);
  Task<List<ShoppingCartItem>> GetCartAsync();
  Task RemoveAsync(Product product);
}