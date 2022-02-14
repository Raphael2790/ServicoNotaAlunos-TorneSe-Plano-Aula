using TorneSe.ServicoNotaAluno.Domain.Entidades;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;

namespace TorneSe.ServicoNotaAluno.Domain.Interfaces.Repositories;
public interface IDisciplinaRepository : IRepository<Disciplina>
{
    Task<Disciplina?> BuscarDisciplinaPorAtividadeId(int atividadeId);
}
