using AKS.Payroll.Database;

namespace AKS.Payroll.Forms.Inventory.Functions
{
    /// <summary>
    /// Model/Temeplete class to make manager/VM class
    /// </summary>
    public abstract class Manager
    {
        protected static AzurePayrollDbContext azureDb;
        protected static LocalPayrollDbContext localDb;
        protected static string StoreCode = "ARD";//TODO: Need to Assign

        protected abstract void Delete();

        protected abstract void Get(string id);

        protected abstract void GetList();

        protected abstract void Save();
    }
}