using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Library.WebApi.Models;

public partial class SchoolLibraryDbContext : DbContext
{
    public SchoolLibraryDbContext()
    {
    }

    public SchoolLibraryDbContext(DbContextOptions<SchoolLibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BorrowInfo> BorrowInfos { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=SchoolLibraryDB;Integrated Security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C2074E39E7A5");

            entity.Property(e => e.BookAuthor)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BookPublisher)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BookTitle)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BorrowInfo>(entity =>
        {
            entity.HasKey(e => e.BorrowId).HasName("PK__BorrowIn__4295F83FC22FA6C3");

            entity.ToTable("BorrowInfo");

            entity.Property(e => e.BorrowDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");

            entity.HasOne(d => d.Book).WithMany(p => p.BorrowInfos)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BorrowInf__BookI__29572725");

            entity.HasOne(d => d.Student).WithMany(p => p.BorrowInfos)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BorrowInf__Stude__286302EC");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52B99BE6E1F8A");

            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
