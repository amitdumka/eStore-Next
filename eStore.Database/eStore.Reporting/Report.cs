namespace eStore.Reporting;

//TODO: move to enum type files
public enum ReportType { PDF,Excel,Screen,Email}
public class Report
{
    public string ReportName { get; set; }
    public DateTime OnDate { get; set; }
    public ReportType ReportType { get; set; }
    public string FilePath { get; set; }

}

