namespace AKS.ParyollSystem.Dtos;

public class PaySlipDTO
{
    public string EmployeeId { get; set; }

    public DateTime OnDate { get; set; }
    public DateTime GenerationDate { get; set; }

    public int NoOfWorkingDays { get; set; }
    public decimal Absent { get; set; }
    public decimal Present { get; set; }
    public decimal Sunday { get; set; }
    public decimal HalfDay { get; set; }
    public decimal PaidLeave { get; set; }
    public decimal WeeklyLeave { get; set; }
    public int NoOfAttendance { get; set; }
    //public int NoOfAttendance
    //{ get { return (int)(Absent + PaidLeave + Present + Sunday + HalfDay); } }

    public decimal BillableDays { get; set; }

    public decimal SalaryPerDay { get; set; }
    public decimal NetSalary { get; set; }

    public decimal GrossSalary { get; set; }
    public string Remarks { get; set; }
}

public class PaySlipsDTO
{
    public string EmployeeId { get; set; }

    public int SYear { get; set; }
    public int EYear { get; set; }

    public PaySlipDTO Jan { get; set; }
    public PaySlipDTO Feb { get; set; }
    public PaySlipDTO Mar { get; set; }
    public PaySlipDTO April { get; set; }
    public PaySlipDTO May { get; set; }
    public PaySlipDTO June { get; set; }
    public PaySlipDTO July { get; set; }
    public PaySlipDTO Aug { get; set; }
    public PaySlipDTO Sept { get; set; }

    public PaySlipDTO Oct { get; set; }
    public PaySlipDTO Nov { get; set; }
    public PaySlipDTO Dec { get; set; }
}