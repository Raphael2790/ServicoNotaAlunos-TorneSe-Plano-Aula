using TorneSe.ServicoNotaAluno.Data.Context;
using TorneSe.ServicoNotaAluno.Domain.Entidades;
using TorneSe.ServicoNotaAluno.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;

namespace TorneSe.ServicoNotaAluno.Data.Repositories;

public class DisciplinaRepository : IDisciplinaRepository
{
    private readonly FakeDbContext _contexto;

    public DisciplinaRepository(FakeDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<Disciplina?> BuscarDisciplinaPorAtividadeId(int atividadeId)
    {
        return await Task.FromResult(_contexto.Disciplinas
                .FirstOrDefault(x => x.Conteudos.Any(y => y.Atividades.Any(z => z.Id == atividadeId))));
    } 

    public IUnitOfWork UnitOfWork => _contexto;

    public void Dispose() {}
}
