using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TorneSe.ServicoNotaAluno.Domain.Entidades;
using TorneSe.ServicoNotaAluno.Domain.ObjetosDominio;

namespace TorneSe.ServicoNotaAluno.Data.Context;

public class NotaAlunoDbContext : DbContext, IUnitOfWork
{
    public NotaAlunoDbContext(DbContextOptions<NotaAlunoDbContext> options) : base(options) { }

    public virtual DbSet<Aluno> Alunos { get; set; }
    public virtual DbSet<AlunosTurmas> AlunosTurmas { get; set; }
    public virtual DbSet<Atividade> Atividades { get; set; }
    public virtual DbSet<Conteudo> Conteudos { get; set; }
    public virtual DbSet<Disciplina> Disciplinas { get; set; }
    public virtual DbSet<Nota> Notas { get; set; }
    public virtual DbSet<Professor> Professores { get; set; }
    public virtual DbSet<Turma> Turmas { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AtribuirPadraoParaTipoTexto(modelBuilder.Model);
        AtribuirPadraoParaTipoDecimal(modelBuilder.Model);
        AtribuirPadraoParaTipoDateTime(modelBuilder.Model);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotaAlunoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql("");

        optionsBuilder.LogTo(Console.WriteLine);
    }

    private static void AtribuirPadraoParaTipoTexto(IMutableModel mutableModel)
    {
        foreach (var property in mutableModel.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("VARCHAR(255)");
    }

    private static void AtribuirPadraoParaTipoDecimal(IMutableModel mutableModel)
    {
        foreach (var property in mutableModel.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(decimal))))
            property.SetColumnType("NUMERIC(9,2)");
    }

    private static void AtribuirPadraoParaTipoDateTime(IMutableModel mutableModel)
    {
        foreach (var property in mutableModel.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(DateTime))))
            property.SetColumnType("TIMESTAMP(6)");
        foreach (var property in mutableModel.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(DateTime?))))
        {
            property.SetColumnType("TIMESTAMP(6)");
            property.IsNullable = true;
        }
    }

    public async Task<bool> Commit() =>
        (await SaveChangesAsync()) > 0;
}
