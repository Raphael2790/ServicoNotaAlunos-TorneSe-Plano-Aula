namespace TorneSe.ServicoNotaAluno.Domain.ObjetosDominio
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}