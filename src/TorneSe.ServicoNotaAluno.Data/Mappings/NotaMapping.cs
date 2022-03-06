using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TorneSe.ServicoNotaAluno.Domain.Entidades;

namespace TorneSe.ServicoNotaAluno.Data.Mappings;

public class NotaMapping : IEntityTypeConfiguration<Nota>
{
    public void Configure(EntityTypeBuilder<Nota> builder)
    {
        builder.ToTable("notas", "servnota")
                .HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions<int>(1, 1);

        builder.Property(x => x.ValorNota)
                .HasColumnName("valor_nota")
                .HasPrecision(3, 2)
                .IsRequired();
        
        builder.Property(x => x.DataLancamento)
                .HasColumnName("data_lancamento")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.UsuarioId)
                .HasColumnName("usuario_id")
                .HasPrecision(10)
                .IsRequired();

        builder.Property(x => x.CanceladaPorRetentativa)
                .HasColumnName("cancelada_por_retentativa")
                .HasColumnType("BOOLEAN")
                .IsRequired();

        builder.Property(x => x.AlunoId)
                .HasColumnName("aluno_id")
                .HasColumnType("INT")
                .IsRequired();

        builder.Property(x => x.AtividadeId)
                .HasColumnName("atividade_id")
                .HasColumnType("INT")
                .IsRequired();

        builder.HasOne(x => x.Aluno)
                .WithMany(x => x.Notas)
                .HasForeignKey(x => x.AlunoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Atividade)
                .WithMany(x => x.Notas)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
