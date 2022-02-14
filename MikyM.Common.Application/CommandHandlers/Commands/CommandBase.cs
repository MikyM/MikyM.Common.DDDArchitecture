using System.Text.Json;

namespace MikyM.Common.Application.CommandHandlers.Commands;

/// <summary>
/// Base command implementation
/// </summary>
public abstract class CommandBase : ICommand
{
    public override string ToString()
        => JsonSerializer.Serialize(this);
}

/// <summary>
/// Base command implementation
/// </summary>
/// <typeparam name="TResult">The type of the result of this command</typeparam>
public abstract class CommandBase<TResult> : ICommand<TResult>
{
    public override string ToString()
        => JsonSerializer.Serialize(this);
}
