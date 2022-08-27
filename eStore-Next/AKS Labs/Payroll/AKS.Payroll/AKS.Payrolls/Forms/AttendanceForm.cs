using AKS.Payroll.Database;
using AKS.Payroll.Forms.EntryForms;
using AKS.Shared.Payroll.Models;
using AKS.Shared.Payrolls.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AKS.Payroll.Forms
{
    public partial class AttendanceForm : Form
    {
        private readonly IMapper _mapper;
        private AzurePayrollDbContext context;
        private ObservableListSource<AttendanceVM> Attendances;
        private DateTime OnDate;
        private string SelectedEmployee = "";

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
            });
            var mapper = new Mapper(config);
            return mapper;
        }

        public AttendanceForm()
        {
            InitializeComponent();
            _mapper = InitializeAutomapper();
            OnDate = DateTime.Now;
        }

        private void AttendanceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            context.Dispose();
        }

        private void AttendanceForm_Load(object sender, EventArgs e)
        {
            context = new AzurePayrollDbContext();

            context.Employees.Load();

            if (cbAllEmployee.Checked)
                lbEmployees.DataSource = context.Employees.Local.OrderBy(c => c.EmployeeId).ToList();
            else
                lbEmployees.DataSource = context.Employees.Local.Where(c => c.IsWorking).OrderBy(c => c.EmployeeId).ToList();

            //.ToBindingList();
            lbEmployees.SelectedItems.Clear();
            Attendances = new ObservableListSource<AttendanceVM>();

            AddToList(context.Attendances.Where(c => c.OnDate.Month == OnDate.Month
            && c.OnDate.Year == OnDate.Year).OrderByDescending(c => c.OnDate).OrderBy(c => c.EmployeeId).ToList());

            dgvAttendances.DataSource = Attendances.Where(c => c.OnDate.Date == DateTime.Today).ToList();
            tSSLCountValue.Text = dgvAttendances.Rows.Count.ToString();
        }

        private void UpdateGridView(DateTime onDate)
        {
            dgvAttendances.DataSource = Attendances.Where(c => c.OnDate.Date == onDate.Date).ToList();
            tSSLCountValue.Text = dgvAttendances.Rows.Count.ToString();
        }

        private void UpdateGridView(string empId, DateTime onDate)
        {

            dgvAttendances.DataBindings.Clear();
            if (onDate.Date != DateTime.Today.Date)
            {
                if((Attendances.Where(c => c.EmployeeId == empId && c.OnDate.Year == onDate.Year && c.OnDate.Month == onDate.Month).Count()) <= 0 )
                {
                    AddToList(context.Attendances.Where(c => c.EmployeeId == empId && c.OnDate.Year == onDate.Year && c.OnDate.Month == onDate.Month).ToList());

                }
                dgvAttendances.DataSource = Attendances.Where(c => c.EmployeeId == empId
                && c.OnDate.Year == onDate.Year
                && c.OnDate.Month == onDate.Month).OrderByDescending(c=>c.OnDate).ToList();

            }
            else
                dgvAttendances.DataSource = Attendances.Where(c => c.EmployeeId == empId).OrderByDescending(c => c.OnDate).ToList();
            tSSLCountValue.Text = dgvAttendances.Rows.Count.ToString();
        }

        private void AddToList(List<Attendance> attds)
        {
            foreach (var att in attds)
            {
                var attvm = _mapper.Map<AttendanceVM>(att);
                attvm.StaffName = context.Employees.Local.FirstOrDefault(c => c.EmployeeId == att.EmployeeId)?.StaffName;
                Attendances.Add(attvm);
            }
        }

        private void lbEmployees_DoubleClick(object sender, EventArgs e)
        {
            var x = ((System.Windows.Forms.ListBox)sender);
            SelectedEmployee = (string)x.SelectedValue;
            if (cbLastMonth.Checked)
                UpdateGridView(x.SelectedValue.ToString(), OnDate.AddMonths(-1));
            else
                UpdateGridView(x.SelectedValue.ToString(), OnDate);

        }

        private void btnAddAttendance_Click(object sender, EventArgs e)
        {
            AttendanceEntryForm form = new();
            form.ParentForm = this;

            if (form.ShowDialog() == DialogResult.OK)
            {

                var newAttend = _mapper.Map<AttendanceVM>(form.SavedAtt);
                newAttend.StaffName = form.EmployeeName;

                if (form.SavedAtt.EntryStatus == EntryStatus.Added)
                    Attendances.Add(newAttend);
                else
                {
                    Attendances.Remove(Attendances.Where(c => c.AttendanceId == newAttend.AttendanceId).First());
                    Attendances.Add(newAttend);
                }
                if (form.DeletedAttednance != null)
                {
                    Attendances.Remove(Attendances.Where(c => c.AttendanceId == form.DeletedAttednance).First());
                }
                UpdateGridView(newAttend.OnDate.Date);
            }
        }

        private void cbAllEmployee_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbAllEmployee.Checked)
                lbEmployees.DataSource = context.Employees.Local.ToBindingList();
            else
                lbEmployees.DataSource = context.Employees.Local.Where(c => c.IsWorking).ToList();//.ToBindingList();
            lbEmployees.Refresh();
        }

        private void dgvAttendances_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var a = _mapper.Map<Attendance>(dgvAttendances.CurrentRow.DataBoundItem);

            var x = new AttendanceEntryForm(a)
            {
                // x.MdiParent = this.MdiParent;
                ParentForm = this
            };
            if (x.ShowDialog() == DialogResult.OK)
            {
                if (x.SavedAtt != null)
                {
                    var newAttend = _mapper.Map<AttendanceVM>(x.SavedAtt);
                    newAttend.StaffName = x.EmployeeName;
                    if (x.SavedAtt.EntryStatus == EntryStatus.Added)
                        Attendances.Add(newAttend);
                    else
                    {
                        Attendances.Remove(Attendances.Where(c => c.AttendanceId == newAttend.AttendanceId).First());
                        Attendances.Add(newAttend);
                    }
                }
                else if (x.DeletedAttednance != null)
                {
                    Attendances.Remove(Attendances.Where(c => c.AttendanceId == x.DeletedAttednance).First());
                }
                UpdateGridView(DateTime.Today.Date);
            }
        }

        private void cbAllEmployee_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbLastMonth_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbLastMonth.Checked)
            {
            }
            else
            {
            }
        }

        private void lbEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}