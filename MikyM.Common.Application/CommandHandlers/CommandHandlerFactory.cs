using System.Collections.Concurrent;
using Autofac;
using MikyM.Common.Application.CommandHandlers.Commands;

namespace MikyM.Common.Application.CommandHandlers;

/// <summary>
/// Command handler factory
/// </summary>
public interface ICommandHandlerFactory
{
    /// <summary>
    /// Gets a <see cref="ICommandHandler"/> of a given type
    /// </summary>
    /// <typeparam name="TCommandHandler">Type of the <see cref="ICommandHandler"/> to get</typeparam>
    /// <returns>Wanted <see cref="ICommandHandler"/></returns>
    TCommandHandler GetHandler<TCommandHandler>() where TCommandHandler : class, ICommandHandler;

    /// <summary>
    /// Gets a <see cref="ICommandHandler"/> for a given <see cref="ICommand{TResult}"/>
    /// </summary>
    /// <typeparam name="TCommand">Type of the <see cref="ICommand{TResult}"/></typeparam>
    /// <typeparam name="TResult">Type of the command result</typeparam>
    /// <returns>Wanted <see cref="ICommandHandler"/></returns>
    ICommandHandler<TCommand, TResult> GetHandlerFor<TCommand, TResult>() where TCommand : class, ICommand<TResult>;
    /// <summary>
    /// Gets a <see cref="ICommandHandler"/> for a given <see cref="ICommand"/>
    /// </summary>
    /// <typeparam name="TCommand">Type of the <see cref="ICommand"/></typeparam>
    /// <returns>Wanted <see cref="ICommandHandler"/></returns>
    ICommandHandler<TCommand> GetHandlerFor<TCommand>() where TCommand : class, ICommand;
}

/// <inheritdoc cref="ICommandHandlerFactory"/>
public class CommandHandlerFactory : ICommandHandlerFactory
{
    private ConcurrentDictionary<string, ICommandHandler>? _commandHandlers;
    private readonly ILifetimeScope _lifetimeScope;

    public CommandHandlerFactory(ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope;
    }

    public TCommandHandler GetHandler<TCommandHandler>() where TCommandHandler : class, ICommandHandler
    {
        if (!typeof(TCommandHandler).IsInterface)
            throw new ArgumentException("Due to Autofac limitations you must use interfaces");

        _commandHandlers ??= new ConcurrentDictionary<string, ICommandHandler>();

        var type = typeof(TCommandHandler);
        string name = type.FullName ?? throw new InvalidOperationException();

        if (_commandHandlers.TryGetValue(name, out var handler)) 
            return (TCommandHandler) handler;

        var other = _commandHandlers.Values.FirstOrDefault(x => x.GetType().IsAssignableTo(type));
        if (other is not null)
            return (TCommandHandler) other;

        if (_commandHandlers.TryAdd(name, _lifetimeScope.Resolve<TCommandHandler>()))
            return (TCommandHandler)_commandHandlers[name];

        if (_commandHandlers.TryGetValue(name, out handler))
            return (TCommandHandler)handler;

        throw new InvalidOperationException($"Couldn't add nor retrieve handler of type {name}");
    }

    public ICommandHandler<TCommand> GetHandlerFor<TCommand>() where TCommand : class, ICommand
    {
        _commandHandlers ??= new ConcurrentDictionary<string, ICommandHandler>();

        var commandType = typeof(TCommand);
        string name = commandType.FullName ?? throw new InvalidOperationException();

        var generic = typeof(ICommandHandler<,>).MakeGenericType(commandType);
        string genericName = generic.FullName ?? throw new InvalidOperationException();

        if (_commandHandlers.TryGetValue(genericName, out var handler)) 
            return (ICommandHandler<TCommand>) handler;

        if (_commandHandlers.TryAdd(genericName, _lifetimeScope.Resolve<ICommandHandler<TCommand>>()))
            return (ICommandHandler<TCommand>)_commandHandlers[genericName];

        if (_commandHandlers.TryGetValue(name, out handler))
            return (ICommandHandler<TCommand>)handler;

        throw new InvalidOperationException($"Couldn't add nor retrieve handler for type {name}");
    }

    public ICommandHandler<TCommand ,TResult> GetHandlerFor<TCommand, TResult>() where TCommand : class, ICommand<TResult>
    {
        _commandHandlers ??= new ConcurrentDictionary<string, ICommandHandler>();

        var commandType = typeof(TCommand);
        var resultType = typeof(TResult);
        string name = commandType.FullName ?? throw new InvalidOperationException();

        var generic = typeof(ICommandHandler<,>).MakeGenericType(commandType, resultType);
        string genericName = generic.FullName ?? throw new InvalidOperationException();

        if (_commandHandlers.TryGetValue(genericName, out var handler)) 
            return (ICommandHandler<TCommand, TResult>) handler;

        if (_commandHandlers.TryAdd(genericName, _lifetimeScope.Resolve<ICommandHandler<TCommand, TResult>>()))
            return (ICommandHandler<TCommand, TResult>) _commandHandlers[genericName];

        if (_commandHandlers.TryGetValue(name, out handler))
            return (ICommandHandler<TCommand, TResult>)handler;

        throw new InvalidOperationException($"Couldn't add nor retrieve handler for type {name}");
    }
}