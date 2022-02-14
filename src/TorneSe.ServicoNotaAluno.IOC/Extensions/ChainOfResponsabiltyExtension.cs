using Microsoft.Extensions.DependencyInjection;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;
using TorneSe.ServicoNotaAluno.Domain.Validations.Handlers.Interfaces;

namespace TorneSe.ServicoNotaAluno.IOC.Extensions;

public static class ChainOfResponsabiltyExtension
{
    public static IServiceCollection AddChainedAsync<TService,TRequest>(this IServiceCollection services, params Type[] implementationTypes)
    where TService : IAsyncHandler<TRequest>
    where TRequest : IRequest
    {
        if (implementationTypes.Length == 0)
        {
            throw new ArgumentException("Pass at least one implementation type", nameof(implementationTypes));
        }

        foreach(Type type in implementationTypes)
            services.AddScoped(type);
        

        services.AddTransient(typeof(TService), provider =>
        {
            var services = provider.GetServices(implementationTypes);

            for (int i = 0; i < services.Count; i++)
            {
                if(services.Count > i + 1)
                    ((IAsyncHandler<TRequest>)services[i]).SetNext((IAsyncHandler<TRequest>)services[i + 1]);
            }

            return services.First();
        });

        return services;
    }

    public static IServiceCollection AddChained<TService,TRequest>(this IServiceCollection services, params Type[] implementationTypes)
    {
        if (implementationTypes.Length == 0)
        {
            throw new ArgumentException("Pass at least one implementation type", nameof(implementationTypes));
        }

        foreach(Type type in implementationTypes)
            services.AddScoped(type);
        

        services.AddTransient(typeof(TService), provider =>
        {
            var services = provider.GetServices(implementationTypes);

            for (int i = 0; i < services.Count; i++)
            {
                if(services.Count > i + 1)
                    ((IHandler<TRequest>)services[i]).SetNext((IHandler<TRequest>)services[i + 1]);
            }

            return services.First();
        });

        return services;
    }
}
