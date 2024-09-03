namespace ShoppingCart.Repositories;

class ProductsRepository : IProductsRepository
{
  private Dictionary<string, (Product Product, int Stock)> Products { get; } = new()
  {
    ["EOSR1"] = (new Product("Canon EOS R", 1099), 10),
    ["EOS70D"] = (new Product("Canon EOS 70D", 699), 20),
    ["ATOMOSNV"] = (new Product("Atomos Ninja V", 799), 30),
    ["SM7B"] = (new Product("Shure SM7B", 399), 40),
  };

  public Task AddAsync(Product product, int stock) =>  Products.TryAdd(product.Id, (product, stock)) 
    ? Task.CompletedTask 
    : Task.FromException(new Exception("Product already exists"));
  
  public Task<Product> FindByCodeAsync(string code) => Products.TryGetValue(code, out var product) 
    ? Task.FromResult(product.Product) 
    : Task.FromException<Product>(new Exception("Product not found"));

  public Task DecreaseStockAsync(string code, int quantity)
  {
    if (Products.TryGetValue(code, out var product) is false)
    {
      return Task.FromException(new Exception("Product not found"));
    }

    if (product.Stock < quantity)
    {
      return Task.FromException(new Exception("Not enough stock"));
    }

    product.Stock -= quantity;
    return Task.CompletedTask;
  }

  public Task IncreaseStockAsync(string code, int quantity)
  {
    if (Products.TryGetValue(code, out var product) is false)
    {
      return Task.FromException(new Exception("Product not found"));
    }

    product.Stock += quantity;
    return Task.CompletedTask;
  }

  public Task<IEnumerable<Product>> AllAsync() => Task.FromResult(Products.Values.Select(p => p.Product));

  public Task<int> GetStockForProductAsync(string code) => Products.TryGetValue(code, out var product) 
    ? Task.FromResult(product.Stock) 
    : Task.FromException<int>(new Exception("Product not found"));
}