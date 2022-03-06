using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;

namespace TorneSe.ServicoNotaAluno.Domain.Entidades;

public class Usuario : Entidade, IAggregateRoot
{
    protected Usuario(string nome, string documento, DateTime dataNascimento, bool ativo, string email, bool usuarioAdm)
    {
        Id = -1;
        this.Nome = nome;
        this.Documento = documento;
        this.DataNascimento = dataNascimento;
        this.Ativo = ativo;
        this.Email = email;
        this.UsuarioAdm = usuarioAdm;
    }

    protected Usuario() { }

    public string Nome { get; protected set; }
    public string Documento { get; protected set; }
    public DateTime DataNascimento { get; protected set; }
    public bool Ativo { get; protected set; }
    public string Email { get; protected set; }
    public bool UsuarioAdm { get; protected set; }
}
