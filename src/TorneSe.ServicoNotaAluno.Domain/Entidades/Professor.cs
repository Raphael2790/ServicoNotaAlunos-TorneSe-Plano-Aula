namespace TorneSe.ServicoNotaAluno.Domain.Entidades;

public class Professor : Usuario
{
    public Professor(string nome, 
                    string documento, DateTime dataNascimento, bool ativo, string email, 
                    bool usuarioAdm,int professorId, string nomeAbreviado, string emailInterno, 
                        bool professorTitular, bool professorSuplente,
                         int usuarioId, DateTime dataCadastro, int disciplinaId)
    {
        Id = professorId;
        Nome = nome;
        Documento = documento;
        DataNascimento = dataNascimento;
        Ativo = ativo;
        Email = email;
        UsuarioAdm = usuarioAdm;
        NomeAbreviado = nomeAbreviado;
        EmailInterno = emailInterno;
        ProfessorTitular = professorTitular;
        ProfessorSuplente = professorSuplente;
        DataCadastro = dataCadastro;
        DisciplinaId = disciplinaId;
    }

    protected Professor() { }

    public string NomeAbreviado { get; private set; }
    public string EmailInterno { get; private set; }
    public bool ProfessorTitular { get; private set; }
    public bool ProfessorSuplente { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public int DisciplinaId { get; private set; }

    public Disciplina Disciplina { get; set; }
}
