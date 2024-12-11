using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RevisaoProva.Models;

public partial class RevisaoContext : DbContext
{
    public RevisaoContext()
    {
    }

    public RevisaoContext(DbContextOptions<RevisaoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aluno> Alunos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=revisao;user=root;password=root;port=3306");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("aluno");

            entity.Property(e => e.Codigo)
                .HasColumnType("int(11)")
                .HasColumnName("CODIGO");
            entity.Property(e => e.Nome)
                .HasMaxLength(500)
                .HasDefaultValueSql("''")
                .HasColumnName("NOME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
