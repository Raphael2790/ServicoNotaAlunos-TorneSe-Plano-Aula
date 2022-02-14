using TorneSe.ServicoNotaAluno.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAluno.Domain.Notification;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;
using TorneSe.ServicoNotaAluno.Domain.Utils;
using TorneSe.ServicoNotaAluno.Domain.Validations.Handlers.Base;

namespace TorneSe.ServicoNotaAluno.Domain.Validations.Handlers;

public class AlunoRequestBuildHandler : AbstractRequestBuildHandler<NotaAlunoValidationRequest>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly NotificationContext _notificationContext;

    public AlunoRequestBuildHandler(IUsuarioRepository usuarioRepository,
                                    NotificationContext notificationContext)
    {
        _usuarioRepository = usuarioRepository;
        _notificationContext = notificationContext;
    }

    public override async Task Handle(NotaAlunoValidationRequest request)
    {
        request.Aluno = await _usuarioRepository.BuscarAlunoPorId(request.AlunoId);

        if(request.Aluno is null)
        {
            _notificationContext.Add(Constants.ValidationMessages.ALUNO_INEXISTENTE);
            return;
        }

        await base.Handle(request);
    }
}
