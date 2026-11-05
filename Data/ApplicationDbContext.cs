using FixTrack.Web.Models;
using FixTrack.Web.Models.Reports;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FixTrack.Web.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Report> Reports => Set<Report>();
    public DbSet<ReportCategory> ReportCategories => Set<ReportCategory>();
    public DbSet<ReportPhoto> ReportPhotos => Set<ReportPhoto>();
    public DbSet<ReportStatusHistory> ReportStatusHistories => Set<ReportStatusHistory>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // Retains all ASP.NET Core Identity mappings.

        builder.Entity<ReportCategory>(entity =>
        {
            entity.Property(x => x.Name).HasMaxLength(120).IsRequired();
            entity.HasIndex(x => x.Name).IsUnique();
        });

        builder.Entity<Report>(entity =>
        {
            entity.Property(x => x.TrackingNumber).HasMaxLength(32).IsRequired();
            entity.HasIndex(x => x.TrackingNumber).IsUnique();
            entity.Property(x => x.Title).HasMaxLength(180).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(4000).IsRequired();
            entity.Property(x => x.LocationDescription).HasMaxLength(500).IsRequired();
            entity.Property(x => x.Latitude).HasPrecision(9, 6);
            entity.Property(x => x.Longitude).HasPrecision(9, 6);
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.Priority).HasConversion<string>().HasMaxLength(16);
            entity.Property(x => x.OfficialRemarks).HasMaxLength(2000);
            entity.HasIndex(x => new { x.ResidentId, x.CreatedAtUtc });
            entity.HasIndex(x => x.Status);

            entity.HasOne(x => x.Resident)
                .WithMany()
                .HasForeignKey(x => x.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.AssignedCrew)
                .WithMany()
                .HasForeignKey(x => x.AssignedCrewId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.Category)
                .WithMany(x => x.Reports)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.DuplicateOfReport)
                .WithMany()
                .HasForeignKey(x => x.DuplicateOfReportId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ReportPhoto>(entity =>
        {
            entity.Property(x => x.StoragePath).HasMaxLength(1024).IsRequired();
            entity.Property(x => x.PhotoType).HasConversion<string>().HasMaxLength(24);
            entity.HasOne(x => x.Report)
                .WithMany(x => x.Photos)
                .HasForeignKey(x => x.ReportId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ReportStatusHistory>(entity =>
        {
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.Note).HasMaxLength(2000);
            entity.HasIndex(x => new { x.ReportId, x.ChangedAtUtc });
            entity.HasOne(x => x.Report)
                .WithMany(x => x.StatusHistory)
                .HasForeignKey(x => x.ReportId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.ChangedByUser)
                .WithMany()
                .HasForeignKey(x => x.ChangedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
