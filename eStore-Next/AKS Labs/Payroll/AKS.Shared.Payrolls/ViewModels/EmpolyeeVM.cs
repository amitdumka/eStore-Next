﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Shared.Payrolls.ViewModels
{
    public class AttendanceVM
    {
        public string AttendanceId { get; set; }
        public DateTime OnDate { get; set; }
        public string EmployeeId { get; set; }
        public string StaffName { get; set; }

        public string EntryTime { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "Tailor")]
        public bool IsTailoring { get; set; }

        public AttUnit Status { get; set; }
        public string StoreId { get; set; }
    }

    public class EmployeeDetailVM : EmployeeVM
    {
        public string EmployeeId { get; set; }

        public string AdharNumber { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        public string FatherName { get; set; }
        public string HighestQualification { get; set; }

        public string MaritalStatus { get; set; }
        public string OtherIdDetails { get; set; }
        public string PanNo { get; set; }
        public string SpouseName { get; set; }
    }

    public class PersonVM : AddressVM
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; } // Enum Gender
        public DateTime DOB { get; set; }
    }

    public class AddressVM
    {
        public string AddressLine { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }

    public class EmployeeVM : PersonVM
    {
        public string EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string StaffName { get; set; }

        [Display(Name = "Job Category")]
        [DefaultValue(0)]
        public EmpType Category { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Tailoring Division")]
        public bool IsTailors { get; set; }

        [Display(Name = "Working")]
        public bool IsWorking { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Leaving Date")]
        public DateTime? LeavingDate { get; set; }
        public string StoreId { get; set; }
    }

    public class MonthlyAttendanceVM
    {
        public string MonthlyAttendanceId { get; set; }
        public string EmployeeId { get; set; }
        public string StaffName { get; set; }

        public int Absent { get; set; }
        public decimal BillableDays { get; set; }
        public int CasualLeave { get; set; }

        public int HalfDay { get; set; }
        public int Holidays { get; set; }
       
        public int NoOfWorkingDays { get; set; }
        public DateTime OnDate { get; set; }
        public int PaidLeave { get; set; }
        public int Present { get; set; }
        public string Remarks { get; set; }
        public int Sunday { get; set; }
        public int WeeklyLeave { get; set; }
    }



    public class SalaryPaymentVM 
    {
        public string SalaryPaymentId { get; set; }

        [Display(Name = "Staff Name")]
        public string EmployeeId { get; set; }

        public string StaffName { get; set; }

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
        public string StoreId { get; set; }

    }
    public class StaffAdvanceReceiptVM
    {
        [Key]
        public string StaffAdvanceReceiptId { get; set; }

        [Display(Name = "Staff Name")]
        public string EmployeeId { get; set; }

        public string StaffName{ get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receipt Date")]
        public DateTime ReceiptDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PayMode PayMode { get; set; }

        public string Details { get; set; }
        public string StoreId { get; set; }
    }


}