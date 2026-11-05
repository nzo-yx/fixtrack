namespace FixTrack.Web.Models.Reports;

public class Report
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string TrackingNumber { get; set; } = string.Empty;

    public string ResidentId { get; set; } = string.Empty;
    public ApplicationUser Resident { get; set; } = null!;

    public int CategoryId { get; set; }
    public ReportCategory Category { get; set; } = null!;

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LocationDescription { get; set; } = string.Empty;
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }

    public ReportStatus Status { get; set; } = ReportStatus.Submitted;
    public ReportPriority? Priority { get; set; }
    public string? AssignedCrewId { get; set; }
    public ApplicationUser? AssignedCrew { get; set; }
    public DateTime? DueAtUtc { get; set; }
    public string? OfficialRemarks { get; set; }

    public Guid? DuplicateOfReportId { get; set; }
    public Report? DuplicateOfReport { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;

    public ICollection<ReportPhoto> Photos { get; set; } = new List<ReportPhoto>();
    public ICollection<ReportStatusHistory> StatusHistory { get; set; } = new List<ReportStatusHistory>();
}
