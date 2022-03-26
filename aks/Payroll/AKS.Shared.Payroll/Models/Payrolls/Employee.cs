using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Shared.Payroll.Models.Payrolls
{
    public class Employee:Person
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        [Display(Name = "Employee Name")]
        public string StaffName { get { return (FirstName + " " + LastName).Trim(); } }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Leaving Date")]
        public DateTime? LeavingDate { get; set; }

        [Display(Name = "Working")]
        public bool IsWorking { get; set; }

    }
}
