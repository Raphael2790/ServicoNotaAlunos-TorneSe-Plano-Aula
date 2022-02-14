using FluentValidation;
using TorneSe.ServicoNotaAluno.Domain.Messages;

namespace TorneSe.ServicoNotaAluno.Domain.Validations;

public class RegistrarNotaAlunoValidation : AbstractValidator<RegistrarNotaAluno>
{
    public static string AlunoIdErroMsg => "O identificador do aluno deve ser maior que zero";
    public static string ProfessorIdErroMsg => "O identificador do professor deve ser maior que zero";
    public static string AtividadeIdErroMsg => "O identificador da atividade deve ser maior que zero";
    public static string CorrelationIdErroMsg => "O identificador da transação não pode ser nulo ou texto vazio";
    public static string NotaAlunoErroMsg => "A nota do aluno não pode ser menor que zero";

    public RegistrarNotaAlunoValidation()
    {
        RuleFor(x => x.AlunoId)
            .GreaterThan(default(int))
            .WithMessage(AlunoIdErroMsg);

        RuleFor(x => x.ProfessorId)
            .GreaterThan(default(int))
            .WithMessage(ProfessorIdErroMsg);
        
        RuleFor(x => x.AtividadeId)
            .GreaterThan(default(int))
            .WithMessage(AtividadeIdErroMsg);

        RuleFor(x => x.CorrelationId)
            .NotEmpty()
            .WithMessage(CorrelationIdErroMsg)
            .NotNull()
            .WithMessage(CorrelationIdErroMsg);

        RuleFor(x => x.ValorNota)
            .GreaterThanOrEqualTo(default(int))
            .WithMessage(NotaAlunoErroMsg);
    }
}
