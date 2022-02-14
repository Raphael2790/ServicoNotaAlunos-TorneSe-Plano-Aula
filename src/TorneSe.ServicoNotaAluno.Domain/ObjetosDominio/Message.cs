using FluentValidation.Results;

namespace TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;

public abstract class Message
{
    public DateTime MensagemCriada { get; protected set; }
    public ValidationResult MensagensValidacoes { get; protected set; }
    
    protected Message()
    {
        MensagemCriada = DateTime.Now;
    }

    public virtual bool MensagemValida()
    {
        throw new NotImplementedException();
    }
}
