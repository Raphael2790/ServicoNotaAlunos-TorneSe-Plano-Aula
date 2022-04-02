using Amazon.SQS;

namespace TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Context.Interfaces;

public interface ISqsContext
{
        AmazonSQSClient Sqs { get; }
        int WaitTimeSeconds { get; }
        string GetQueueUrl(string queueVariable);
}
