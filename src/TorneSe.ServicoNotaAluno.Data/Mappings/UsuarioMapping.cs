using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TorneSe.ServicoNotaAluno.Domain.Entidades;

namespace TorneSe.ServicoNotaAluno.Data.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios", "servnota")
                .HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions<int>(1, 1);

        builder.Property(x => x.Nome)
                .HasColumnName("nome")
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

        builder.Property(x => x.Documento)
                .HasColumnName("documento")
                .HasColumnType("VARCHAR(11)")
                .IsRequired();

        builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasColumnType("VARCHAR(255)")
                .IsRequired();

        builder.Property(x => x.DataNascimento)
                .HasColumnName("data_nascimento")
                .HasColumnType("DATE")
                .IsRequired();

        builder.Property(x => x.Ativo)
                .HasColumnName("ativo")
                .HasColumnType("BOOLEAN")
                .IsRequired()
                .HasDefaultValueSql("TRUE");

        builder.Property(x => x.UsuarioAdm)
                .HasColumnName("usuario_adm")
                .HasColumnType("BOOLEAN")
                .IsRequired()
                .HasDefaultValueSql("FALSE");
    }
}
