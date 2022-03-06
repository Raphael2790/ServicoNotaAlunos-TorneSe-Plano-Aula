using TorneSe.ServicoNotaAluno.Domain.Entidades;
using TorneSe.ServicoNotaAluno.Domain.Enums;
using TorneSe.ServicoNotaAluno.Domain.Interfaces.Services;
using TorneSe.ServicoNotaAluno.Domain.Notification;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;
using TorneSe.ServicoNotaAluno.Domain.Utils;
using TorneSe.ServicoNotaAluno.Domain.Validations.Handlers.Interfaces;

namespace TorneSe.ServicoNotaAluno.Domain.Services;

public class NotaAlunoValidationService : INotaAlunoValidationService
{
    private readonly NotificationContext _notificationContext;
    private readonly IHandler<NotaAlunoValidationRequest> _validationHandler;

    public NotaAlunoValidationService(NotificationContext notificationContext,
                                      IHandler<NotaAlunoValidationRequest> validationHandler)
    {
        _notificationContext = notificationContext;
        _validationHandler = validationHandler;
    }

    private void ValidarAluno(Aluno aluno, int disciplinaId)
    {
        if(!aluno.Ativo)
        {
            _notificationContext.Add(Constants.ValidationMessages.ALUNO_INATIVO);
            return;
        }

        if(!AlunoEstaMatriculado(aluno,disciplinaId))
        {
            _notificationContext.Add(Constants.ValidationMessages.ALUNO_NAO_MATRICULADO);
            return;
        }
    }

    private bool AlunoEstaMatriculado(Aluno aluno, int disciplinaId) =>
        aluno.Turmas.Any(x => x.DisciplinaId == disciplinaId);

    private void ValidarProfessor(Professor professor, int disciplinaId)
    {
        //chain of responsability
        if(!professor.Ativo)
        {
            _notificationContext.Add(Constants.ValidationMessages.PROFESSOR_INATIVO);
            return;
        }

        if(!(professor.Disciplina.Id == disciplinaId))
        {
            _notificationContext.Add(Constants.ValidationMessages.PROFESSOR_NAO_PODE_DAR_NOTA_DISCIPLINA);
            return;
        }

        if(!professor.ProfessorTitular && professor.ProfessorSuplente)
        {
            _notificationContext.Add(Constants.ValidationMessages.PROFESSOR_SUPLENTE_NAO_PODE_DAR_NOTA);
            return;
        }
    }

    private void ValidarDisciplina(Disciplina disciplina)
    {
        //chain of responsability
        if(disciplina.TipoDisciplina == TipoDisciplina.Encontro)
        {
            _notificationContext.Add(Constants.ValidationMessages.DISCIPLINA_TIPO_ENCONTRO);
            return;
        }

        if(!DisciplinaAtiva(disciplina))
        {
            _notificationContext.Add(Constants.ValidationMessages.DISCIPLINA_FECHADA);
            return;
        }
    }

    private bool DisciplinaAtiva(Disciplina disciplina) =>
        disciplina.DataInicio <= DateTime.Now && disciplina.DataFim >= DateTime.Now;

    public void ValidarLancamentoNota(Aluno aluno, Professor professor, Disciplina disciplina)
    {
        ValidarDisciplina(disciplina);
        ValidarAluno(aluno, disciplina.Id);
        ValidarProfessor(professor, disciplina.Id);
    }

    public void ValidarLancamentoNota(NotaAlunoValidationRequest request)
    {
        _validationHandler.Handle(request);
    }
}

