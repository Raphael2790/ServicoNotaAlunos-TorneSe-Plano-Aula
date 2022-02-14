namespace TorneSe.ServicoNotaAluno.Worker;

public class WorkerExemplo : IHostedService, IDisposable
{
    private readonly ILogger<WorkerExemplo> _logger;
    private readonly HttpClient _client;
    private Timer? _timer;

    public WorkerExemplo(ILogger<WorkerExemplo> logger)
    {
        _logger = logger;
        _client = new HttpClient();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Serviço iniciado com sucesso!");
        _timer = new Timer(VerificarSite, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        return Task.CompletedTask;
    }

    private async void VerificarSite(object? state)
    {
        var response = await _client.GetAsync("https://www.google.com.br");
        if(response.IsSuccessStatusCode)
            _logger.LogInformation("O site está OK!");
        else
            _logger.LogError("O site não está legal!");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
         _logger.LogInformation("Serviço está sendo encerrado!");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _client?.Dispose();
    }
}
