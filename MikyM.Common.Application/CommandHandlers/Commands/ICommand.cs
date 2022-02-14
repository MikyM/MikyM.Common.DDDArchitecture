namespace MikyM.Common.Application.CommandHandlers.Commands;

/// <summary>
/// Base command marker interface
/// </summary>
public interface ICommand
{
    
}

/// <summary>
/// Marker interface, used only internally
/// </summary>
public interface IResultCommand : ICommand
{

}

/// <summary>
/// Base command with a result marker interface
/// </summary>
public interface ICommand<TResult> : IResultCommand
{
}
