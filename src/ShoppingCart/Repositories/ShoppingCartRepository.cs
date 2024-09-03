namespace ShoppingCart.Repositories;

class ShoppingCartRepository : IShoppingCartRepository
{
  private List<ShoppingCartItem> Products { get; } = [];

  public Task AddAsync(Product product)
  {
    var existingEntry = Products.FirstOrDefault(p => p.Product.Id == product.Id);
    
    if (existingEntry is not null)
    {
      existingEntry.Quantity++;
    }
    else
    {
      Products.Add(new ShoppingCartItem(product, 1));
    }

    return Task.CompletedTask;
  }

  public Task DecreaseQuantityAsync(string productId, int quantity = 1)
  {
    var existingEntry = Products.FirstOrDefault(p => p.Product.Id == productId);
    
    if (existingEntry is not null)
    {
      existingEntry.Quantity -= quantity;
    }

    return Task.CompletedTask;
  }

  public Task<List<ShoppingCartItem>> GetCartAsync()
  {
    return Task.FromResult(Products);
  }

  public Task IncreaseQuantityAsync(string productId, int quantity = 1)
  {
    var existingEntry = Products.FirstOrDefault(p => p.Product.Id == productId);
    
    if (existingEntry is not null)
    {
      existingEntry.Quantity += quantity;
    }

    return Task.CompletedTask;
  }

  public Task RemoveAsync(Product product)
  {
    Products.RemoveAll(p => p.Product.Id == product.Id);
    return Task.CompletedTask;
  }
}