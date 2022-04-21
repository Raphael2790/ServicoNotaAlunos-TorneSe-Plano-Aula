using Microsoft.Extensions.Logging;
using TorneSe.ServicoNotaAluno.Application.Interfaces;
using TorneSe.ServicoNotaAluno.Domain.Interfaces.Services;
using TorneSe.ServicoNotaAluno.Domain.Notification;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;
using TorneSe.ServicoNotaAluno.Domain.Utils;

namespace TorneSe.ServicoNotaAluno.Application.Services;

public class NotaAlunoApplicationService : INotaAlunoApplicationService
{
    private readonly INotaAlunoService _notaAlunoService;
    private readonly IUnitOfWork _uow;
    private readonly INotaAlunoRequestService _notaAlunoRequestService;
    private readonly INotaAlunoResponseService _notaAlunoResponseService;
    private readonly NotificationContext _notificationContext;
    private readonly ILogger<NotaAlunoApplicationService> _logger;

    public NotaAlunoApplicationService(INotaAlunoService notaAlunoService,
                                        IUnitOfWork uow,
                                        INotaAlunoRequestService notaAlunoRequestService,
                                        INotaAlunoResponseService notaAlunoResponseService, 
                                        NotificationContext notificationContext,
                                        ILogger<NotaAlunoApplicationService> logger)
    {
        _notaAlunoService = notaAlunoService;
        _uow = uow;
        _notaAlunoRequestService = notaAlunoRequestService;
        _notaAlunoResponseService = notaAlunoResponseService;
        _notificationContext = notificationContext;
        _logger = logger;
    }
    public async Task LancarNota()
    {
        try
        {
            _logger.LogInformation($"Orquestrando o fluxo da aplicação - Data{DateTime.Now.ToString()}");

            var mensagem = await _notaAlunoRequestService.BuscarMensagem();

            if(_notificationContext.HasNotifications)
            {
                _logger.LogWarning(_notificationContext.ToJson());
                return;
            }

            _logger.LogInformation($"Mensagem recebida @Mensagem", mensagem);

            await _notaAlunoService.LancarNotaAluno(mensagem.MessageBody);

            if(_notificationContext.HasNotifications)
            {
                _logger.LogWarning(_notificationContext.ToJson());
                return;
            }

            if(!await _uow.Commit())
                _notificationContext.Add(Constants.ApplicationMessages.FALHA_NA_PERSISTENCIA);

            await _notaAlunoRequestService.DeletarMensagem(mensagem.MessageHandle);
            await _notaAlunoResponseService.Enviar(mensagem.MessageBody);

            if(_notificationContext.HasNotifications)
                _logger.LogWarning(_notificationContext.ToJson());
            else
                _logger.LogInformation($"Mensagem correlationId {mensagem.MessageBody.CorrelationId} processada com sucesso!");
        }
        catch (Exception ex)
        {
             _logger.LogError(ex.Message);
        }
    }
}
