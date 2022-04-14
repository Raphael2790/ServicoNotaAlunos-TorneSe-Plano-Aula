using TorneSe.ServicoNotaAluno.Application.Interfaces;
using TorneSe.ServicoNotaAluno.Domain.Messages;
using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients.Interfaces;
using TorneSe.ServicoNotaAluno.Domain.Notification;
using TorneSe.ServicoNotaAluno.Domain.Utils;
using TorneSe.ServicoNotaAluno.Data.Sqs.Messages;

namespace TorneSe.ServicoNotaAluno.Application.Services;

public class NotaAlunoRequestService : INotaAlunoRequestService
{
    private readonly ILancarNotaAlunoReceiveClient _receiveClient;
    private readonly NotificationContext _notificationContext;

    public NotaAlunoRequestService(ILancarNotaAlunoReceiveClient receiveClient
                                    ,NotificationContext notificationContext)
    {
        _receiveClient = receiveClient;
        _notificationContext = notificationContext;
    }

    public async Task<QueueMessage<RegistrarNotaAluno>> BuscarMensagem()
    {
        var message = await _receiveClient.GetMessageAsync();

        if(_notificationContext.HasNotifications)
            return default;
        

        if(message is null)
        {
            _notificationContext.Add(Constants.ApplicationMessages.SEM_MENSAGENS_NA_FILA);
            return default;
        }

        return message;
    }

    public async Task DeletarMensagem(string messageHandle) =>
        await _receiveClient.DeleteMessageAsync(messageHandle);
    
}
