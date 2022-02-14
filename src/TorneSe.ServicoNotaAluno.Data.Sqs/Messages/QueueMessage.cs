namespace TorneSe.ServicoNotaAluno.Data.Sqs.Messages;

public class QueueMessage<T>
{
    public string MessageId { get; set; }
    public string MessageHandle { get; set; }
    public T MessageBody { get; set; }
    public int ReceiveCount { get; set; }
}
