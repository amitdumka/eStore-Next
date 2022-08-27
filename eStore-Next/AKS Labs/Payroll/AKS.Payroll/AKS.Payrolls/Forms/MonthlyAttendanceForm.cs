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
        private readonly IMapper _mapper;
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        private bool _allEmployees = true;
        private bool _allRecord = true;
        private ObservableListSource<MonthlyAttendanceVM> Attendances;
        private DateTime OnDate = DateTime.Today;
        public MonthlyAttendanceForm()
        {
            InitializeComponent();
            localDb = new LocalPayrollDbContext();
            azureDb = new AzurePayrollDbContext();
            _mapper = InitializeAutomapper();

        }

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
        private void btnCurrentMonth_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Do you want generate monthly attendance for Current Month? Are you sure??",
                                     "Confirm !!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                PayrollManager pm = new();

                if (pm.CalculateMonthlyAttendance(null, DateTime.Today))
                {
                    MessageBox.Show("It has been generated!");
                    RefreshData();
                }
            }


        }

        private void btnPrevMonth_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Do you want generate monthly attendance for Current Month? Are you sure??",
                                    "Confirm !!",
                                    MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                PayrollManager pm = new();

                if (pm.CalculateMonthlyAttendance(null, DateTime.Today.AddMonths(-1)))
                {
                    MessageBox.Show("It has been generated!");
                    RefreshData();
                }
            }
        }

        private void btnPrintMissingAttendances_Click(object sender, EventArgs e)
        {
            PayrollValidator payrollValidator = new();
            if (lbEmployees.SelectedValue != null)
            {
                var empId = lbEmployees.SelectedValue.ToString();
                MessageBox.Show(empId);
                var emp = azureDb.Employees.Local.Where(c => c.EmployeeId == empId).FirstOrDefault();
                var data = payrollValidator.FindMissingAttendances(azureDb, empId, emp.JoiningDate, emp.LeavingDate);
                if (data != null && data.Found)
                {
                    string[] line = new string[data.MissingDates.Count];
                    int i = 0;
                    foreach (var item in data.MissingDates)
                    {
                        line[i++] = item.ToString();
                    }
                    string path = "C:\\SaveData\\" + empId.Replace("/", "-");

                    File.WriteAllLines(path + ".txt", line);
                    dataGridView1.DataSource = data.MissingDates.ToList();
                }
            }
            else
            {
                MessageBox.Show("Kindly select one Employee");
            }

        }

        private void btnProcessAll_Click(object sender, EventArgs e)
        {
            PayrollManager pm = new();

            pm.ProcessMonthlyAttendanceForAllEmployee(null);
        }

        private void btnSelectedEmployee_Click(object sender, EventArgs e)
        {
        }
        private void btnVerifyAttendance_Click(object sender, EventArgs e)
        {

        }

        private void lbEmployees_DoubleClick(object sender, EventArgs e)
        {
            var x = ((System.Windows.Forms.ListBox)sender);
            UpdateGridView(x.SelectedValue.ToString(), OnDate);
        }

        private void LoadData()
        {
            if (_allEmployees)
                lbEmployees.DataSource = (azureDb.Employees.Local.Select(c => new { c.EmployeeId, c.StaffName }).ToList());
            else
                lbEmployees.DataSource = azureDb.Employees.Local.Where(c => c.IsWorking).Select(c => new { c.EmployeeId, c.StaffName }).ToList(); ;



            lbEmployees.SelectedItems.Clear();
            UpdateAttendanceList(azureDb.MonthlyAttendances.Where(c => c.OnDate.Year == DateTime.Today.Year
           && c.OnDate.Month >= DateTime.Today.AddMonths(-1).Month).OrderByDescending(c => c.OnDate).ToList());

            dgvAttendances.DataSource = Attendances.ToList();
            tsslCountVaue.Text = dgvAttendances.Rows.Count.ToString();
        }
        private void RefreshData()
        {
            azureDb.Dispose();
            azureDb = new AzurePayrollDbContext();
            Attendances.Clear();
            var maList = azureDb.MonthlyAttendances.Where(c => c.OnDate.Year == DateTime.Today.Year
            && c.OnDate.Month >= DateTime.Today.AddMonths(-1).Month).OrderByDescending(c => c.OnDate).ToList();


            UpdateAttendanceList(maList);
            dgvAttendances.DataBindings.Clear();
            dgvAttendances.DataSource = Attendances.ToList();
            tsslCountVaue.Text = dgvAttendances.Rows.Count.ToString();
            dgvAttendances.Refresh();

        }

        private void MonthlyAttendanceForm_Load(object sender, EventArgs e)
        {
            azureDb.Employees.Load();
            Attendances = new ObservableListSource<MonthlyAttendanceVM>();
            LoadData();

        }

        private bool RemoveEmployee(string empId)
        {
            int c = Attendances.Count;
            var atts = Attendances.Where(c => c.EmployeeId == empId).ToList();
            foreach (var att in atts)
            {
                Attendances.Remove(att);
            }
            if ((c - atts.Count) == Attendances.Count) return true; else return false;
        }

        private void tcMonthlyAttendances_TabIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString() + "\n" + e.ToString() + "\n" + tcMonthlyAttendances.SelectedTab.Name);
        }

        private void UpdateAttendanceList(List<MonthlyAttendance> maList)
        {

            foreach (var att in maList)
            {
                var attvm = _mapper.Map<MonthlyAttendanceVM>(att);
                attvm.StaffName = azureDb.Employees.Local.FirstOrDefault(c => c.EmployeeId == att.EmployeeId)?.StaffName;
                try
                {
                    Attendances.Remove(attvm);
                }
                catch (Exception ex)
                {

                }
                Attendances.Add(attvm);

            }
            //Attendances= (ObservableListSource<MonthlyAttendanceVM>)Attendances.Distinct().ToList();
        }
        private void UpdateGridView(string empId, DateTime onDate)
        {
            dgvAttendances.DataBindings.Clear();
            if (_allRecord)
            {
                var ctr = Attendances.Where(c => c.EmployeeId == empId).Count();
                var ctr2 = azureDb.MonthlyAttendances.Where(c => c.EmployeeId == empId).Count();
                if (ctr2 != ctr)
                {
                    bool x = RemoveEmployee(empId);
                    var atx = azureDb.MonthlyAttendances.Where(c => c.EmployeeId == empId).OrderByDescending(c => c.OnDate).ToList();
                    UpdateAttendanceList(atx);
                }
            }
            dgvAttendances.DataSource = Attendances.Where(c => c.EmployeeId == empId).ToList();
            tsslCountVaue.Text = dgvAttendances.Rows.Count.ToString();
        }
    }
}