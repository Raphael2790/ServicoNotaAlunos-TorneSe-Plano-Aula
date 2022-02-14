using TorneSe.ServicoNotaAluno.IOC;
using TorneSe.ServicoNotaAluno.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.AddEnvironmentVariables();
    })
    .ConfigureServices(services =>
    {
        services.ConfigureDependencyInjection()
                .AddHostedService<ServicoNotaAlunoWorker>();
    })
    .Build();

await host.RunAsync();
