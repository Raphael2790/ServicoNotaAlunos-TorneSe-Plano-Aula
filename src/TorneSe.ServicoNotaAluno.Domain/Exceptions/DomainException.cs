namespace TorneSe.ServicoNotaAluno.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string mensagem) : base(mensagem){}
    public DomainException(string mensagem, Exception exception) : base(mensagem, exception) { }
}
