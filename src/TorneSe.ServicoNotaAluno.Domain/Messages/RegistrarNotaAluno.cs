using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;
using TorneSe.ServicoNotaAluno.Domain.Validations;

namespace TorneSe.ServicoNotaAluno.Domain.Messages;

public class RegistrarNotaAluno : Message
{
    public int AlunoId { get; set; }
    public int ProfessorId { get; set; }
    public int AtividadeId { get; set; }
    public string CorrelationId { get; set; }
    public double ValorNota { get; set; }
    public bool NotaSubstitutiva { get; set; }

    public override bool MensagemValida()
    {
        MensagensValidacoes = new RegistrarNotaAlunoValidation().Validate(this);
        return MensagensValidacoes.IsValid;
    }
}
