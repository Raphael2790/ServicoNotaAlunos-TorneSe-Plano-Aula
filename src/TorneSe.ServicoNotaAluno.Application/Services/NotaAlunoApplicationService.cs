using TorneSe.ServicoNotaAluno.Application.Interfaces;
using TorneSe.ServicoNotaAluno.Domain.Interfaces.Services;
using TorneSe.ServicoNotaAluno.Domain.Messages;

namespace TorneSe.ServicoNotaAluno.Application.Services;

public class NotaAlunoApplicationService : INotaAlunoApplicationService
{
    private readonly INotaAlunoService _notaAlunoService;

    public NotaAlunoApplicationService(INotaAlunoService notaAlunoService)
    {
        _notaAlunoService = notaAlunoService;
    }
    public async Task LancarNota(RegistrarNotaAluno mensagem)
    {
        try
        {
            Console.WriteLine("Orquestrando o fluxo da aplicação");
            await _notaAlunoService.LancarNotaAluno(mensagem);
        }
        catch (Exception ex)
        {
             System.Console.WriteLine(ex.Message);
        }
    }
}
