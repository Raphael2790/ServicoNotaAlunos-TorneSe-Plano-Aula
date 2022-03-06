namespace TorneSe.ServicoNotaAluno.Domain.Entidades;

public class AlunosTurmas
{
    public int AlunoId { get; set; }
    public int TurmaId { get; set; }
    public DateTime DataCadastro { get; set; }

    public Turma Turma { get; set; }
    public Aluno Aluno { get; set; }
}
