using TorneSe.ServicoNotaAluno.Domain.Messages;

namespace TorneSe.ServicoNotaAluno.Data.Sqs.SQS.Clients.Interfaces;

public interface ILancarNotaAlunoResponseClient : IQueueClient<NotaAlunoRegistrada> { }

