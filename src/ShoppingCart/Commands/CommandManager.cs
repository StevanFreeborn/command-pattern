namespace ShoppingCart.Commands;

class CommandManager
{
  private readonly Stack<ICommand> _commands = new();

  public async Task InvokeAsync(ICommand command)
  {
    if (await command.CanExecuteAsync())
    {
      _commands.Push(command);
      await command.ExecuteAsync();
    }
  }

  public async Task UndoAsync()
  {
    while (_commands.Count > 0)
    {
      var command = _commands.Pop();
      await command.UndoAsync();
    }
  }
}