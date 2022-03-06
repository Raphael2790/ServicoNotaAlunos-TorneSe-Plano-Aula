using Microsoft.EntityFrameworkCore;
using TorneSe.ServicoNotaAluno.Data.Context;
using TorneSe.ServicoNotaAluno.Domain.Entidades;
using TorneSe.ServicoNotaAluno.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;

namespace TorneSe.ServicoNotaAluno.Data.Repositories;

public class DisciplinaRepository : IDisciplinaRepository
{
    private readonly FakeDbContext _contexto;
    private readonly NotaAlunoDbContext _context;

    public DisciplinaRepository(FakeDbContext contexto,
                                NotaAlunoDbContext context)
    {
        _contexto = contexto;
        _context = context;
    }

    public async Task<Disciplina?> BuscarDisciplinaPorAtividadeId(int atividadeId)
    {
        return await Task.FromResult(_contexto.Disciplinas
                .FirstOrDefault(x => x.Conteudos.Any(y => y.Atividades.Any(z => z.Id == atividadeId))));
    } 

    public async Task<Disciplina?> BuscarDisciplinaPorAtividadeIdDb(int atividadeId)
    {
        return await _context.Disciplinas
                .FirstOrDefaultAsync(x => x.Conteudos.Any(y => y.Atividades.Any(z => z.Id == atividadeId)));
    } 

    public IUnitOfWork UnitOfWork => _context;

    public void Dispose() 
    {
        _context?.Dispose();
        _contexto?.Dispose();
    }
}
