using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PatientAiderDashboard.Models
{
    public partial class PatientAiderContext : DbContext
    {
        public PatientAiderContext()
        {
        }

        public PatientAiderContext(DbContextOptions<PatientAiderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Encounters> Encounters { get; set; }
        public virtual DbSet<Footnotes> Footnotes { get; set; }
        public virtual DbSet<Icons> Icons { get; set; }
        public virtual DbSet<Links> Links { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<References> References { get; set; }
        public virtual DbSet<Sections> Sections { get; set; }
        public virtual DbSet<SectionsXtopics> SectionsXtopics { get; set; }
        public virtual DbSet<Topics> Topics { get; set; }
        public virtual DbSet<TopicsLinks> TopicsLinks { get; set; }
        public virtual DbSet<TopicsMedia> TopicsMedia { get; set; }
        public virtual DbSet<TopicsReferences> TopicsReferences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=.\\SQLEXPRESS;Database=PatientSafeDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<References>(entity =>
            {
                entity.HasOne(d => d.Link)
                    .WithMany(p => p.References)
                    .HasForeignKey(d => d.LinkId)
                    .HasConstraintName("FK_References_Links");
            });

            modelBuilder.Entity<Sections>(entity =>
            {
                entity.HasOne(d => d.Encounter)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.EncounterId)
                    .HasConstraintName("FK_Sections_Encounters");
            });

            modelBuilder.Entity<SectionsXtopics>(entity =>
            {
                entity.HasKey(e => new { e.SectionId, e.TopicId });

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.SectionsXtopics)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SectionsXTopics_Sections");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.SectionsXtopics)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SectionsXTopics_Topics");
            });

            modelBuilder.Entity<TopicsLinks>(entity =>
            {
                entity.HasKey(e => new { e.TopicId, e.LinkId });

                entity.HasOne(d => d.Link)
                    .WithMany(p => p.TopicsLinks)
                    .HasForeignKey(d => d.LinkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TopicsLinks_Links");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.TopicsLinks)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TopicsLinks_Topics");
            });

            modelBuilder.Entity<TopicsMedia>(entity =>
            {
                entity.HasKey(e => new { e.TopicId, e.MediaId });

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.TopicsMedia)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TopicsMedia_Media");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.TopicsMedia)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TopicsMedia_Topics");
            });

            modelBuilder.Entity<TopicsReferences>(entity =>
            {
                entity.HasKey(e => new { e.TopicId, e.ReferenceId });

                entity.HasOne(d => d.Reference)
                    .WithMany(p => p.TopicsReferences)
                    .HasForeignKey(d => d.ReferenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TopicsReferences_References");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.TopicsReferences)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TopicsReferences_Topics");
            });
        }
    }
}
