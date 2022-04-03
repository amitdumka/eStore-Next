using AKS.Shared.Payroll.Models;
using AKS.Shared.Payrolls.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKS.Payroll.DTOMapping
{
    public  class DMMapper
    {
        public static Mapper Mapper { get; set; }
        public static Mapper InitializeAutomapper()
        {
            if(Mapper == null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EmployeeDetails, EmployeeDetailVM>()
                    .ForMember(dest => dest.StaffName, act => act.MapFrom(src => src.Employee.StaffName))
                    .ForMember(dest => dest.JoiningDate, act => act.MapFrom(src => src.Employee.JoiningDate))
                    .ForMember(dest => dest.LeavingDate, act => act.MapFrom(src => src.Employee.LeavingDate))
                    .ForMember(dest => dest.Category, act => act.MapFrom(src => src.Employee.Category))
                    .ForMember(dest => dest.IsWorking, act => act.MapFrom(src => src.Employee.IsWorking))
                    .ForMember(dest => dest.IsTailors, act => act.MapFrom(src => src.Employee.IsTailors))

                    .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Employee.Title))
                    .ForMember(dest => dest.FirstName, act => act.MapFrom(src => src.Employee.FirstName))
                    .ForMember(dest => dest.LastName, act => act.MapFrom(src => src.Employee.LastName))
                    .ForMember(dest => dest.StreetName, act => act.MapFrom(src => src.Employee.StreetName))
                    .ForMember(dest => dest.AddressLine, act => act.MapFrom(src => src.Employee.AddressLine))
                    .ForMember(dest => dest.Gender, act => act.MapFrom(src => src.Employee.Gender))
                    .ForMember(dest => dest.City, act => act.MapFrom(src => src.Employee.City))
                    .ForMember(dest => dest.Country, act => act.MapFrom(src => src.Employee.Country))
                    .ForMember(dest => dest.DOB, act => act.MapFrom(src => src.Employee.DOB))
                    .ForMember(dest => dest.State, act => act.MapFrom(src => src.Employee.State))

                    .ForMember(dest => dest.ZipCode, act => act.MapFrom(src => src.Employee.ZipCode))
                    ;
                    cfg.CreateMap<Employee, EmployeeVM>();
                    cfg.CreateMap<Attendance, AttendanceVM>();
                    cfg.CreateMap<AttendanceVM, Attendance>();
                    cfg.CreateMap<MonthlyAttendance, MonthlyAttendanceVM>();
                    cfg.CreateMap<MonthlyAttendanceVM, MonthlyAttendance>();
                    cfg.CreateMap<SalaryPayment, SalaryPaymentVM>().ForMember(dest => dest.StaffName, act => act.MapFrom(src => src.Employee.StaffName));
                    cfg.CreateMap<SalaryPaymentVM, SalaryPayment>();
                    cfg.CreateMap<StaffAdvanceReceipt, StaffAdvanceReceiptVM>().ForMember(dest => dest.StaffName, act => act.MapFrom(src => src.Employee.StaffName));
                    cfg.CreateMap<StaffAdvanceReceiptVM, StaffAdvanceReceipt>();
                });
                Mapper = new Mapper(config);
            }
           
            return Mapper;
        }
    }
}
