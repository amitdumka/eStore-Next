using AKS.Payroll.Database;
using AKS.Shared.Templets.Helpers;

namespace AKS.Shared.Templets.DataModels
{
    public abstract class DataModel<T>
    {
        protected AzurePayrollDbContext azureDb;
        protected LocalPayrollDbContext localDb;
        protected string StoreCode;
       
        public void SetStoreCode(string sc) { StoreCode = sc; }
        public string GetStoreCode() { return StoreCode; }

        public DataModel()
        {
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();
        }

        public DataModel(AzurePayrollDbContext db)
        {
            azureDb = db;
        }

        public DataModel(LocalPayrollDbContext ldb)
        {
            localDb = ldb;
        }

        public DataModel(AzurePayrollDbContext db, LocalPayrollDbContext ldb)
        {
            azureDb = db; localDb = ldb;
        }

        public AzurePayrollDbContext GetDatabaseInstance() { return azureDb; }
        public LocalPayrollDbContext GetLocalDbInstance() { return localDb; }

        public int SaveChanges()
        { return azureDb.SaveChanges(); }

        //View
        public abstract T Get(string id);

        public abstract T Get(int id);

        public abstract List<T> GetList();

        public void AddOrUpdate(T record, bool isNew = true)
        {
            if (isNew)
                azureDb.Add(record);
            else
                azureDb.Update(record);
        }

        //Create and Update
        public T? Save(T record, bool isNew = true)
        {
            try
            {
                if (isNew)
                    azureDb.Add(record);
                else
                    azureDb.Update(record);
                int count = azureDb.SaveChanges();
                if (count > 0) return record;
                else return default(T);
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return default(T);
            }
        }

        public List<T> SaveRange(List<T> records, bool isNew = true)
        {
            try
            {
                if (isNew)
                    azureDb.AddRange(records);
                else
                    azureDb.UpdateRange(records);
                int count = azureDb.SaveChanges();
                if (count > 0) return records;
                else return null;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return null;
            }
        }

        //Delete
        public bool Delete(T record)
        {
            try
            {
                azureDb.Remove(record);
                return azureDb.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return false;
            }
        }

        public bool DeleteRange(List<T> records)
        {
            try
            {
                azureDb.RemoveRange(records);
                return azureDb.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return false;
            }
        }

        public int AddorUpdateRecord<TK>(TK obj, bool isNew, bool save = false)
        {
            if (isNew)
                azureDb.Add(obj);
            else azureDb.Update(obj);
            if (save)
                return azureDb.SaveChanges();
            return 0;
        }
    }

    public abstract class DataModel<T, Y, Z> : DataModel<T, Y>
    {
        //View
        public abstract Z GetSeconday(string id);

        public abstract Z GetSeconday(int id);

        public abstract List<Z> GetSecondayList();

        //Create and Update
        public void AddOrUpdate(Z record, bool isNew = true)
        {
            if (isNew)
                azureDb.Add(record);
            else
                azureDb.Update(record);
        }

        public Z? Save(Z record, bool isNew = true)
        {
            try
            {
                if (isNew)
                    azureDb.Add(record);
                else
                    azureDb.Update(record);
                int count = azureDb.SaveChanges();
                if (count > 0) return record;
                else return default(Z);
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return default(Z);
            }
        }

        public List<Z> SaveRange(List<Z> records, bool isNew = true)
        {
            try
            {
                if (isNew)
                    azureDb.AddRange(records);
                else
                    azureDb.UpdateRange(records);
                int count = azureDb.SaveChanges();
                if (count > 0) return records;
                else return null;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return null;
            }
        }

        //Delete
        public bool Delete(Z record)
        {
            try
            {
                azureDb.Remove(record);
                return azureDb.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return false;
            }
        }

        public bool DeleteRange(List<Z> records)
        {
            try
            {
                azureDb.RemoveRange(records);
                return azureDb.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return false;
            }
        }
    }

    public abstract class DataModel<T, Y> : DataModel<T>
    {
        //View
        public abstract Y GetY(string id);

        public abstract Y GetY(int id);

        public abstract List<Y> GetYList();

        //Create and Update
        public void AddOrUpdate(Y record, bool isNew = true)
        {
            if (isNew)
                azureDb.Add(record);
            else
                azureDb.Update(record);
        }

        public Y? Save(Y record, bool isNew = true)
        {
            try
            {
                if (isNew)
                    azureDb.Add(record);
                else
                    azureDb.Update(record);
                int count = azureDb.SaveChanges();
                if (count > 0) return record;
                else return default(Y);
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return default(Y);
            }
        }

        public List<Y> SaveRange(List<Y> records, bool isNew = true)
        {
            try
            {
                if (isNew)
                    azureDb.AddRange(records);
                else
                    azureDb.UpdateRange(records);
                int count = azureDb.SaveChanges();
                if (count > 0) return records;
                else return null;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return null;
            }
        }

        //Delete
        public bool Delete(Y record)
        {
            try
            {
                azureDb.Remove(record);
                return azureDb.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return false;
            }
        }

        public bool DeleteRange(List<Y> records)
        {
            try
            {
                azureDb.RemoveRange(records);
                return azureDb.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return false;
            }
        }
    }

}
