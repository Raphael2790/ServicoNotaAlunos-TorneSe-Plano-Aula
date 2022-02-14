using System.Linq.Expressions;
using TorneSe.ServicoNotaAluno.Data.Context;
using TorneSe.ServicoNotaAluno.Domain.Entidades;
using TorneSe.ServicoNotaAluno.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;

namespace TorneSe.ServicoNotaAluno.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly FakeDbContext _context;

    public UsuarioRepository(FakeDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Aluno?> BuscarAlunoPorId(int alunoId) =>
        await Task.FromResult(_context.Alunos.FirstOrDefault(x => x.Id == alunoId));

    public async Task<Professor?> BuscarProfessorPorId(int professorId) =>
        await Task.FromResult(_context.Professores.FirstOrDefault(x => x.Id == professorId));

    public void Dispose() =>
        _context?.Dispose();

    public async Task<IQueryable<Aluno>> PesquisarPor(Expression<Func<Aluno, bool>> predicado, params Expression<Func<Aluno, object>>[] inclusoes)
    {
        IQueryable<Aluno> query = await PesquisarTodos(inclusoes);

        query = query.Where(predicado);

        return query;
    }

    public async Task<IQueryable<Aluno>> PesquisarTodos(params System.Linq.Expressions.Expression<Func<Aluno, object>>[] inclusoes)
    {
        // IQueryable<Aluno> query = _context.Set<Aluno>();

        // foreach (var item in inclusoes)
        // {
        //     query = query.Include(item);
        // }

        // return query;
        return new List<Aluno>().AsQueryable();
    }

}
