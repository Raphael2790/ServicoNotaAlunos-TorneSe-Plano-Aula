using TorneSe.ServicoNotaAluno.Domain.Messages;

namespace TorneSe.ServicoNotaAluno.Application.Interfaces;

public interface INotaAlunoResponseService
{
    Task Enviar(RegistrarNotaAluno notaAlunoRegistrada);
}
