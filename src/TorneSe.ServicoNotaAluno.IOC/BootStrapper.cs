using Microsoft.Extensions.DependencyInjection;
using TorneSe.ServicoNotaAluno.Application.Interfaces;
using TorneSe.ServicoNotaAluno.Application.Services;
using TorneSe.ServicoNotaAluno.Data.Context;
using TorneSe.ServicoNotaAluno.Data.Repositories;
using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients;
using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients.Interfaces;
using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Context;
using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Context.Interfaces;
using TorneSe.ServicoNotaAluno.Data.UnitOfWork;
using TorneSe.ServicoNotaAluno.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAluno.Domain.Interfaces.Services;
using TorneSe.ServicoNotaAluno.Domain.Notification;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;
using TorneSe.ServicoNotaAluno.Domain.Services;
using TorneSe.ServicoNotaAluno.Domain.Validations.Handlers;
using TorneSe.ServicoNotaAluno.Domain.Validations.Handlers.Interfaces;
using TorneSe.ServicoNotaAluno.IOC.Extensions;

namespace TorneSe.ServicoNotaAluno.IOC;

public static class BootStrapper
{
    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
    {
        return services.RegisterServices()
                        .RegisterRepositories()
                        .RegisterContexts()
                        .RegisterQueues()
                        .RegisterNotificationContext()
                        .RegisterChains()
                        .RegisterUnitOfWork()
                        .RegisterSqsContext();
    } 

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<INotaAlunoApplicationService, NotaAlunoApplicationService>();
        services.AddScoped<INotaAlunoService, NotaAlunoService>();
        services.AddScoped<INotaAlunoValidationService, NotaAlunoValidationService>();
        services.AddScoped<INotaAlunoRequestService, NotaAlunoRequestService>();
        services.AddScoped<INotaAlunoResponseService, NotaAlunoResponseService>();
        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        return services;
    }

    private static IServiceCollection RegisterContexts(this IServiceCollection services)
    {
        services.AddScoped<FakeDbContext>();
        return services;
    }

    private static IServiceCollection RegisterUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    private static IServiceCollection RegisterQueues(this IServiceCollection services)
    {
        services.AddScoped<ILancarNotaAlunoFakeClient, LancarNotaAlunoFakeClient>();
        services.AddScoped<ILancarNotaAlunoReceiveClient, LancarNotaAlunoReceiveClient>();
        services.AddScoped<ILancarNotaAlunoResponseClient, LancarNotaAlunoResponseClient>();
        return services;
    }

    private static IServiceCollection RegisterNotificationContext(this IServiceCollection services)
    {
        services.AddScoped<NotificationContext>();
        return services;
    }

    private static IServiceCollection RegisterChains(this IServiceCollection services)
    {
        services.AddChainedAsync<IAsyncHandler<NotaAlunoValidationRequest>,NotaAlunoValidationRequest>(typeof(AlunoRequestBuildHandler), typeof(ProfessorRequestBuildHandler), typeof(DisciplinaRequestBuildHandler));
        services.AddChained<IHandler<NotaAlunoValidationRequest>, NotaAlunoValidationRequest>(typeof(AlunoValidationHandler), typeof(ProfessorValidationHandler), typeof(DisciplinaValidationHandler));
        return services;
    }

    private static IServiceCollection RegisterSqsContext(this IServiceCollection services)
    {
        services.AddSingleton<ISqsContext, SqsContext>();
        return services;
    }
}
