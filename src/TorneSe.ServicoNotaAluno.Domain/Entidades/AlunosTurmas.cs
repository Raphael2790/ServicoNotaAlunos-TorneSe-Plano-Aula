namespace TorneSe.ServicoNotaAluno.Domain.Entidades;

public class AlunosTurmas
{
    public AlunosTurmas()
    {
        Turmas = new List<Turma>();
        Alunos = new List<Aluno>();
    }

    public int AlunoId { get; set; }
    public int TurmaId { get; set; }
    public DateTime DataCadastro { get; set; }

    public ICollection<Turma> Turmas { get; set; }
    public ICollection<Aluno> Alunos { get; set; }
}
