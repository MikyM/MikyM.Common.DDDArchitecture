namespace MikyM.Common.Application.CommandHandlers.Helpers;

/// <summary>
/// Command handler options
/// </summary>
public sealed class CommandHandlerOptions
{

    internal CommandHandlerOptions(ApplicationOptions config)
    {
        Config = config;
    }

    internal ApplicationOptions Config { get; set; }

    /// <summary>
    /// Gets or sets the default lifetime for base generic data services
    /// </summary>
    public Lifetime DefaultLifetime { get; set; } = Lifetime.InstancePerLifetimeScope;
}