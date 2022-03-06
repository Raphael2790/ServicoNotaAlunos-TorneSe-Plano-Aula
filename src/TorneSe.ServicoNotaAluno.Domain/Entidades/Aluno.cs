namespace TorneSe.ServicoNotaAluno.Domain.Entidades;

public class Aluno : Usuario
{
    public Aluno(string nome, string documento, DateTime dataNascimento, bool ativo, 
                string email, bool usuarioAdm,int alunoId, string nomeAbreviado, 
                string emailInterno, int usuarioId, DateTime dataCadastro)
    {
        Id = alunoId;
        Nome = nome;
        Documento = documento;
        DataNascimento = dataNascimento;
        Ativo = ativo;
        Email = email;
        UsuarioAdm = usuarioAdm;
        NomeAbreviado = nomeAbreviado;
        EmailInterno = emailInterno;
        DataCadastro = dataCadastro;
        Notas = new List<Nota>();
    }

    protected Aluno() { }

    public string NomeAbreviado { get; private set; }
    public string EmailInterno { get; private set; }
    public DateTime DataCadastro { get; private set; }

    public ICollection<Nota> Notas { get; private set; }
    public ICollection<AlunosTurmas> AlunosTurmas { get;  set; }
    public ICollection<Turma> Turmas { get; set; }

    public void AtribuirNota(Nota nota) => Notas.Add(nota);
}
