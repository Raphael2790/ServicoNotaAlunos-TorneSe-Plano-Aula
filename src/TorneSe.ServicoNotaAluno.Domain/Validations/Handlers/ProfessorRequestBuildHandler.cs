using TorneSe.ServicoNotaAluno.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAluno.Domain.Notification;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;
using TorneSe.ServicoNotaAluno.Domain.Utils;
using TorneSe.ServicoNotaAluno.Domain.Validations.Handlers.Base;

namespace TorneSe.ServicoNotaAluno.Domain.Validations.Handlers;

public class ProfessorRequestBuildHandler : AbstractRequestBuildHandler<NotaAlunoValidationRequest>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly NotificationContext _notificationContext;

    public ProfessorRequestBuildHandler(IUsuarioRepository usuarioRepository,
                                        NotificationContext notificationContext)
    {
        _usuarioRepository = usuarioRepository;
        _notificationContext = notificationContext;
    }

    public override async Task Handle(NotaAlunoValidationRequest request)
    {
        request.Professor = await _usuarioRepository.BuscarProfessorPorIdDb(request.ProfessorId);

        if(request.Professor is null)
        {
            _notificationContext.Add(Constants.ValidationMessages.PROFESSOR_INEXISTENTE);
            return;
        }

        await base.Handle(request);
    }
}
