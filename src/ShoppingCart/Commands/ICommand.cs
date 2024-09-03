namespace ShoppingCart.Commands;

interface ICommand
{
  Task ExecuteAsync();
  Task<bool> CanExecuteAsync();
  Task UndoAsync();
}