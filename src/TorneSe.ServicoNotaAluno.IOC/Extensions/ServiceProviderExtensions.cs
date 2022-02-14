namespace TorneSe.ServicoNotaAluno.IOC.Extensions;

public static class ServiceProviderExtensions
{
    public static  List<object> GetServices(this IServiceProvider provider,Type[] implemetations)
    {
        var services = new List<object>(implemetations.Count());

        foreach(var implementation in implemetations)
            services.Add(provider.GetService(implementation));

        return services;
    }
}
