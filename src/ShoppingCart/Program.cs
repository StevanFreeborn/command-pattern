var shoppingCartRepository = new ShoppingCartRepository();
var productsRepository = new ProductsRepository();

var product = await productsRepository.FindByCodeAsync("SM7B");
product.Id = "SM7B";

var addToCartCommand = new AddToCartCommand(shoppingCartRepository, productsRepository, product);
var increaseQuantityCommand = new ChangeQuantityCommand(shoppingCartRepository, productsRepository, product, Operation.Increase);

var manager = new CommandManager();
await manager.InvokeAsync(addToCartCommand);
await manager.InvokeAsync(increaseQuantityCommand);

var cart = await shoppingCartRepository.GetCartAsync();

foreach (var item in cart)
{
  Console.WriteLine($"{item.Product.Name} - {item.Quantity}");
}