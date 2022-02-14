using TorneSe.ServicoNotaAluno.Domain.Entidades;
using TorneSe.ServicoNotaAluno.Domain.Utils;

namespace TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;

public class NotaAlunoValidationRequest : IRequest
{
    public static NotaAlunoValidationRequest Instance => new NotaAlunoValidationRequest();
    private NotaAlunoValidationRequest(){}

    public int AlunoId { get; set; }
    public Aluno Aluno { get;  set; }
    public int ProfessorId { get; set; }
    public Professor Professor { get;  set; }
    public int AtividadeId { get; set; }
    public Disciplina Disciplina { get; set; }
    public List<string> Erros { get; private set; }

    public bool RequestValido()
    {
        if(Professor is null)
        {
            Erros.Add(Constants.ValidationMessages.PROFESSOR_INEXISTENTE);
            return false;
        }

        if(Aluno is null)
        {
            Erros.Add(Constants.ValidationMessages.ALUNO_INEXISTENTE);
            return false;
        }

        if(Disciplina is null)
        {
            Erros.Add(Constants.ValidationMessages.DISCIPLINA_INEXISTENTE);
            return false;
        }

        return true;
    }
}
