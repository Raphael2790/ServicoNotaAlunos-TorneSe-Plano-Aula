using TorneSe.ServicoNotaAluno.Domain.Enums;

namespace TorneSe.ServicoNotaAluno.Domain.Entidades;

public class Turma : Entidade
{
    public Turma(string nome, Periodo periodo, DateTime dataInicio, DateTime dataFinal, int disciplinaId, DateTime dataCadastrado)
    {
        Nome = nome;
        Periodo = periodo;
        DataInicio = dataInicio;
        DataFinal = dataFinal;
        DisciplinaId = disciplinaId;
        DataCadastrado = dataCadastrado;
    }

    protected Turma() { }

    public string Nome { get; private set; }
    public Periodo Periodo { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime DataFinal { get; private set; }
    public int DisciplinaId { get; private set; }
    public DateTime DataCadastrado { get; private set; }

    public Disciplina Disciplina { get; private set; }
    public ICollection<AlunosTurmas> AlunosTurmas { get; private set; }
}
