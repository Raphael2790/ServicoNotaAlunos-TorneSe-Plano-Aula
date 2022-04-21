using TorneSe.ServicoNotaAluno.Data.Context;
using TorneSe.ServicoNotaAluno.IOC;
using TorneSe.ServicoNotaAluno.Worker;
using Serilog;
using TorneSe.ServicoNotaAluno.IOC.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.SetBasePath(hostContext.HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

        hostContext.Configuration = config.Build();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.ConfigureDependencyInjection()
                .AddHostedService<ServicoNotaAlunoWorker>()
                .AddNpgsql<NotaAlunoDbContext>(hostContext.Configuration.GetConnectionString("DefaultConnection")
        , opt =>
                {
                    opt.CommandTimeout(10);
                    opt.EnableRetryOnFailure(4, TimeSpan.FromSeconds(10), null);
                })
                .ConfigureSerilog(hostContext.Configuration, hostContext.HostingEnvironment);
    })
    .UseSerilog()
    .Build();

await host.RunAsync();
