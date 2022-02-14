using TorneSe.ServicoNotaAluno.Domain.Validations.Handlers.Interfaces;

namespace TorneSe.ServicoNotaAluno.Domain.Validations.Handlers;

public abstract class AbstractValidationHandler<T> : IHandler<T>
{
    private IHandler<T> _nextHandler;

    public virtual void Handle(T request)
    {
        if (this._nextHandler != null)
        {
            this._nextHandler.Handle(request);
        }
    }

    public IHandler<T> SetNext(IHandler<T> handler)
    {
        _nextHandler = handler;
        return _nextHandler;
    }
}
