
using TorneSe.ServicoNotaAluno.Data.Sqs.Messages;
using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients.Interfaces;

namespace TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients;

public abstract class SqsClient<T> : IQueueClient<T>
{
    public virtual Task DeleteMessageAsync(string messageHandle)
    {
        throw new NotImplementedException();
    }

    public virtual Task<QueueMessage<T>> GetMessageAsync()
    {
        throw new NotImplementedException();
    }

    public virtual Task<List<QueueMessage<T>>> GetMessageAsync(int count)
    {
        throw new NotImplementedException();
    }

    public virtual Task SendMessageAsync(T message)
    {
        throw new NotImplementedException();
    }

    public virtual Task SendMessageAsync(List<T> messageList)
    {
        throw new NotImplementedException();
    }
}
