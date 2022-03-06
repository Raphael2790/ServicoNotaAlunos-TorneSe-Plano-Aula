using TorneSe.ServicoNotaAluno.Domain.Entidades;
using TorneSe.ServicoNotaAluno.Domain.Notification;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;
using TorneSe.ServicoNotaAluno.Domain.Utils;

namespace TorneSe.ServicoNotaAluno.Domain.Validations.Handlers;

public class AlunoValidationHandler : AbstractValidationHandler<NotaAlunoValidationRequest>
{
    private readonly NotificationContext _notificationContext;

    public AlunoValidationHandler(NotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public override void Handle(NotaAlunoValidationRequest request)
    {
        if(!request.Aluno.Ativo)
        {
            _notificationContext.Add(Constants.ValidationMessages.ALUNO_INATIVO);
            return;
        }

        if(!AlunoEstaMatriculado(request.Aluno,request.Disciplina.Id))
        {
            _notificationContext.Add(Constants.ValidationMessages.ALUNO_NAO_MATRICULADO);
            return;
        }

        if(request.Aluno.Notas.Any(x => x.AtividadeId == request.AtividadeId && x.CanceladaPorRetentativa))
        {
            _notificationContext.Add(Constants.ValidationMessages.ALUNO_JA_POSSUI_ATIVIDADE_SEMELHANTE_CANCELADA);
        }

        base.Handle(request);
    }

    private bool AlunoEstaMatriculado(Aluno aluno, int disciplinaId) =>
        aluno.Turmas.Any(x => x.DisciplinaId == disciplinaId);
}
