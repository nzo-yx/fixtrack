namespace FixTrack.Web.Models.Reports;

public class ReportCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public ICollection<Report> Reports { get; set; } = new List<Report>();
}
