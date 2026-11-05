namespace FixTrack.Web.Models.Reports;

public class ReportPhoto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ReportId { get; set; }
    public Report Report { get; set; } = null!;
    public string StoragePath { get; set; } = string.Empty;
    public ReportPhotoType PhotoType { get; set; } = ReportPhotoType.Submitted;
    public DateTime UploadedAtUtc { get; set; } = DateTime.UtcNow;
}
