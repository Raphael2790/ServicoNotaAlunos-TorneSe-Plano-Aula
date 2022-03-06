using TorneSe.ServicoNotaAluno.Data.Sqs.Messages;
using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients.Interfaces;
using TorneSe.ServicoNotaAluno.Domain.Messages;
using TorneSe.ServicoNotaAluno.Domain.Notification;

namespace TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients;

public class LancarNotaAlunoFakeClient : SqsClient<RegistrarNotaAluno>, ILancarNotaAlunoFakeClient
{
    private readonly Queue<QueueMessage<RegistrarNotaAluno>> _filaNotasParaRegistrar;
    private readonly NotificationContext _notificationContext;

    public LancarNotaAlunoFakeClient(NotificationContext notificationContext)
    {
        _filaNotasParaRegistrar = NotasParaProcessar();
        _notificationContext = notificationContext;
    }

    public override async Task<QueueMessage<RegistrarNotaAluno>> GetMessageAsync()
    {
        QueueMessage<RegistrarNotaAluno> message = default(QueueMessage<RegistrarNotaAluno>);

        try
        {
            message = _filaNotasParaRegistrar.FirstOrDefault();
        } catch(Exception ex)
        {
            _notificationContext.Add(ex.Message);
        }

        return await Task.FromResult(message);
    }

    public override async Task DeleteMessageAsync(string messageHandle)
    {
        await Task.FromResult(_filaNotasParaRegistrar.Dequeue());
    }

    private Queue<QueueMessage<RegistrarNotaAluno>> NotasParaProcessar()
    {
        var queue = new Queue<QueueMessage<RegistrarNotaAluno>>();
        var lista = new List<QueueMessage<RegistrarNotaAluno>>();

        lista.Add(
            new()
            {
                MessageId = Guid.NewGuid().ToString(),
                MessageHandle = Guid.NewGuid().ToString(),
                ReceiveCount = 0,
                MessageBody = new()
                {
                    AlunoId = 1234,
                    AtividadeId = 34545,
                    CorrelationId = Guid.NewGuid().ToString(),
                    ProfessorId = 1282727,
                    ValorNota = 10,
                    NotaSubstitutiva = false
                }
            }
        );

        lista.Add(new()
            {
                MessageId = Guid.NewGuid().ToString(),
                MessageHandle = Guid.NewGuid().ToString(),
                ReceiveCount = 0,
                MessageBody = new()
                {
                    AlunoId = 1234,
                    AtividadeId = 1,
                    CorrelationId = Guid.NewGuid().ToString(),
                    ProfessorId = 1282727,
                    ValorNota = 10,
                    NotaSubstitutiva = false
                }
            });
        
        queue.Enqueue(lista[new Random().Next(0,lista.Count)]);

        return queue;
    }
}
