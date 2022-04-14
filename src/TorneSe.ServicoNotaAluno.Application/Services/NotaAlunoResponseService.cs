using TorneSe.ServicoNotaAluno.Application.Interfaces;
using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients.Interfaces;
using TorneSe.ServicoNotaAluno.Domain.Messages;
using TorneSe.ServicoNotaAluno.Domain.Notification;

namespace TorneSe.ServicoNotaAluno.Application.Services
{
    public class NotaAlunoResponseService : INotaAlunoResponseService
    {
        private readonly ILancarNotaAlunoResponseClient _responseClient;
        private readonly NotificationContext _notificationContext;

        public NotaAlunoResponseService(ILancarNotaAlunoResponseClient responseClient,
                                        NotificationContext notificationContext)
        {
            _responseClient = responseClient;
            _notificationContext = notificationContext;
        }

        public async Task Enviar(RegistrarNotaAluno registrarNotaAluno)
        {
            await _responseClient.SendMessageAsync(new NotaAlunoRegistrada
            {
                AlunoId = registrarNotaAluno.AlunoId,
                AtividadeId = registrarNotaAluno.AtividadeId,
                CorrelationId = registrarNotaAluno.CorrelationId,
                PossuiErros = _notificationContext.HasNotifications,
                Erros = _notificationContext
            });
        }
    }
}