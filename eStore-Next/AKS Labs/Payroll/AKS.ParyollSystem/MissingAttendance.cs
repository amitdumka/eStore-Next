namespace AKS.ParyollSystem
{
    //TODO: move to DTO's

    public class MissingAttendance
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime? LeavingDate { get; set; }
        public List<DateTime> MissingDates { get; set; }
        public List<DateTime> DuplicateDates { get; set; }
        public bool Found { get; set; }
        public bool Duplicates { get; set; }
    }
}

//// Initialize diff
//int diffD = days[0] - 0;
//for (int i = 0; i < noOyDays; i++)
//{
//    // Check if diff and days[i]-i
//    // both are equal or not
//    if (days[i] - i != diffD)
//    {
//        // Loop for consecutive
//        // missing elements
//        while (diffD < days[i] - i)
//        {
//            missing.MissingDates.Add(new DateTime(month.Year, month.Month, i + diffD));
//            diffD++;
//        }
//    }
//}