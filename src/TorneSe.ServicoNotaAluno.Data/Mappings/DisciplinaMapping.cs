using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TorneSe.ServicoNotaAluno.Domain.Entidades;
using TorneSe.ServicoNotaAluno.Domain.Enums;

namespace TorneSe.ServicoNotaAluno.Data.Mappings;

public class DisciplinaMapping : IEntityTypeConfiguration<Disciplina>
{
    public void Configure(EntityTypeBuilder<Disciplina> builder)
    {
        builder.ToTable("disciplinas", "servnota")
                .HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityAlwaysColumn<int>()
                .HasIdentityOptions<int>(1, 1);
        
        builder.Property(x => x.Nome)
                .HasColumnName("nome")
                .HasColumnType("VARCHAR(100)")
                .IsRequired()
                .HasMaxLength(100);
        
        builder.Property(x => x.Descricao)
                .HasColumnName("descricao")
                .HasColumnType("VARCHAR(100)")
                .IsRequired()
                .HasMaxLength(100);

        builder.Property(x => x.DataInicio)
                .HasColumnName("data_inicio")
                .HasColumnType("TIMESTAMP(6)")
                .IsRequired();

        builder.Property(x => x.DataFim)
                .HasColumnName("data_fim")
                .HasColumnType("TIMESTAMP(6)")
                .IsRequired();
        
        builder.Property(x => x.DataCadastro)
                .HasColumnName("data_cadastro")
                .HasColumnType("TIMESTAMP(6)")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.TipoDisciplina)
                .HasColumnName("tipo_disciplina")
                .HasConversion<string>()
                .IsRequired();

        builder.Property(x => x.ProfessorId)
                .HasColumnName("professor_id")
                .HasColumnType("INT");

        builder.HasOne(x => x.Professor)
                .WithOne(x => x.Disciplina)
                .HasForeignKey<Disciplina>(x => x.ProfessorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasMany(x => x.Conteudos)
                .WithOne(x => x.Disciplina)
                .HasForeignKey(x => x.DisciplinaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Navigation(x => x.Conteudos)
                .AutoInclude(true);

        builder.HasData(DisciplinasFake());
    }

    private ICollection<Disciplina> DisciplinasFake()
    {
        //biblioteca Bogus para gerar dados falsos é uma oportunidade

        var disciplinas = new List<Disciplina>();

        var disciplina = new Disciplina(1341567,"Matemática", "Matemática base ensino médio"
        , new DateTime(2021,10,12),new DateTime(2022,02,12), TipoDisciplina.Teorica, 
        new DateTime(2021, 09,12), 1282727);

        // var conteudo = new Conteudo("Equações segundo grau", "Aprendizado de equações de segundo grau",
        // new DateTime(2021,10,18), new DateTime(2021,11,18), new DateTime(2021,10,15));

        // var atividade = new Atividade(34545,"Atividade avaliativa equações", TipoAtividade.Avaliativa,
        //  new DateTime(2021,11,10), new DateTime(2021, 11, 01), false);

        //conteudo.CadastrarAtividade(atividade);

        //disciplina.AdicionarConteudo(conteudo);

        disciplinas.Add(disciplina);

        return disciplinas;
    }
}
