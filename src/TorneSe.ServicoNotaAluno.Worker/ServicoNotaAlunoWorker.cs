using TorneSe.ServicoNotaAluno.Application.Interfaces;
using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients.Interfaces;
using TorneSe.ServicoNotaAluno.Domain.Notification;
using TorneSe.ServicoNotaAluno.Domain.Utils;

namespace TorneSe.ServicoNotaAluno.Worker;

public class ServicoNotaAlunoWorker : BackgroundService
{
    private readonly ILogger<ServicoNotaAlunoWorker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ServicoNotaAlunoWorker(ILogger<ServicoNotaAlunoWorker> logger, 
                                  IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
                _logger.LogInformation($"Iniciando o serviço de notas");
                using var scoped = _serviceScopeFactory.CreateScope();
                var notaAlunoAppService = scoped.ServiceProvider.GetRequiredService<INotaAlunoApplicationService>();
                var messageClient = scoped.ServiceProvider.GetRequiredService<ILancarNotaAlunoFakeClient>();
                var notificationContext = scoped.ServiceProvider.GetRequiredService<NotificationContext>();

                var message = await messageClient.GetMessageAsync();

                if(notificationContext.HasNotifications)
                {
                    _logger.LogError(notificationContext.ToJson());
                    continue;
                }

                if(message is null)
                {
                    _logger.LogInformation(Constants.ApplicationMessages.SEM_MENSAGENS_NA_FILA);
                    continue;
                }

                await notaAlunoAppService.LancarNota(message.MessageBody);
            
        }
    }
}
