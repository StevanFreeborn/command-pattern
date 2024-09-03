
namespace ShoppingCart.Commands;

class AddToCartCommand(
  IShoppingCartRepository shoppingCartRepository, 
  IProductsRepository productRepository,
  Product product
) : ICommand
{
  private readonly IShoppingCartRepository _shoppingCartRepository = shoppingCartRepository;
  private readonly IProductsRepository _productRepository = productRepository;
  private readonly Product _product = product;

  public async Task<bool> CanExecuteAsync()
  {
    return _product is not null && await _productRepository.GetStockForProductAsync(_product.Id) > 0;
  }

  public async Task ExecuteAsync()
  {
    if (_product is not null)
    {
      await _productRepository.DecreaseStockAsync(_product.Id, 1);
      await _shoppingCartRepository.AddAsync(_product);
    }
  }

  public async Task UndoAsync()
  {
    if (_product is not null)
    {
      var items = await _shoppingCartRepository.GetCartAsync();
      var item = items.FirstOrDefault(p => p.Product.Id == _product.Id);

      if (item is null)
      {
        return;
      }

      await _productRepository.IncreaseStockAsync(_product.Id, item.Quantity);
      await _shoppingCartRepository.RemoveAsync(_product);
    }
  }
}