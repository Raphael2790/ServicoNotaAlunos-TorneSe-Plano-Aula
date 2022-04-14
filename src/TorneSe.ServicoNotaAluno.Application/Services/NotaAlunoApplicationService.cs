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

    public NotaAlunoApplicationService(INotaAlunoService notaAlunoService,
                                        IUnitOfWork uow,
                                        INotaAlunoRequestService notaAlunoRequestService,
                                        INotaAlunoResponseService notaAlunoResponseService, 
                                        NotificationContext notificationContext)
    {
        _notaAlunoService = notaAlunoService;
        _uow = uow;
        _notaAlunoRequestService = notaAlunoRequestService;
        _notaAlunoResponseService = notaAlunoResponseService;
        _notificationContext = notificationContext;
    }
    public async Task LancarNota()
    {
        try
        {
            Console.WriteLine("Orquestrando o fluxo da aplicação");

            var mensagem = await _notaAlunoRequestService.BuscarMensagem();

            if(_notificationContext.HasNotifications)
            {
                System.Console.WriteLine(_notificationContext.ToJson());
                return;
            }

            await _notaAlunoService.LancarNotaAluno(mensagem.MessageBody);

            if(_notificationContext.HasNotifications)
            {
                System.Console.WriteLine(_notificationContext.ToJson());
                return;
            }

            if(!await _uow.Commit())
                _notificationContext.Add(Constants.ApplicationMessages.FALHA_NA_PERSISTENCIA);

            await _notaAlunoRequestService.DeletarMensagem(mensagem.MessageHandle);
            await _notaAlunoResponseService.Enviar(mensagem.MessageBody);

            if(_notificationContext.HasNotifications)
                System.Console.WriteLine(_notificationContext.ToJson());
            else
                System.Console.WriteLine($"Mensagem correlationId {mensagem.MessageBody.CorrelationId} processada com sucesso!");
        }
        catch (Exception ex)
        {
             System.Console.WriteLine(ex.Message);
        }
    }
}
