using TorneSe.ServicoNotaAluno.Data.Sqs.Messages;
using TorneSe.ServicoNotaAluno.Domain.Messages;

namespace TorneSe.ServicoNotaAluno.Application.Interfaces;

public interface INotaAlunoRequestService
{
    Task<QueueMessage<RegistrarNotaAluno>> BuscarMensagem();
    Task DeletarMensagem(string messageHandle);
}
