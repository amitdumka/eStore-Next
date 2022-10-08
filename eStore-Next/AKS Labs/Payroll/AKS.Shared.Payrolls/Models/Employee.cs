using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Shared.Payroll.Models
{
    [Table("V1_MonthlyAttendances")]
    public class MonthlyAttendance : BaseST
    {
        [Key]
        public string MonthlyAttendanceId { get; set; }

        public string EmployeeId { get; set; }
        public DateTime OnDate { get; set; }
        public virtual Employee Employee { get; set; }

        //Postive
        public int Present { get; set; }

        public int HalfDay { get; set; }
        public int Sunday { get; set; }
        public int PaidLeave { get; set; }
        public int Holidays { get; set; }

        //Negative
        public int CasualLeave { get; set; }

        public int Absent { get; set; }
        public int WeeklyLeave { get; set; }

        public string Remarks { get; set; }
        public int NoOfWorkingDays { get; set; }
        //public decimal BillableDays { get; set; }

        public int DayInMonths
        { get { return (DateTime.DaysInMonth(OnDate.Year, OnDate.Month)); } }

        public int Count
        { get { return Present + HalfDay + Sunday + PaidLeave + CasualLeave + Absent + WeeklyLeave + Holidays; } }

        public decimal BillableDays => (decimal)((HalfDay / 2.0m) + 0.0m) + ((Present + Sunday + PaidLeave + Holidays) + 0.0m);

        public bool Valid
        { get { return (Count == DayInMonths); } }
    }

    [Table("V1_Attendances")]
    public class Attendance : BaseST
    {
        [Key]//TODO: need to mention min length and max length
        public string AttendanceId { get; set; }

        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public DateTime OnDate { get; set; }
        public AttUnit Status { get; set; }
        public string EntryTime { get; set; }
        public string Remarks { get; set; }

        [Display(Name = "Tailor")]
        public bool IsTailoring { get; set; }
    }

    public class EmployeeDetails : BaseST
    {
        [Key]
        public string EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        public string AdharNumber { get; set; }
        public string PanNo { get; set; }
        public string OtherIdDetails { get; set; }

        public string FatherName { get; set; }
        public string MaritalStatus { get; set; }
        public string SpouseName { get; set; }
        public string HighestQualification { get; set; }

        public string BankAccountNumber { get; set; }
        public string BankNameWithBranch { get; set; }
        public string IFSCode { get; set; }

    }

    [Table("V1_Employees")]
    public class Employee : Person
    {
        [Key]
        public string EmployeeId { get; set; }

        public int EmpId { get; set; } // Temp Till full migratin is done.

        [Display(Name = "Employee Name")]
        public string StaffName
        { get { return (FirstName + " " + LastName).Trim(); } }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Leaving Date")]
        public DateTime? LeavingDate { get; set; }

        [Display(Name = "Working")]
        public bool IsWorking { get; set; }

        [Display(Name = "Job Category")]
        [DefaultValue(0)]
        public EmpType Category { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Tailoring Division")]
        public bool IsTailors { get; set; }

        [Required]
        public string StoreId { get; set; }

        public virtual Store Store { get; set; }
        public bool MarkedDeleted { get; set; }
    }

    [Table("V1_Salaries")]
    public class Salary : BaseST
    {
        [Key]
        public string SalaryId { get; set; }

        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BasicSalary { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CloseDate { get; set; }

        public bool IsEffective { get; set; }

        public bool WowBill { get; set; }
        public bool Incentive { get; set; }
        public bool LastPcs { get; set; }
        public bool SundayBillable { get; set; }
        public bool FullMonth { get; set; }

        [DefaultValue(false)]
        public bool IsTailoring { get; set; }
    }

    [Table("V1_SalaryPayments")]
    public class SalaryPayment : BaseST
    {
        public string SalaryPaymentId { get; set; }

        [Display(Name = "Staff Name")]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; }

        [Display(Name = "Salary/Year(021992)")]
        public int SalaryMonth { get; set; }

        [Display(Name = "Payment Reason")]
        public SalaryComponet SalaryComponet { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime OnDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PayMode PayMode { get; set; }

        public string Details { get; set; }
    }

    [Table("V1_StaffAdvanceReceipts")]
    public class StaffAdvanceReceipt : BaseST
    {
        [Key]
        public string StaffAdvanceReceiptId { get; set; }

        [Display(Name = "Staff Name")]
        public string EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receipt Date")]
        public DateTime OnDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PayMode PayMode { get; set; }

        public string Details { get; set; }
    }

    //TODO: use from payslip report one. for better use
    [Table("V1_PaySlips")]
    public class PaySlip : BaseST
    {
        public string PaySlipId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OnDate { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }

        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public string? SalaryId { get; set; }
        public virtual Salary CurrentSalary { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BasicSalaryRate { get; set; }


        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BasicSalary { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalPayableSalary { get; set; }

        public decimal NoOfDaysPresent { get; set; }
        public int WorkingDays { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalSale { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal SaleIncentive { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal WOWBillAmount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal WOWBillIncentive { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal LastPcsAmount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal LastPCsIncentive { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal OthersIncentive { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal GrossSalary { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal StandardDeductions { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TDSDeductions { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal PFDeductions { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal AdvanceDeducations { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal OtherDeductions { get; set; }

        public string Remarks { get; set; }

        public bool? IsTailoring { get; set; }
    }

    public class SalaryLedger : Base
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public DateTime OnDate { get; set; }
        public string Particulars { get; set; }
        public decimal InAmount { get; set; }
        public decimal OutAmount { get; set; }

    }
    public class TimeSheet : Base
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public DateTime OutTime { get; set; }
        public DateTime? InTime { get; set; }
        public string Reason { get; set; }
        public virtual Employee Employee { get; set; }
        public double Duration { get { return ((InTime.HasValue ? InTime.Value : DateTime.Now) - OutTime).TotalMinutes; } }

    }

}