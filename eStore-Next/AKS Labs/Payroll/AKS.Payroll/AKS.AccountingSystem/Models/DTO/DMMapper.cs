using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Commons.ViewModels.Accounts;
using AKS.Shared.Payroll.Models;
using AKS.Shared.Payrolls.ViewModels;
using AutoMapper;

namespace AKS.AccountingSystem.DTO
{
    public class DMMapper
    {
        public static Mapper Mapper { get; set; }

        public static Mapper InitializeAutomapper()
        {
            if (Mapper == null)
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
                    cfg.CreateMap<CashVoucher, CashVoucherVM>()
                    .ForMember(dest => dest.TranscationName, act => act.MapFrom(src => src.TranscationMode.TranscationName))
                    .ForMember(dest => dest.Party, act => act.MapFrom(src => src.Partys.PartyName))
                    .ForMember(dest => dest.StaffName, act => act.MapFrom(src => src.Employee.StaffName));
                    cfg.CreateMap<Voucher, VoucherVM>()
                    .ForMember(dest => dest.Party, act => act.MapFrom(src => src.Partys.PartyName))
                    .ForMember(dest => dest.StaffName, act => act.MapFrom(src => src.Employee.StaffName));
                    cfg.CreateMap<VoucherVM, Voucher>();

                    cfg.CreateMap<CashVoucherVM, CashVoucher>();
                    cfg.CreateMap<DailySale, DailySaleVM>()
                      .ForMember(dest => dest.TerminalName, act => act.MapFrom(src => src.EDC.Name))
                    .ForMember(dest => dest.StoreName, act => act.MapFrom(src => src.Store.StoreName))
                   .ForMember(dest => dest.SalemanName, act => act.MapFrom(src => src.Saleman.Name));
                    cfg.CreateMap<DailySaleVM, DailySale>();
                });
                Mapper = new Mapper(config);
            }

            return Mapper;
        }
    }
}