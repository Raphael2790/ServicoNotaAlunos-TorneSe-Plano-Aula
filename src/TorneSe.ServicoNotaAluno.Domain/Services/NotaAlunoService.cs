using TorneSe.ServicoNotaAluno.Domain.Entidades;
using TorneSe.ServicoNotaAluno.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAluno.Domain.Interfaces.Services;
using TorneSe.ServicoNotaAluno.Domain.Messages;
using TorneSe.ServicoNotaAluno.Domain.Notification;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;
using TorneSe.ServicoNotaAluno.Domain.Validations.Handlers.Interfaces;

namespace TorneSe.ServicoNotaAluno.Domain.Services;

public class NotaAlunoService : INotaAlunoService
{
    private readonly NotificationContext _notificationContext;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly INotaAlunoValidationService _notaAlunoValidationService;
    private readonly IAsyncHandler<NotaAlunoValidationRequest> _requestBuildHandler;

    public NotaAlunoService(NotificationContext notificationContext,
                            IUsuarioRepository usuarioRepository,
                            INotaAlunoValidationService notaAlunoValidationService,
                            IAsyncHandler<NotaAlunoValidationRequest> requestBuildHandler
                            )
    {
        _notificationContext = notificationContext;
        _usuarioRepository = usuarioRepository;
        _notaAlunoValidationService = notaAlunoValidationService;
        _requestBuildHandler = requestBuildHandler;
    }

    public async Task LancarNotaAluno(RegistrarNotaAluno message)
    {
        //Logica para lançar a nota conforme regras de dominio
        Console.WriteLine("Executando logica de negocio para lançar nota");

        if(!message.MensagemValida())
        {
            AdicionarErrosAoContexto(message);
            return;
        }

        var request = await BuildRequest(message);

        _notaAlunoValidationService
            .ValidarLancamentoNota(request);

        if(_notificationContext.HasNotifications)
            return;

        if(AlunoPossuiNotaParaCancelar(request.Aluno, message))
            RemoverNotaAluno(request.Aluno, message);

        var nota = new Nota(request.AlunoId,request.AtividadeId, message.ValorNota,DateTime.Now, 1000);

        request.Aluno.AtribuirNota(nota);
    }

    private async Task<NotaAlunoValidationRequest> BuildRequest(RegistrarNotaAluno message)
    {
        var request = NotaAlunoValidationRequest.Instance;

        request.AlunoId = message.AlunoId;
        request.AtividadeId = message.AtividadeId;
        request.ProfessorId = message.ProfessorId;

        await _requestBuildHandler.Handle(request);

        return request;
    }

    private void RemoverNotaAluno(Aluno aluno, RegistrarNotaAluno message) 
    {
        var nota = aluno.Notas.Where(x => x.AtividadeId == message.AtividadeId).FirstOrDefault();
        nota.CancelarNotaPorRetentativa();
    }

    private void AdicionarErrosAoContexto(RegistrarNotaAluno message) =>
         _notificationContext.AddRange(message.MensagensValidacoes.Errors.Select(x => x.ErrorMessage));

    private bool AlunoPossuiNotaParaCancelar(Aluno aluno, RegistrarNotaAluno message) =>
        aluno.Notas.Any(x => x.AtividadeId == message.AtividadeId && message.NotaSubstitutiva);

}
