using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Banking;

namespace AKS.AccountingSystem
{
    public interface IController<T> : IDisposable
    {
        public Task<bool> CreateAsync(T record);

        public Task<bool> Update(T record);

        public Task<bool> Delete(T record);

        public Task<bool> DeleteAll(List<T> records);

        public Task<bool> DeleteAll(List<int> records);

        public Task<bool> DeleteAll(List<string> records);

        public Task<bool> Delete(string id);

        public Task<bool> Delete(int id);

        public List<T> Records();

        public T Record(int id);

        public T Record(string id);

        public List<T> Record(List<string> columnNames, List<string> value);
    }

    public class BankTranscationsController : IController<BankTranscation>
    {
        private bool disposedValue;
        private AzurePayrollDbContext azureDb;
        public BankTranscationsController(AzurePayrollDbContext dbContext)
        {
            azureDb = dbContext;
        }

        public async Task<bool> CreateAsync(BankTranscation record)
        {
            if (record != null) azureDb.BankTranscations.Add(record);
            return (await azureDb.SaveChangesAsync()) > 0;

            //throw new NotImplementedException();
        }

        public async Task<bool> Delete(BankTranscation record)
        {
            azureDb.BankTranscations.Remove(record);
            return (await azureDb.SaveChangesAsync()) > 0;
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id)
        {
            var record = azureDb.BankTranscations.Find(id);
            if (record != null) azureDb.BankTranscations.Remove(record);

            return (await azureDb.SaveChangesAsync()) > 0;

        }

        public async Task<bool> DeleteAll(List<BankTranscation> records)
        {
            azureDb.BankTranscations.RemoveRange(records);
            return (await azureDb.SaveChangesAsync()) > 0;
        }

        public Task<bool> DeleteAll(List<int> records)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAll(List<string> records)
        {
            throw new NotImplementedException();
        }

        public BankTranscation Record(int id)
        {
            return azureDb.BankTranscations.Find(id);
        }

        public BankTranscation Record(string id)
        {
            throw new NotImplementedException();
        }

        public List<BankTranscation> Record(List<string> columnNames, List<string> value)
        {
            throw new NotImplementedException();
        }

        public List<BankTranscation> Records()
        {
            return azureDb.BankTranscations.ToList();
        }

        public async Task<bool> Update(BankTranscation record)
        {
            if (record != null)
            {
                if (azureDb.BankTranscations.Any(c => c.BankTranscationId == record.BankTranscationId))
                    azureDb.BankTranscations.Update(record);
            }
            return (await azureDb.SaveChangesAsync()) > 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    azureDb.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BankTranscation()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}