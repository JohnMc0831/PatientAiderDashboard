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
        {}

        public virtual DbSet<Encounters> Encounters { get; set; }
        public virtual DbSet<Footnotes> Footnotes { get; set; }
        public virtual DbSet<Icons> Icons { get; set; }
        public virtual DbSet<Sections> Sections { get; set; }
        public virtual DbSet<SectionsXtopics> SectionsXtopics { get; set; }
        public virtual DbSet<Topics> Topics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(Startup.PatientAiderConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
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
        }
    }
}
