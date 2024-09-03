namespace ShoppingCart.Interfaces;

interface IProductsRepository
{
  Task AddAsync(Product product, int stock);
  Task<Product> FindByCodeAsync(string code);
  Task DecreaseStockAsync(string code, int quantity);
  Task IncreaseStockAsync(string code, int quantity);
  Task<IEnumerable<Product>> AllAsync();
  Task<int> GetStockForProductAsync(string code);
}