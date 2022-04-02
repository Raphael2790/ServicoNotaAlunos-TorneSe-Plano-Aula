using Amazon;
using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Context.Interfaces;

namespace TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Context;

public class SqsContext : ISqsContext
{
    private readonly AmazonSQSClient _sqs;
    private readonly int _waitTimeSeconds;
    private readonly string _awsAccessKey;
    private readonly string _awsSecretAccessKey;
    private readonly IConfiguration _configuration;

    public SqsContext(IConfiguration configuration)
    {
         var awsConfiguration = configuration.GetSection("AWSConfiguration");
        _awsAccessKey = awsConfiguration.GetValue<string>("AWSAccessKey");
        _awsSecretAccessKey = awsConfiguration.GetValue<string>("AWSSecretAccessKey");
        _waitTimeSeconds = awsConfiguration.GetValue<int>("AWSQueueWaitTimeSeconds");
        _sqs = GetAmazonSqsClient();
        _configuration = configuration;
    }

    public AmazonSQSClient Sqs => _sqs;

    public int WaitTimeSeconds => _waitTimeSeconds;

    public string GetQueueUrl(string queueVariable)
    {
        string queueName = GetQueueName(queueVariable);
        return _sqs.GetQueueUrlAsync(queueName).Result.QueueUrl;
    }

    private AmazonSQSClient GetAmazonSqsClient()
    {
        return new AmazonSQSClient(_awsAccessKey, _awsSecretAccessKey, RegionEndpoint.USEast1);
    }

    private string GetQueueName(string queueVariable)
    {
        return _configuration.GetSection("AWSQueue").GetValue<string>(queueVariable);
    }
}