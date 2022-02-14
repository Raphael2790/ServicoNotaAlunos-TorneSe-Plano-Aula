using TorneSe.ServicoNotaAluno.Domain.Messages;

namespace TorneSe.ServicoNotaAluno.Application.Interfaces;

public interface INotaAlunoApplicationService
{
    Task LancarNota(RegistrarNotaAluno mensagem);
}
