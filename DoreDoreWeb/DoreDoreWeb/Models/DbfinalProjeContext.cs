using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DoreDoreWeb.Models;

public partial class DbfinalProjeContext : DbContext
{
    public DbfinalProjeContext()
    {
    }

    public DbfinalProjeContext(DbContextOptions<DbfinalProjeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<ActorFilm> ActorFilms { get; set; }

    public virtual DbSet<Afi> Afis { get; set; }

    public virtual DbSet<AfisFilmId> AfisFilmIds { get; set; }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentsUser> CommentsUsers { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<DirectorFilm> DirectorFilms { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<FilmCommed> FilmCommeds { get; set; }

    public virtual DbSet<FilmFragman> FilmFragmen { get; set; }

    public virtual DbSet<Fragman> Fragmen { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    public virtual DbSet<TypeFilm> TypeFilms { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=DBFinalProje;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.ToTable("Actor");

            entity.Property(e => e.ActorId).HasColumnName("ActorID");
            entity.Property(e => e.ActorFotoDosyaYolu)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.ActorName)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.ActorSurname)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        modelBuilder.Entity<ActorFilm>(entity =>
        {
            entity.ToTable("ActorFilm");

            entity.Property(e => e.ActorFilmId).HasColumnName("ActorFilmID");
            entity.Property(e => e.ActorId).HasColumnName("ActorID");
            entity.Property(e => e.ActorRol).HasMaxLength(20);
            entity.Property(e => e.CharacterId).HasColumnName("CharacterID");
            entity.Property(e => e.FilmId).HasColumnName("FilmID");

            entity.HasOne(d => d.Actor).WithMany(p => p.ActorFilms)
                .HasForeignKey(d => d.ActorId)
                .HasConstraintName("FK_ActorFilm_Actor");

            entity.HasOne(d => d.Character).WithMany(p => p.ActorFilms)
                .HasForeignKey(d => d.CharacterId)
                .HasConstraintName("FK_ActorFilm_Character");

            entity.HasOne(d => d.Film).WithMany(p => p.ActorFilms)
                .HasForeignKey(d => d.FilmId)
                .HasConstraintName("FK_ActorFilm_Film");
        });

        modelBuilder.Entity<Afi>(entity =>
        {
            entity.HasKey(e => e.AfisId);

            entity.Property(e => e.AfisId)
                .ValueGeneratedNever()
                .HasColumnName("AfisID");
            entity.Property(e => e.AfisDosyaYolu).HasMaxLength(300);
            entity.Property(e => e.AfisName).HasMaxLength(30);
        });

        modelBuilder.Entity<AfisFilmId>(entity =>
        {
            entity.HasKey(e => e.AfisFilmId1);

            entity.ToTable("AfisFilmID");

            entity.Property(e => e.AfisFilmId1)
                .ValueGeneratedNever()
                .HasColumnName("AfisFilmID");
            entity.Property(e => e.AfisId).HasColumnName("AfisID");
            entity.Property(e => e.FilmId).HasColumnName("FilmID");

            entity.HasOne(d => d.Afis).WithMany(p => p.AfisFilmIds)
                .HasForeignKey(d => d.AfisId)
                .HasConstraintName("FK_AfisFilmID_Afis");

            entity.HasOne(d => d.Film).WithMany(p => p.AfisFilmIds)
                .HasForeignKey(d => d.FilmId)
                .HasConstraintName("FK_AfisFilmID_Film");
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.ToTable("Character");

            entity.Property(e => e.CharacterId).HasColumnName("CharacterID");
            entity.Property(e => e.CharacterBDate).HasColumnName("CharacterB_Date");
            entity.Property(e => e.CharacterName).HasMaxLength(30);
            entity.Property(e => e.CharacterSorName).HasMaxLength(30);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommetId);

            entity.Property(e => e.CommetId).HasColumnName("CommetID");
            entity.Property(e => e.CommentText).HasMaxLength(300);
        });

        modelBuilder.Entity<CommentsUser>(entity =>
        {
            entity.HasKey(e => e.CommetUserId).HasName("PK_CombinatiyonCUF");

            entity.ToTable("CommentsUser");

            entity.Property(e => e.CommetUserId).HasColumnName("CommetUserID");
            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentsUsers)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FK_CommentsUser_Comments");

            entity.HasOne(d => d.User).WithMany(p => p.CommentsUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_CommentsUser_Users");
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.ToTable("Director");

            entity.Property(e => e.DirectorId).HasColumnName("DirectorID");
            entity.Property(e => e.DirectorName).HasMaxLength(20);
            entity.Property(e => e.DirectorSurName).HasMaxLength(20);
        });

        modelBuilder.Entity<DirectorFilm>(entity =>
        {
            entity.ToTable("DirectorFilm");

            entity.Property(e => e.DirectorFilmId).HasColumnName("DirectorFilmID");
            entity.Property(e => e.DirectorId).HasColumnName("DirectorID");
            entity.Property(e => e.FilmId).HasColumnName("FilmID");

            entity.HasOne(d => d.Director).WithMany(p => p.DirectorFilms)
                .HasForeignKey(d => d.DirectorId)
                .HasConstraintName("FK_DirectorFilm_Director");

            entity.HasOne(d => d.Film).WithMany(p => p.DirectorFilms)
                .HasForeignKey(d => d.FilmId)
                .HasConstraintName("FK_DirectorFilm_Film");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.ToTable("Film");

            entity.Property(e => e.FilmId).HasColumnName("FilmID");
            entity.Property(e => e.FilmDescriptiyon).HasMaxLength(300);
            entity.Property(e => e.FilmDosyaYolu).HasMaxLength(100);
            entity.Property(e => e.FilmImageDosyaYolu).HasMaxLength(100);
            entity.Property(e => e.FilmImdb).HasColumnName("FilmIMDB");
            entity.Property(e => e.FilmName).HasMaxLength(40);
        });

        modelBuilder.Entity<FilmCommed>(entity =>
        {
            entity.HasKey(e => e.FilmCommendsId);

            entity.Property(e => e.FilmCommendsId).HasColumnName("FilmCommendsID");
            entity.Property(e => e.CommentsUserId).HasColumnName("CommentsUserID");
            entity.Property(e => e.FilmId).HasColumnName("FilmID");

            entity.HasOne(d => d.CommentsUser).WithMany(p => p.FilmCommeds)
                .HasForeignKey(d => d.CommentsUserId)
                .HasConstraintName("FK_FilmCommeds_CommentsUser");

            entity.HasOne(d => d.Film).WithMany(p => p.FilmCommeds)
                .HasForeignKey(d => d.FilmId)
                .HasConstraintName("FK_FilmCommeds_Film");
        });

        modelBuilder.Entity<FilmFragman>(entity =>
        {
            entity.ToTable("FilmFragman");

            entity.Property(e => e.FilmFragmanId).HasColumnName("FilmFragmanID");
            entity.Property(e => e.FilmId).HasColumnName("FilmID");
            entity.Property(e => e.FragmanId).HasColumnName("FragmanID");

            entity.HasOne(d => d.Film).WithMany(p => p.FilmFragmen)
                .HasForeignKey(d => d.FilmId)
                .HasConstraintName("FK_FilmFragman_Film");

            entity.HasOne(d => d.Fragman).WithMany(p => p.FilmFragmen)
                .HasForeignKey(d => d.FragmanId)
                .HasConstraintName("FK_FilmFragman_Fragman");
        });

        modelBuilder.Entity<Fragman>(entity =>
        {
            entity.ToTable("Fragman");

            entity.Property(e => e.FragmanId).HasColumnName("FragmanID");
            entity.Property(e => e.FilmFragmanYol).HasMaxLength(100);
            entity.Property(e => e.FragmanDescription).HasMaxLength(100);
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.ToTable("Type");

            entity.Property(e => e.TypeId)
                .ValueGeneratedNever()
                .HasColumnName("TypeID");
            entity.Property(e => e.TypeName).HasMaxLength(20);
        });

        modelBuilder.Entity<TypeFilm>(entity =>
        {
            entity.ToTable("TypeFilm");

            entity.Property(e => e.TypeFilmId).HasColumnName("TypeFilmID");
            entity.Property(e => e.FilmId).HasColumnName("FilmID");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Film).WithMany(p => p.TypeFilms)
                .HasForeignKey(d => d.FilmId)
                .HasConstraintName("FK_TypeFilm_Film");

            entity.HasOne(d => d.Type).WithMany(p => p.TypeFilms)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_TypeFilm_Type");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.ProfilFoto).HasMaxLength(100);
            entity.Property(e => e.UserEposta).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserPassaworld).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
