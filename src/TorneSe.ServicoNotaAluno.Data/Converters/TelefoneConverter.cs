using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TorneSe.ServicoNotaAluno.Domain.ValueObjects;

namespace TorneSe.ServicoNotaAluno.Data.Converters;

public class TelefoneConverter : ValueConverter<Telefone, string>
{

    public static TelefoneConverter Instance => new();

    private TelefoneConverter() 
        : base(x => ConverterTelefoneParaTexto(x)
        , x => ConverterTextoParaTelefone(x), null)
    {
    }

    private static string ConverterTelefoneParaTexto(Telefone telefone) =>
        telefone.ToString();

    private static Telefone ConverterTextoParaTelefone(string telefone) =>
        new Telefone(telefone);
}
