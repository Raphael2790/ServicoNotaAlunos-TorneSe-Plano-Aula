using TorneSe.ServicoNotaAluno.Domain.Entidades;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;

namespace TorneSe.ServicoNotaAluno.Domain.Interfaces.Services;

public interface INotaAlunoValidationService
{
    void ValidarLancamentoNota(Aluno aluno, Professor professor, Disciplina disciplina);
    void ValidarLancamentoNota(NotaAlunoValidationRequest request);
}