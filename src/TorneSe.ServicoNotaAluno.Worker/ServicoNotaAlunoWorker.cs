using TorneSe.ServicoNotaAluno.Application.Interfaces;

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

                await notaAlunoAppService.LancarNota();
                _logger.LogInformation($"Finalizando o serviço de notas");
        }
    }
}
