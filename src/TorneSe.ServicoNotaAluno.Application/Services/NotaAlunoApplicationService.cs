using TorneSe.ServicoNotaAluno.Application.Interfaces;
using TorneSe.ServicoNotaAluno.Domain.Interfaces.Services;
using TorneSe.ServicoNotaAluno.Domain.Messages;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;

namespace TorneSe.ServicoNotaAluno.Application.Services;

public class NotaAlunoApplicationService : INotaAlunoApplicationService
{
    private readonly INotaAlunoService _notaAlunoService;
    private readonly IUnitOfWork _uow;

    public NotaAlunoApplicationService(INotaAlunoService notaAlunoService,
                                        IUnitOfWork uow)
    {
        _notaAlunoService = notaAlunoService;
        _uow = uow;
    }
    public async Task LancarNota(RegistrarNotaAluno mensagem)
    {
        try
        {
            Console.WriteLine("Orquestrando o fluxo da aplicação");
            await _notaAlunoService.LancarNotaAluno(mensagem);
            await _uow.Commit();
        }
        catch (Exception ex)
        {
             System.Console.WriteLine(ex.Message);
        }
    }
}
