using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;
using TorneSe.ServicoNotaAluno.Domain.Entidades;

namespace TorneSe.ServicoNotaAluno.Domain.Interfaces.Repositories;
public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Aluno?> BuscarAlunoPorId(int alunoId);
    Task<Professor?> BuscarProfessorPorId(int professorId);
    Task<Aluno?> BuscarAlunoPorIdDb(int alunoId);
    Task<Professor?> BuscarProfessorPorIdDb(int professorId);
    Task<IQueryable<Aluno>> PesquisarPor(System.Linq.Expressions.Expression<Func<Aluno, bool>> predicado, params System.Linq.Expressions.Expression<Func<Aluno, object>>[] inclusoes);
    Task<IQueryable<Aluno>> PesquisarTodos(params System.Linq.Expressions.Expression<Func<Aluno, object>>[] inclusoes);
}