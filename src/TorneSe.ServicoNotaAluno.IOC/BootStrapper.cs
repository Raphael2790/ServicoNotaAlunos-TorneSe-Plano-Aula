using Microsoft.Extensions.DependencyInjection;
using TorneSe.ServicoNotaAluno.Application.Interfaces;
using TorneSe.ServicoNotaAluno.Application.Services;
using TorneSe.ServicoNotaAluno.Data.Context;
using TorneSe.ServicoNotaAluno.Data.Repositories;
using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients;
using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients.Interfaces;
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
        RegisterServices(services);
        RegisterRepositories(services);
        RegisterContexts(services);
        RegisterQueues(services);
        RegisterNotificationContext(services);
        RegisterChains(services);
        RegisterUnitOfWork(services);
        return services;
    } 

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<INotaAlunoApplicationService, NotaAlunoApplicationService>();
        services.AddScoped<INotaAlunoService, NotaAlunoService>();
        services.AddScoped<INotaAlunoValidationService, NotaAlunoValidationService>();
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    }

    private static void RegisterContexts(IServiceCollection services)
    {
        services.AddScoped<FakeDbContext>();
    }

    private static void RegisterUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void RegisterQueues(IServiceCollection services)
    {
        services.AddScoped<ILancarNotaAlunoFakeClient, LancarNotaAlunoFakeClient>();
    }

    private static void RegisterNotificationContext(IServiceCollection services)
    {
        services.AddScoped<NotificationContext>();
    }

    private static void RegisterChains(IServiceCollection services)
    {
        services.AddChainedAsync<IAsyncHandler<NotaAlunoValidationRequest>,NotaAlunoValidationRequest>(typeof(AlunoRequestBuildHandler), typeof(ProfessorRequestBuildHandler), typeof(DisciplinaRequestBuildHandler));
        services.AddChained<IHandler<NotaAlunoValidationRequest>, NotaAlunoValidationRequest>(typeof(AlunoValidationHandler), typeof(ProfessorValidationHandler), typeof(DisciplinaValidationHandler));
    }
}
