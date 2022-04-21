
using Autofac;
using MikyM.Common.Utilities;

namespace MikyM.Common.Application;

/// <summary>
/// Registration extension configuration
/// </summary>
public sealed class ApplicationConfiguration
{
    internal ContainerBuilder Builder { get; set; }

    internal  ApplicationConfiguration(ContainerBuilder builder)
    {
        this.Builder = builder;
    }

    /// <summary>
    /// Registers an interceptor with <see cref="ContainerBuilder"/>
    /// </summary>
    /// <param name="factoryMethod">Factory method for the registration</param>
    /// <returns>Current instance of the <see cref="ApplicationConfiguration"/></returns>
    public ApplicationConfiguration AddInterceptor<T>(Func<IComponentContext, T> factoryMethod) where T : notnull
    {
        Builder.Register(factoryMethod);

        return this;
    }

    /// <summary>
    /// Registers an async executor with the container
    /// </summary>
    /// <returns>Current instance of the <see cref="ApplicationConfiguration"/></returns>
    public ApplicationConfiguration AddAsyncExecutor()
    {
        Builder.AddAsyncExecutor();
        return this;
    }
}
