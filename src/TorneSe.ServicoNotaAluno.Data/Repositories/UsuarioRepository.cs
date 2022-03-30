using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TorneSe.ServicoNotaAluno.Data.Context;
using TorneSe.ServicoNotaAluno.Domain.Entidades;
using TorneSe.ServicoNotaAluno.Domain.Interfaces.Repositories;

namespace TorneSe.ServicoNotaAluno.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly FakeDbContext _contexto;
    private readonly NotaAlunoDbContext _context;

    public UsuarioRepository(FakeDbContext contexto,
                            NotaAlunoDbContext context)
    {
        _contexto = contexto;
        _context = context;
    }

    public async Task<Aluno?> BuscarAlunoPorId(int alunoId) =>
        await Task.FromResult(_contexto.Alunos.FirstOrDefault(x => x.Id == alunoId));

    public async Task<Professor?> BuscarProfessorPorId(int professorId) =>
        await Task.FromResult(_contexto.Professores.FirstOrDefault(x => x.Id == professorId));

    public async Task<Aluno?> BuscarAlunoPorIdDb(int alunoId) =>
        await _context.Alunos.TagWith("-- Use NOLOCK").FirstOrDefaultAsync(x => x.Id == alunoId);

    public async Task<Professor?> BuscarProfessorPorIdDb(int professorId) =>
        await _context.Professores.TagWith("-- Use NOLOCK").AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(x => x.Id == professorId);

    public void Dispose() =>
        _contexto?.Dispose();

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
