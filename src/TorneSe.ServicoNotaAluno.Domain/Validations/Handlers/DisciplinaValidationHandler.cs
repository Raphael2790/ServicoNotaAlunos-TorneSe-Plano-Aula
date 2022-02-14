using TorneSe.ServicoNotaAluno.Domain.Entidades;
using TorneSe.ServicoNotaAluno.Domain.Enums;
using TorneSe.ServicoNotaAluno.Domain.Notification;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;
using TorneSe.ServicoNotaAluno.Domain.Utils;
using TorneSe.ServicoNotaAluno.Domain.Validations.Handlers.Interfaces;

namespace TorneSe.ServicoNotaAluno.Domain.Validations.Handlers;

public class DisciplinaValidationHandler : AbstractValidationHandler<NotaAlunoValidationRequest>
{
    private readonly NotificationContext _notificationContext;

    public DisciplinaValidationHandler(NotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public override void Handle(NotaAlunoValidationRequest request)
    {
        if(request.Disciplina.TipoDisciplina == TipoDisciplina.Encontro)
        {
            _notificationContext.Add(Constants.ValidationMessages.DISCIPLINA_TIPO_ENCONTRO);
            return;
        }

        if(!DisciplinaAtiva(request.Disciplina))
        {
            _notificationContext.Add(Constants.ValidationMessages.DISCIPLINA_FECHADA);
            return;
        }
        
        base.Handle(request);
    }

    private bool DisciplinaAtiva(Disciplina disciplina) =>
        disciplina.DataInicio <= DateTime.Now && disciplina.DataFim >= DateTime.Now;
}
