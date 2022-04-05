using AKS.Shared.Payroll.Models;
using AKS.Shared.Payrolls.ViewModels;
using AutoMapper;

namespace AKS.ParyollSystem.AutoMappers
{
    public class PayrollProfile : Profile
    {
        public PayrollProfile()
        {
            // Default mapping when property names are same
            CreateMap<Employee, EmployeeVM>();
            CreateMap<Attendance, AttendanceVM>();
            CreateMap<EmployeeDetailVM, EmployeeDetailVM>();
            CreateMap<MonthlyAttendance, MonthlyAttendanceVM>();

            CreateMap<MonthlyAttendanceVM, MonthlyAttendanceVM>();
            CreateMap<EmployeeDetailVM, EmployeeDetails>();
            CreateMap<AttendanceVM, Attendance>();
            CreateMap<EmployeeVM, Employee>();

            // Mapping when property names are different
            //CreateMap<User, UserViewModel>()
            //    .ForMember(dest =>
            //    dest.FName,
            //    opt => opt.MapFrom(src => src.FirstName))
            //    .ForMember(dest =>
            //    dest.LName,
            //    opt => opt.MapFrom(src => src.LastName));
        }
    }
}
