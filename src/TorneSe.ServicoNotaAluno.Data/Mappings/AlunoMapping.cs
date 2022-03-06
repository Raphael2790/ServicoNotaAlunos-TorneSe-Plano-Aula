using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TorneSe.ServicoNotaAluno.Domain.Entidades;

namespace TorneSe.ServicoNotaAluno.Data.Mappings;

public class AlunoMapping : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("alunos", "servnota");

        builder.Property(x => x.NomeAbreviado)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("nome_abreviado")
                .IsRequired();

        builder.Property(x => x.EmailInterno)
                .HasColumnType("VARCHAR(200)")
                .HasColumnName("email_interno")
                .IsRequired();

        builder.Property(x => x.DataCadastro)
                .HasColumnName("data_cadastro")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasMany(x => x.Notas)
                .WithOne(x => x.Aluno)
                .HasForeignKey(x => x.AlunoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasMany(x => x.Turmas)
                .WithMany(x => x.Alunos)
                .UsingEntity<AlunosTurmas>("AlunosTurmas",
                    x => x.HasOne(y => y.Turma)
                            .WithMany(y => y.AlunosTurmas)
                            .HasForeignKey(y => y.TurmaId),
                    x => x.HasOne(y => y.Aluno)
                            .WithMany(y => y.AlunosTurmas)
                            .HasForeignKey(y => y.AlunoId),
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

        builder.Navigation(x => x.Turmas)
                .AutoInclude();

        builder.Navigation(x => x.Notas)
                .AutoInclude();

        builder.HasData(AlunosFake());
    }

    private ICollection<Aluno> AlunosFake()
    {
        var alunos = new List<Aluno>();

        Aluno aluno = new("Raphael Silvestre", "87628929919", new DateTime(1990, 3, 10), true,
        "raphael.s@email.com", false,1234,"Raphael", "raphael.s@email.com", 1212, DateTime.Now);

        // aluno.Turmas = new List<Turma>
        // {
        //     new("Grupo Matemática I", Periodo.Noturno, new DateTime(2021,06,01),
        //             new DateTime(2021,12,01), 1341567, DateTime.Now)
        // };

        alunos.Add(aluno);

        return alunos;
    }

}
