using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using WebApplication1000.Entities;

namespace WebApplication1000.Context;

/// <summary>
/// The database context class for interacting with the MySQL database.
/// </summary>
public partial class MyDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MyDbContext"/> class.
    /// </summary>
    public MyDbContext()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MyDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dydaktyk> Dydaktyks { get; set; }

    public virtual DbSet<Dyrektor> Dyrektors { get; set; }

    public virtual DbSet<Grupa> Grupas { get; set; }

    public virtual DbSet<Osoba> Osobas { get; set; }

    public virtual DbSet<Przedmiot> Przedmiots { get; set; }

    public virtual DbSet<Semestr> Semestrs { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Zajecium> Zajecia { get; set; }
    
    /// <summary>
    /// Configures the database context options.
    /// </summary>
    /// <param name="optionsBuilder">The options builder.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=sql7.freesqldatabase.com;database=sql7713329;uid=sql7713329;pwd=71tFe2myZS;AllowZeroDateTime=True;ConvertZeroDateTime=True", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.5.62-mysql"));

    /// <summary>
    /// Configures the schema needed for the context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder
        .UseCollation("latin1_swedish_ci")
        .HasCharSet("latin1");

    modelBuilder.Entity<Dydaktyk>(entity =>
    {
        entity.HasKey(e => e.OsobaSIndex).HasName("PRIMARY");

        entity.ToTable("Dydaktyk");

        entity.Property(e => e.OsobaSIndex)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("Osoba_S_index");
        entity.Property(e => e.MagisterDegree)
            .HasMaxLength(50)
            .HasColumnName("Magister_Degree");
        entity.Property(e => e.Password).HasMaxLength(20);

        entity.HasOne(d => d.OsobaSIndexNavigation).WithOne(p => p.Dydaktyk)
            .HasForeignKey<Dydaktyk>(d => d.OsobaSIndex)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("Dydaktyk_Osoba");
    });

    modelBuilder.Entity<Dyrektor>(entity =>
    {
        entity.HasKey(e => e.OsobaSIndex).HasName("PRIMARY");

        entity.ToTable("Dyrektor");

        entity.Property(e => e.OsobaSIndex)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("Osoba_S_index");
        entity.Property(e => e.Password).HasMaxLength(20);
        entity.Property(e => e.Year).HasColumnType("int(11)");

        entity.HasOne(d => d.OsobaSIndexNavigation).WithOne(p => p.Dyrektor)
            .HasForeignKey<Dyrektor>(d => d.OsobaSIndex)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("Dyrektor_Osoba");
    });

    modelBuilder.Entity<Grupa>(entity =>
    {
        entity.HasKey(e => e.IdGrupa).HasName("PRIMARY");

        entity.ToTable("Grupa");

        entity.HasIndex(e => e.SemestrIdSemestr, "Grupa_Semestr");

        entity.Property(e => e.IdGrupa)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("id_Grupa");
        entity.Property(e => e.SemestrIdSemestr)
            .HasColumnType("int(11)")
            .HasColumnName("Semestr_id_semestr");

        entity.HasOne(d => d.SemestrIdSemestrNavigation).WithMany(p => p.Grupas)
            .HasForeignKey(d => d.SemestrIdSemestr)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("Grupa_Semestr");

        entity.HasMany(d => d.DydaktykOsobaSIndices).WithMany(p => p.GrupaIdGrupas)
            .UsingEntity<Dictionary<string, object>>(
                "DydaktykGrupa",
                r => r.HasOne<Dydaktyk>().WithMany()
                    .HasForeignKey("DydaktykOsobaSIndex")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Dydaktyk_Grupa_Dydaktyk"),
                l => l.HasOne<Grupa>().WithMany()
                    .HasForeignKey("GrupaIdGrupa")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_13_Grupa"),
                j =>
                {
                    j.HasKey("GrupaIdGrupa", "DydaktykOsobaSIndex")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    j.ToTable("Dydaktyk_Grupa");
                    j.HasIndex(new[] { "DydaktykOsobaSIndex" }, "Dydaktyk_Grupa_Dydaktyk");
                    j.IndexerProperty<int>("GrupaIdGrupa")
                        .HasColumnType("int(11)")
                        .HasColumnName("Grupa_id_Grupa");
                    j.IndexerProperty<int>("DydaktykOsobaSIndex")
                        .HasColumnType("int(11)")
                        .HasColumnName("Dydaktyk_Osoba_S_index");
                });

        entity.HasMany(d => d.StudentOsobaSIndices).WithMany(p => p.GrupaIdGrupas)
            .UsingEntity<Dictionary<string, object>>(
                "StudentGrupa",
                r => r.HasOne<Student>().WithMany()
                    .HasForeignKey("StudentOsobaSIndex")
                    .OnDelete(DeleteBehavior.Cascade) // Enable cascade delete
                    .HasConstraintName("Student_Grupa_Student"),
                l => l.HasOne<Grupa>().WithMany()
                    .HasForeignKey("GrupaIdGrupa")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StudentGrupa_Grupa"),
                j =>
                {
                    j.HasKey("GrupaIdGrupa", "StudentOsobaSIndex")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    j.ToTable("Student_Grupa");
                    j.HasIndex(new[] { "StudentOsobaSIndex" }, "Student_Grupa_Student");
                    j.IndexerProperty<int>("GrupaIdGrupa")
                        .HasColumnType("int(11)")
                        .HasColumnName("Grupa_id_Grupa");
                    j.IndexerProperty<int>("StudentOsobaSIndex")
                        .HasColumnType("int(11)")
                        .HasColumnName("Student_Osoba_S_index");
                });
    });

    // Repeat similar cascade delete configurations for other entities as needed
    modelBuilder.Entity<Osoba>(entity =>
    {
        entity.HasKey(e => e.SIndex).HasName("PRIMARY");

        entity.ToTable("Osoba");

        entity.Property(e => e.SIndex)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("S_index");
        entity.Property(e => e.Name).HasMaxLength(15);
        entity.Property(e => e.Surname).HasMaxLength(15);
    });

    modelBuilder.Entity<Przedmiot>(entity =>
    {
        entity.HasKey(e => e.IdPrzedmiot).HasName("PRIMARY");

        entity.ToTable("Przedmiot");

        entity.Property(e => e.IdPrzedmiot)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("id_Przedmiot");
        entity.Property(e => e.Description).HasMaxLength(80);
        entity.Property(e => e.Name).HasMaxLength(10);

        entity.HasMany(d => d.SemestrIdSemestrs).WithMany(p => p.PrzedmiotIdPrzedmiots)
            .UsingEntity<Dictionary<string, object>>(
                "PrzedmiotSemestr",
                r => r.HasOne<Semestr>().WithMany()
                    .HasForeignKey("SemestrIdSemestr")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Przedmiot_Semestr_Semestr"),
                l => l.HasOne<Przedmiot>().WithMany()
                    .HasForeignKey("PrzedmiotIdPrzedmiot")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Przedmiot_Semestr_Przedmiot"),
                j =>
                {
                    j.HasKey("PrzedmiotIdPrzedmiot", "SemestrIdSemestr")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    j.ToTable("Przedmiot_Semestr");
                    j.HasIndex(new[] { "SemestrIdSemestr" }, "Przedmiot_Semestr_Semestr");
                    j.IndexerProperty<int>("PrzedmiotIdPrzedmiot")
                        .HasColumnType("int(11)")
                        .HasColumnName("Przedmiot_id_Przedmiot");
                    j.IndexerProperty<int>("SemestrIdSemestr")
                        .HasColumnType("int(11)")
                        .HasColumnName("Semestr_id_semestr");
                });
    });

    modelBuilder.Entity<Semestr>(entity =>
    {
        entity.HasKey(e => e.IdSemestr).HasName("PRIMARY");

        entity.ToTable("Semestr");

        entity.Property(e => e.IdSemestr)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("id_semestr");
    });

    modelBuilder.Entity<Student>(entity =>
    {
        entity.HasKey(e => e.OsobaSIndex).HasName("PRIMARY");

        entity.ToTable("Student");

        entity.Property(e => e.OsobaSIndex)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("Osoba_S_index");
        entity.Property(e => e.Password).HasMaxLength(20);
        entity.Property(e => e.YearOfStudy).HasColumnName("Year_Of_Study");

        entity.HasOne(d => d.OsobaSIndexNavigation).WithOne(p => p.Student)
            .HasForeignKey<Student>(d => d.OsobaSIndex)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("Student_Osoba");
    });

    modelBuilder.Entity<Zajecium>(entity =>
    {
        entity.HasKey(e => e.IdZajecia).HasName("PRIMARY");

        entity.HasIndex(e => e.GrupaIdGrupa, "Zajecia_Grupa");

        entity.HasIndex(e => e.PrzedmiotIdPrzedmiot, "Zajecia_Przedmiot");

        entity.Property(e => e.IdZajecia)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("id_Zajecia");
        entity.Property(e => e.Duration).HasColumnType("time");
        entity.Property(e => e.EndZajecia)
            .HasDefaultValueSql("'0000-00-00 00:00:00'")
            .HasColumnType("timestamp")
            .HasColumnName("End_Zajecia");
        entity.Property(e => e.GrupaIdGrupa)
            .HasColumnType("int(11)")
            .HasColumnName("Grupa_id_Grupa");
        entity.Property(e => e.NumberAuditory)
            .HasColumnType("int(11)")
            .HasColumnName("Number_Auditory");
        entity.Property(e => e.PrzedmiotIdPrzedmiot)
            .HasColumnType("int(11)")
            .HasColumnName("Przedmiot_id_Przedmiot");
        entity.Property(e => e.StartZajecia)
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp")
            .HasColumnName("Start_Zajecia");
        entity.Property(e => e.Theme).HasMaxLength(20);
        entity.Property(e => e.Type).HasColumnType("int(11)");

        entity.HasOne(d => d.GrupaIdGrupaNavigation).WithMany(p => p.Zajecia)
            .HasForeignKey(d => d.GrupaIdGrupa)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("Zajecia_Grupa");

        entity.HasOne(d => d.PrzedmiotIdPrzedmiotNavigation).WithMany(p => p.Zajecia)
            .HasForeignKey(d => d.PrzedmiotIdPrzedmiot)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("Zajecia_Przedmiot");
    });

    OnModelCreatingPartial(modelBuilder);
}


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
