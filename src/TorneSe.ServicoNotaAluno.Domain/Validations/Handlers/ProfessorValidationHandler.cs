using TorneSe.ServicoNotaAluno.Domain.Notification;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;
using TorneSe.ServicoNotaAluno.Domain.Utils;

namespace TorneSe.ServicoNotaAluno.Domain.Validations.Handlers;

public class ProfessorValidationHandler : AbstractValidationHandler<NotaAlunoValidationRequest>
{
    private readonly NotificationContext _notificationContext;

    public ProfessorValidationHandler(NotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public override void Handle(NotaAlunoValidationRequest request)
    {
        if(!request.Professor.Usuario.Ativo)
        {
            _notificationContext.Add(Constants.ValidationMessages.PROFESSOR_INATIVO);
            return;
        }

        if(!(request.Professor.DisciplinaId == request.Disciplina.Id))
        {
            _notificationContext.Add(Constants.ValidationMessages.PROFESSOR_NAO_PODE_DAR_NOTA_DISCIPLINA);
            return;
        }

        if(!request.Professor.ProfessorTitular && request.Professor.ProfessorSuplente)
        {
            _notificationContext.Add(Constants.ValidationMessages.PROFESSOR_SUPLENTE_NAO_PODE_DAR_NOTA);
            return;
        }
        
        base.Handle(request);
    }
}
