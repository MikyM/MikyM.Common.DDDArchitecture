using Autofac;
using MikyM.Common.Application.Interfaces;
using MikyM.Common.Application.Services;
using MikyM.Common.DataAccessLayer.UnitOfWork;

namespace MikyM.Common.Application.Extensions
{
    /// <summary>
    /// Extension methods for setting up unit of work related services in a <see cref="ContainerBuilder"/>.
    /// </summary>
    public static class AutofacExtensions
    {
        /// <summary>
        /// Registers generic services in the <see cref="ContainerBuilder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="ContainerBuilder"/> to add services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        /// <remarks>
        /// This method only support one db context, if been called more than once, will throw exception.
        /// </remarks>
        public static ContainerBuilder AddGenericServices(this ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ReadOnlyService<,,>)).As(typeof(IReadOnlyService<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(CrudService<,,>)).As(typeof(ICrudService<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ServiceBase<>)).As(typeof(IServiceBase)).InstancePerLifetimeScope();
            return builder;
        }
    }
}
