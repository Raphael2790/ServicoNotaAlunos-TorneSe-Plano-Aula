using TorneSe.ServicoNotaAluno.Domain.Messages;

namespace TorneSe.ServicoNotaAluno.Domain.Interfaces.Services;

public interface INotaAlunoService
{
    Task LancarNotaAluno(RegistrarNotaAluno message);
}
