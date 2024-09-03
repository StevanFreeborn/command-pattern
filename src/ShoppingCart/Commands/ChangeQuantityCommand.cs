
namespace ShoppingCart.Commands;

class ChangeQuantityCommand(
  IShoppingCartRepository shoppingCartRepository,
  IProductsRepository productRepository,
  Product product,
  Operation operation
  ) : ICommand
{
  private readonly Operation _operation = operation;
  private readonly IShoppingCartRepository _shoppingCartRepository = shoppingCartRepository;
  private readonly IProductsRepository _productRepository = productRepository;
  private readonly Product _product = product;

  public async Task<bool> CanExecuteAsync()
  {
    switch (_operation)
    {
      case Operation.Increase:
        return await _productRepository.GetStockForProductAsync(_product.Id) - 1 >= 0;
      case Operation.Decrease:
        var items = await _shoppingCartRepository.GetCartAsync();
        var item = items.FirstOrDefault(p => p.Product.Id == _product.Id);
        return item is not null && item.Quantity is not 0;
      default:
        throw new InvalidOperationException("Invalid operation");
    }
  }

  public async Task ExecuteAsync()
  {
    switch (_operation)
    {
      case Operation.Increase:
        await _productRepository.DecreaseStockAsync(_product.Id, 1);
        await _shoppingCartRepository.IncreaseQuantityAsync(_product.Id);
        break;
      case Operation.Decrease:
        await _productRepository.IncreaseStockAsync(_product.Id, 1);
        await _shoppingCartRepository.DecreaseQuantityAsync(_product.Id);
        break;
      default:
        throw new InvalidOperationException("Invalid operation");
    }
  }

  public async Task UndoAsync()
  {
    switch (_operation)
    {
      case Operation.Increase:
        var items = await _shoppingCartRepository.GetCartAsync();
        var item = items.FirstOrDefault(p => p.Product.Id == _product.Id);

        if (item is null)
        {
          return;
        }

        await _productRepository.IncreaseStockAsync(_product.Id, 1);
        await _shoppingCartRepository.DecreaseQuantityAsync(_product.Id);
        break;
      case Operation.Decrease:
        await _productRepository.DecreaseStockAsync(_product.Id, 1);
        await _shoppingCartRepository.IncreaseQuantityAsync(_product.Id);
        break;
      default:
        throw new InvalidOperationException("Invalid operation");
    }
  }
}

enum Operation
{
  Increase,
  Decrease
}