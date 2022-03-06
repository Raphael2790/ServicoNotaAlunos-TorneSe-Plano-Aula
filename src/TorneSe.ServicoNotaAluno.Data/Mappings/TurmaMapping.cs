using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TorneSe.ServicoNotaAluno.Domain.Entidades;
using TorneSe.ServicoNotaAluno.Domain.Enums;

namespace TorneSe.ServicoNotaAluno.Data.Mappings;

public class TurmaMapping : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.ToTable("turmas", "servnota")
                .HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(1, 1);

        builder.Property(x => x.Nome)
                .HasColumnName("nome")
                .HasColumnType("VARCHAR(50)")
                .IsRequired();

        builder.Property(x => x.Periodo)
                .HasColumnName("periodo")
                .HasConversion<string>()
                .IsRequired();

        builder.Property(x => x.DataInicio)
                .HasColumnName("data_inicio")
                .HasColumnType("TIMESTAMP(6)")
                .IsRequired();

        builder.Property(x => x.DataFinal)
                .HasColumnName("data_final")
                .HasColumnType("TIMESTAMP(6)")
                .IsRequired();
        
        builder.Property(x => x.DataCadastro)
                .HasColumnName("data_cadastro")
                .HasColumnType("TIMESTAMP(6)")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.DisciplinaId)
                .HasColumnName("disciplina_id")
                .HasColumnType("INT")
                .IsRequired();

        builder.HasOne(x => x.Disciplina)
                .WithMany(x => x.Turmas)
                .HasForeignKey(x => x.DisciplinaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

         builder.HasMany(x => x.Alunos)
                .WithMany(x => x.Turmas)
                .UsingEntity<AlunosTurmas>("AlunosTurmas",
                    x => x.HasOne(y => y.Aluno)
                            .WithMany(y => y.AlunosTurmas)
                            .HasForeignKey(y => y.AlunoId),
                    x => x.HasOne(y => y.Turma)
                            .WithMany(y => y.AlunosTurmas)
                            .HasForeignKey(y => y.TurmaId),
                    x => 
                    {
                        x.ToTable("alunos_turmas")
                                .HasKey(y => new { y.AlunoId, y.TurmaId });

                        x.Property(x => x.AlunoId)
                                .HasColumnName("aluno_id")
                                .HasColumnType("INT")
                                .IsRequired();
                        
                        x.Property(x => x.TurmaId)
                                .HasColumnName("turma_id")
                                .HasColumnType("INT")
                                .IsRequired();
                        
                        x.Property(x => x.DataCadastro)
                                .HasColumnName("data_cadastro")
                                .HasColumnType("TIMESTAMP(6)")
                                .HasDefaultValueSql("CURRENT_TIMESTAMP");
                    });
                    
        builder.HasData(FakeTurmas());
    }

    private ICollection<Turma> FakeTurmas()
    {
        return new List<Turma>
        {
            new("Grupo Matemática I", Periodo.Noturno, new DateTime(2021,06,01),
                    new DateTime(2021,12,01), 1341567, DateTime.Now)
        };
    }
}
