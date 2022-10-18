
using AKS.Payroll.Database;
using AKS.Payroll.Forms.EntryForms;
using AKS.Payrolls.Forms;
using AKS.Shared.Payroll.Models;
using AKS.Shared.Payrolls.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

//https://docs.microsoft.com/en-gb/ef/ef6/fundamentals/databinding/winforms
namespace AKS.Payroll.Forms
{
    public partial class EmployeeForm : Form
    {
        private AzurePayrollDbContext context;
        private readonly IMapper _mapper;
        private ObservableListSource<EmployeeVM> _employeesView { get; set; }
        private ObservableListSource<EmployeeDetailVM> _employeeDetailsView { get; set; }

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
            });
            var mapper = new Mapper(config);
            return mapper;
        }

        public EmployeeForm()
        {
            InitializeComponent();

            _mapper = InitializeAutomapper();
            _employeesView = new ObservableListSource<EmployeeVM>();
            _employeeDetailsView = new ObservableListSource<EmployeeDetailVM>();
        }

        private void EmployeeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            context.Dispose();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            context = new AzurePayrollDbContext();
            UpdateEmployeeVMList(context.Employees.ToList());
            dgvEmployee.DataSource = this._employeesView.ToBindingList();
        }

        private void LoadEmpDetails()
        {
            if (_employeeDetailsView.Count <= 0)
            {
                var eDtails = context.EmployeeDetails.Include(c => c.Employee).ToList();
                foreach (var item in eDtails)
                {
                    _employeeDetailsView.Add(_mapper.Map<EmployeeDetailVM>((item)));
                }
            }
            dgvEmployeeDetails.DataSource = this._employeeDetailsView.ToBindingList();
        }

        private void UpdateEmployeeVMList(List<Employee> emp)
        {
            foreach (var item in emp)
            {
                _employeesView.Add(_mapper.Map<EmployeeVM>(item));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            context.SaveChanges();
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            LoadEmpDetails();
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            EmployeeEntry entry = new EmployeeEntry();
            entry.Show();
        }

        private void btnAddBankDetails_Click(object sender, EventArgs e)
        {
            EmployeeBankDetailsForm form = new EmployeeBankDetailsForm();
            if (form.ShowDialog() == DialogResult.OK)
            {

            }
            else if (DialogResult == DialogResult.Yes)
            {

            }
        }
    }
}