namespace FixTrack.Web.Models.Reports;

public class ReportStatusHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ReportId { get; set; }
    public Report Report { get; set; } = null!;
    public ReportStatus Status { get; set; }
    public string? Note { get; set; }
    public string? ChangedByUserId { get; set; }
    public ApplicationUser? ChangedByUser { get; set; }
    public DateTime ChangedAtUtc { get; set; } = DateTime.UtcNow;
}
