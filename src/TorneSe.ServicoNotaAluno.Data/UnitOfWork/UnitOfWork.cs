using TorneSe.ServicoNotaAluno.Data.Context;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;

namespace TorneSe.ServicoNotaAluno.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly NotaAlunoDbContext _context;

    public UnitOfWork(NotaAlunoDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Commit()
    {
        if(_context.HasUnsavedChanges())
            return await _context.SaveChangesAsync() > 0;

        return false;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
