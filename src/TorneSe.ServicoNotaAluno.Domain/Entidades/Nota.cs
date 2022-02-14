namespace TorneSe.ServicoNotaAluno.Domain.Entidades;

public class Nota
{
    public Nota(int alunoId, int atividadeId, double valorNota, DateTime dataLancamento, int usuarioId)
    {
        AlunoId = alunoId;
        AtividadeId = atividadeId;
        ValorNota = valorNota;
        DataLancamento = dataLancamento;
        UsuarioId = usuarioId;
        CanceladaPorRetentativa = false;
    }

    protected Nota() { }

    public int AlunoId { get; private set; }
    public int AtividadeId { get; private set; }
    public double ValorNota { get; private set; }
    public DateTime DataLancamento { get; set; }
    public int UsuarioId { get; private set; }
    public bool CanceladaPorRetentativa { get; private set; }

    public Aluno Aluno { get; set; }
    public Atividade Atividade { get; set; }

    public void CancelarNotaPorRetentativa() =>
        CanceladaPorRetentativa = true;
}
