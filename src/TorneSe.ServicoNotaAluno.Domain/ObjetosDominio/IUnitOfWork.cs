namespace TorneSe.ServicoNotaAluno.Domain.ObjetosDominio
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}