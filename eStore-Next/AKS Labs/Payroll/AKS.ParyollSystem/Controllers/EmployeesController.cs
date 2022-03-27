using AKS.Payroll.Database;
using AKS.Shared.Payroll.Models;

namespace AKS.ParyollSystem.Controllers
{
    public class EmployeesController : IController<Employee>
    {
        private bool disposedValue;
        private PayrollDbContext _context;

        public EmployeesController(PayrollDbContext db)
        {
            _context = db;
        }

        public async Task<bool> CreateAsync(Employee record)
        {
            if (_context == null)
            {
                using (_context = new PayrollDbContext())
                {
                    _context.Employees.Add(record);
                    return await _context.SaveChangesAsync() > 0;
                }
            }
            else
            {
                _context.Employees.Add(record);
                return await _context.SaveChangesAsync() > 0;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        Task<bool> IController<Employee>.Delete(Employee record)
        {
            throw new NotImplementedException();
        }

        Task<bool> IController<Employee>.Delete(string id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IController<Employee>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IController<Employee>.DeleteAll(List<Employee> records)
        {
            throw new NotImplementedException();
        }

        Task<bool> IController<Employee>.DeleteAll(List<int> records)
        {
            throw new NotImplementedException();
        }

        Task<bool> IController<Employee>.DeleteAll(List<string> records)
        {
            throw new NotImplementedException();
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~EmployeesController()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        Employee IController<Employee>.Record(int id)
        {
            throw new NotImplementedException();
        }

        Employee IController<Employee>.Record(string id)
        {
            throw new NotImplementedException();
        }

        List<Employee> IController<Employee>.Record(List<string> columnNames, List<string> value)
        {
            throw new NotImplementedException();
        }

        List<Employee> IController<Employee>.Records()
        {
            throw new NotImplementedException();
        }

        Task<bool> IController<Employee>.Update(Employee record)
        {
            throw new NotImplementedException();
        }
    }
}