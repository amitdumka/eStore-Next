using AKS.ParyollSystem;
using AKS.Payroll.Database;
using AKS.Shared.Payroll.Models;
using AKS.Shared.Payrolls.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AKS.Payroll.Forms
{
    public partial class MonthlyAttendanceForm : Form
    {
        private readonly AzurePayrollDbContext azureDb;
        private readonly LocalPayrollDbContext localDb;
        private readonly IMapper _mapper;
        private DateTime OnDate = DateTime.Today;
        private static Mapper InitializeAutomapper()
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
            });
            var mapper = new Mapper(config);
            return mapper;
        }


        private ObservableListSource<MonthlyAttendanceVM> Attendances;
      

        private bool _allEmployees = false;

        public MonthlyAttendanceForm()
        {
            InitializeComponent();
            localDb = new LocalPayrollDbContext();
            azureDb = new AzurePayrollDbContext();
            
        }

        private void btnCurrentMonth_Click(object sender, EventArgs e)
        {
        }

        private void btnSelectedEmployee_Click(object sender, EventArgs e)
        {
        }

        private void btnProcessAll_Click(object sender, EventArgs e)
        {
            PayrollManager pm = new PayrollManager();

            pm.ProcessMonthlyAttendanceForAllEmployee(null);
        }

        private void LoadData()
        {
            if (_allEmployees)
                lbEmployees.DataSource = (azureDb.Employees.Local.Select(c => new { c.EmployeeId, c.StaffName }).ToList());
            else
                lbEmployees.DataSource = azureDb.Employees.Local.Where(c => c.IsWorking).Select(c => new { c.EmployeeId, c.StaffName }).ToList(); ;
            var maList= azureDb.MonthlyAttendances.Where(c=>c.OnDate.Year==DateTime.Today.Year && c.OnDate.Month==DateTime.Today.Month).ToList();
            lbEmployees.SelectedItems.Clear();
            UpdateAttendanceList(maList);
            dgvAttendances.DataSource = Attendances.Where(c => c.OnDate.Date == DateTime.Today).ToList();
            tsslCountVaue.Text = dgvAttendances.Rows.Count.ToString();
        }

        private void UpdateAttendanceList(List<MonthlyAttendance>maList)
        {

            foreach (var att in maList)
            {
                var attvm = _mapper.Map<MonthlyAttendanceVM>(att);
                attvm.StaffName = azureDb.Employees.Local.FirstOrDefault(c => c.EmployeeId == att.EmployeeId)?.StaffName;
                Attendances.Add(attvm);
            }
        }

        private void MonthlyAttendanceForm_Load(object sender, EventArgs e)
        {
            azureDb.Employees.Load();
            Attendances = new ObservableListSource<MonthlyAttendanceVM>();
            LoadData();

        }

        private void UpdateGridView(string empId, DateTime onDate)
        {
            dgvAttendances.DataBindings.Clear();
            dgvAttendances.DataSource = Attendances.Where(c => c.EmployeeId == empId).ToList();
            tsslCountVaue.Text = dgvAttendances.Rows.Count.ToString();
        }

        private void lbEmployees_DoubleClick(object sender, EventArgs e)
        {
            var x = ((System.Windows.Forms.ListBox)sender);
            UpdateGridView(x.SelectedValue.ToString(), OnDate);
        }
    }
}