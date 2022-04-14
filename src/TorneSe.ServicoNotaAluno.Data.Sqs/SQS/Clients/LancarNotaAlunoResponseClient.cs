using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients.Interfaces;
using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Context.Interfaces;
using TorneSe.ServicoNotaAluno.Domain.Messages;
using TorneSe.ServicoNotaAluno.Domain.Notification;

namespace TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients
{
    public class LancarNotaAlunoResponseClient : SqsClient<NotaAlunoRegistrada>, ILancarNotaAlunoResponseClient
    {
        const string SQS_RESPONSE_QUEUE_NAME = "ResponseMessageQueue";
        public LancarNotaAlunoResponseClient(ISqsContext sqsContext, 
                                        NotificationContext notificationContext) : 
                                        base(sqsContext, notificationContext, SQS_RESPONSE_QUEUE_NAME)
        {
        }
    }
}