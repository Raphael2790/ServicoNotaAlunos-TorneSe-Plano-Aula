using TorneSe.ServicoNotaAluno.Domain.Validations.Handlers.Interfaces;

namespace TorneSe.ServicoNotaAluno.Domain.Validations.Handlers.Base;

public abstract class AbstractRequestBuildHandler<T> : IAsyncHandler<T>
{
    private IAsyncHandler<T> _nextHandler;

    public virtual async Task Handle(T request)
    {
        if (this._nextHandler != null)
        {
            await this._nextHandler.Handle(request);
        }
    }

    public IAsyncHandler<T> SetNext(IAsyncHandler<T> handler)
    {
        _nextHandler = handler;
        return _nextHandler;
    }
}
