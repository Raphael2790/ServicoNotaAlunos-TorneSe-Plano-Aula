using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;

namespace TorneSe.ServicoNotaAluno.Domain.Messages;

public class NotaAlunoRegistrada : Message
{
    public NotaAlunoRegistrada()
    {
        Erros = new ();
    }
    public int AlunoId { get; set; }
    public int AtividadeId { get; set; }
    public bool PossuiErros { get; set; }
    public Guid CorrelationId { get; set; }
    public List<string> Erros { get; set; }
}
