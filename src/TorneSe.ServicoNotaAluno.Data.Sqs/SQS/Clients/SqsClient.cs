
using System.Text.Json;
using Amazon.SQS.Model;
using TorneSe.ServicoNotaAluno.Data.Sqs.Messages;
using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients.Interfaces;
using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Context.Interfaces;
using TorneSe.ServicoNotaAluno.Domain.Notification;

namespace TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients;

public abstract class SqsClient<T> : IQueueClient<T>
{
    private readonly ISqsContext _context;
    private readonly NotificationContext _notificationContext;
    private readonly string _queueUrl;

    public SqsClient(ISqsContext sqsContext, 
                    NotificationContext notificationContext, string sqsQueueName)
    {
        _context = sqsContext;
        _notificationContext = notificationContext;
        _queueUrl = sqsContext.GetQueueUrl(sqsQueueName);
    }

    public virtual async Task DeleteMessageAsync(string messageHandle)
    {

        try
        {
            var deleteRequest = new DeleteMessageRequest
            {
                QueueUrl = _queueUrl,
                ReceiptHandle = messageHandle
            };

            await _context.Sqs.DeleteMessageAsync(deleteRequest);
        }
        catch (Exception ex)
        {
            _notificationContext.Add(ex.Message);
        }
    }

    public virtual async Task<QueueMessage<T>> GetMessageAsync()
    {
        var queueResult = await GetMessageAsync(1);

        if (queueResult != null && queueResult.Any())
        {

            return queueResult.First();

        }

        return default;
    }

    public virtual async Task<List<QueueMessage<T>>> GetMessageAsync(int count)
    {
        var list = new List<QueueMessage<T>>();

        try
        {
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = _queueUrl,
                MaxNumberOfMessages = count,
                WaitTimeSeconds = _context.WaitTimeSeconds,
                AttributeNames = new List<string>() { "ApproximateReceiveCount" }
            };
            var receiveMessageResponse = await _context.Sqs.ReceiveMessageAsync(receiveMessageRequest);

            if (receiveMessageResponse.Messages.Any())
            {

                foreach (var message in receiveMessageResponse.Messages)
                {
                    var item = new QueueMessage<T>
                    {
                        MessageHandle = message.ReceiptHandle,
                        MessageId = message.MessageId
                    };
                    if (message.Attributes.Any(i => i.Key == "ApproximateReceiveCount"))
                        item.ReceiveCount = Convert.ToInt32(message.Attributes["ApproximateReceiveCount"]);
                    else
                        item.ReceiveCount = 0;

                    if (typeof(T) == typeof(string))
                    {
                        item.MessageBody = (T)Convert.ChangeType(message.Body, typeof(T));
                    }
                    else
                    {
                        item.MessageBody = JsonSerializer.Deserialize<T>(message.Body);
                    }

                    list.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            _notificationContext.Add(ex.Message);
        }

        return list;
    }

    public virtual async Task SendMessageAsync(T message)
    {
        try
        {
            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = JsonSerializer.Serialize(message)
            };

            await _context.Sqs.SendMessageAsync(sendMessageRequest);
        }
        catch (Exception ex)
        {
            _notificationContext.Add(ex.Message);
        }
    }

    public virtual async Task SendMessageAsync(List<T> messageList)
    {
        try
        {

            var messageBatchList = new List<SendMessageBatchRequestEntry>();
            foreach (var message in messageList)
            {
                messageBatchList.Add(new SendMessageBatchRequestEntry()
                {
                    Id = message.GetHashCode().ToString(),
                    MessageBody = JsonSerializer.Serialize(message)
                });
            }

            var sendMessageBatchRequest = new SendMessageBatchRequest
            {
                QueueUrl = _queueUrl,
                Entries = messageBatchList
            };

            await _context.Sqs.SendMessageBatchAsync(sendMessageBatchRequest);

        }
        catch (Exception ex)
        {
            _notificationContext.Add(ex.Message);
        }
    }
}
