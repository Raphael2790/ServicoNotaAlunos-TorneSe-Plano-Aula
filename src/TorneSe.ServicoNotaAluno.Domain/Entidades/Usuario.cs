using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;

namespace TorneSe.ServicoNotaAluno.Domain.Entidades;

public class Usuario : Entidade, IAggregateRoot
{
    public Usuario(string nome, string documento, DateTime dataNascimento, bool ativo, string email, bool usuarioAdm)
    {
        this.Nome = nome;
        this.Documento = documento;
        this.DataNascimento = dataNascimento;
        this.Ativo = ativo;
        this.Email = email;
        this.UsuarioAdm = usuarioAdm;

    }

    protected Usuario() { }

    public string Nome { get; private set; }
    public string Documento { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public bool Ativo { get; private set; }
    public string Email { get; private set; }
    public bool UsuarioAdm { get; private set; }
}
