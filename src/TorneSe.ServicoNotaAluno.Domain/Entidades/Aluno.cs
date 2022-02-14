namespace TorneSe.ServicoNotaAluno.Domain.Entidades;

public class Aluno : Entidade
{
    public Aluno(int alunoId, string nomeAbreviado, string emailInterno, int usuarioId, DateTime dataCadastro)
    {
        Id = alunoId;
        NomeAbreviado = nomeAbreviado;
        EmailInterno = emailInterno;
        UsuarioId = usuarioId;
        DataCadastro = dataCadastro;
        Notas = new List<Nota>();
    }

    protected Aluno() { }

    public string NomeAbreviado { get; private set; }
    public string EmailInterno { get; private set; }
    public int UsuarioId { get; private set; }
    public DateTime DataCadastro { get; private set; }

    public Usuario Usuario { get; set; }
    public ICollection<Nota> Notas { get; private set; }
    public ICollection<AlunosTurmas> AlunosTurmas { get;  set; }

    public void AtribuirNota(Nota nota) => Notas.Add(nota);
}
