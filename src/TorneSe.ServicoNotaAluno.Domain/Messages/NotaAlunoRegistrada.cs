using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;

namespace TorneSe.ServicoNotaAluno.Domain.Messages;

public class NotaAlunoRegistrada : Message
{
    public NotaAlunoRegistrada()
    {
        Erros = new List<string>();
    }
    public int AlunoId { get; set; }
    public int AtividadeId { get; set; }
    public bool PossuiErros { get; set; }
    public string CorrelationId { get; set; }
    public IEnumerable<string> Erros { get; set; }
}
