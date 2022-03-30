namespace TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;
public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
}

