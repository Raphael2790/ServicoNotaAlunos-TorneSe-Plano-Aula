using TorneSe.ServicoNotaAluno.Data.Sqs.Messages;

namespace TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients.Interfaces
{
    public interface IQueueClient<T>
    {
        Task SendMessageAsync(T message);
        Task SendMessageAsync(List<T> messageList);
        Task<QueueMessage<T>> GetMessageAsync();
        Task<List<QueueMessage<T>>> GetMessageAsync(int count);
        Task  DeleteMessageAsync(string messageHandle);
    }
}