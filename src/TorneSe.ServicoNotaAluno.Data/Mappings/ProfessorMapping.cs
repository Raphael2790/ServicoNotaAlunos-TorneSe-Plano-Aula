using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TorneSe.ServicoNotaAluno.Domain.Entidades;

namespace TorneSe.ServicoNotaAluno.Data.Mappings;

public class ProfessorMapping : IEntityTypeConfiguration<Professor>
{
    public void Configure(EntityTypeBuilder<Professor> builder)
    {
        builder.ToTable("professores", "servnota");

        builder.Property(x => x.NomeAbreviado)
                .HasColumnName("nome_abreviado")
                .HasMaxLength(50)
                .HasColumnType("VARCHAR(50)")
                .IsRequired();

        builder.Property(x => x.EmailInterno)
                .HasColumnName("email_interno")
                .HasMaxLength(100)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

        builder.Property(x => x.ProfessorTitular)
                .HasColumnName("professor_titular")
                .HasColumnType("BOOLEAN")
                .HasDefaultValueSql("TRUE")
                .IsRequired();

        builder.Property(x => x.ProfessorSuplente)
                .HasColumnName("professor_suplente")
                .HasColumnType("BOOLEAN")
                .HasDefaultValueSql("FALSE")
                .IsRequired();

        builder.Property(x => x.DataCadastro)
                .HasColumnName("data_cadastro")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.DisciplinaId)
                .HasColumnName("diciplina_id")
                .HasColumnType("INT")
                .IsRequired();

        builder.HasOne(x => x.Disciplina)
                .WithOne(x => x.Professor)
                .HasForeignKey<Disciplina>(x => x.ProfessorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasData(ProfessoresFake());
    }

    private ICollection<Professor> ProfessoresFake()
    {
        var professores = new List<Professor>();

        Professor professor = new("Danilo Aparecido", "30292919821", new DateTime(1983,01,01), true,
        "danilo.aparecido@email.com", false,1282727, "Danilo", "danilo.s@email.com", true,false,1212, DateTime.Now,1341567);

        professores.Add(professor);

        return professores;
    }
}
