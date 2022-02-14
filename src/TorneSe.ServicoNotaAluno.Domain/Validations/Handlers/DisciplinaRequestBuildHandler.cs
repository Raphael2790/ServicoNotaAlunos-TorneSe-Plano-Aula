using TorneSe.ServicoNotaAluno.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAluno.Domain.Notification;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;
using TorneSe.ServicoNotaAluno.Domain.Utils;
using TorneSe.ServicoNotaAluno.Domain.Validations.Handlers.Base;

namespace TorneSe.ServicoNotaAluno.Domain.Validations.Handlers;

public class DisciplinaRequestBuildHandler : AbstractRequestBuildHandler<NotaAlunoValidationRequest>
{
    private readonly IDisciplinaRepository _disciplinaRepository;
    private readonly NotificationContext _notificationContext;

    public DisciplinaRequestBuildHandler(IDisciplinaRepository disciplinaRepository,
                                    NotificationContext notificationContext)
    {
        _disciplinaRepository = disciplinaRepository;
        _notificationContext = notificationContext;
    }

    public override async Task Handle(NotaAlunoValidationRequest request)
    {
        request.Disciplina = await _disciplinaRepository.BuscarDisciplinaPorAtividadeId(request.AtividadeId);

        if(request.Disciplina is null)
        {
            _notificationContext.Add(Constants.ValidationMessages.DISCIPLINA_INEXISTENTE);
            return;
        }
        
        await base.Handle(request);
    }
}
