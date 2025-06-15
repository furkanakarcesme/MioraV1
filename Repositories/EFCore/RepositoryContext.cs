using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public RepositoryContext(DbContextOptions options) :
            base(options)
        {

        }
        
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<PdfUpload> PdfUploads { get; set; }
        public DbSet<XRayUpload> XRayUploads { get; set; }
        public DbSet<LabObservation> LabObservations { get; set; }
        public DbSet<AnalysisResult> AnalysisResults { get; set; }
        public DbSet<AiPromptLog> AiPromptLogs { get; set; }
        public DbSet<ChatSession> ChatSessions { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AppointmentConfig());
            modelBuilder.ApplyConfiguration(new AvailabilityConfig());
            modelBuilder.ApplyConfiguration(new CityConfig());
            modelBuilder.ApplyConfiguration(new ClinicConfig());
            modelBuilder.ApplyConfiguration(new ClinicHospitalConfig());
            modelBuilder.ApplyConfiguration(new DistrictConfig());
            modelBuilder.ApplyConfiguration(new HospitalConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

            modelBuilder.Entity<UploadBase>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            modelBuilder.Entity<UploadBase>()
                .Property(u => u.FileName).HasMaxLength(255);
            
            modelBuilder.Entity<UploadBase>()
                .Property(u => u.FilePath).HasMaxLength(1024);

            modelBuilder.Entity<AnalysisResult>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Type).HasConversion<string>().HasMaxLength(50);
                entity.Property(e => e.RawJson).IsRequired();
            });

            modelBuilder.Entity<LabObservation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Status).HasConversion<string>().HasMaxLength(50);
                entity.HasOne(e => e.Analysis)
                    .WithMany(ar => ar.LabObservations)
                    .HasForeignKey(e => e.AnalysisId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AiPromptLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Analysis)
                    .WithMany(ar => ar.AiPromptLogs)
                    .HasForeignKey(e => e.AnalysisId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            modelBuilder.Entity<ChatSession>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Analysis)
                    .WithMany()
                    .HasForeignKey(e => e.AnalysisId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Text).IsRequired();
                entity.HasOne(e => e.Session)
                    .WithMany(cs => cs.ChatMessages)
                    .HasForeignKey(e => e.SessionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}