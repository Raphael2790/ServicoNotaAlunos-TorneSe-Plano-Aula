using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients.Interfaces;
using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Context.Interfaces;
using TorneSe.ServicoNotaAluno.Domain.Messages;
using TorneSe.ServicoNotaAluno.Domain.Notification;

namespace TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients;

public class LancarNotaAlunoReceiveClient : SqsClient<RegistrarNotaAluno>, ILancarNotaAlunoReceiveClient
{
    const string SQS_RECEIVE_QUEUE_NAME = "ReceiveMessageQueue";
    public LancarNotaAlunoReceiveClient(ISqsContext sqsContext, 
                                        NotificationContext notificationContext) 
                    : base(sqsContext, notificationContext, SQS_RECEIVE_QUEUE_NAME)
    {
    }
}
