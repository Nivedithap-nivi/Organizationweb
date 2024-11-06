using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Organizationweb.Models;

public partial class OrganizationContext : DbContext
{
    public OrganizationContext()
    {
    }

    public OrganizationContext(DbContextOptions<OrganizationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DeptTbl> DeptTbls { get; set; }

    public virtual DbSet<Emp> Emps { get; set; }

    public virtual DbSet<Library> Libraries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=Organization;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeptTbl>(entity =>
        {
            entity.HasKey(e => e.DeptId);

            entity.ToTable("DeptTbl");

            entity.Property(e => e.DeptId).ValueGeneratedNever();
            entity.Property(e => e.DeptName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Lib).WithMany(p => p.DeptTbls)
                .HasForeignKey(d => d.LibId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeptTbl_Library");
        });

        modelBuilder.Entity<Emp>(entity =>
        {
            entity.ToTable("Emp");

            entity.Property(e => e.EmpId).ValueGeneratedNever();
            entity.Property(e => e.EmpName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Dept).WithMany(p => p.Emps)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Emp_DeptTbl");
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.HasKey(e => e.LibId);

            entity.ToTable("Library");

            entity.Property(e => e.LibId).ValueGeneratedNever();
            entity.Property(e => e.LibName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ui).HasColumnName("ui");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
