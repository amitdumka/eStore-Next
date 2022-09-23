
using AKS.MAUI.Databases;
using AKS.Shared.Commons.Data.Helpers.Auth;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace eStore_MauiLib.DataModels.Base
{

    public abstract class BaseDataModel<T> where T : class
    {
        #region Fields
        public string StoreCode;
        public Permission Role;
        public string Permissions;

        public DBType Mode { get; set; }
        public ConType ConType { get; set; }
        public List<T> Entity { get; set; }

        public bool IsError { get; set; }
        public string ErrorMsg { get; set; }

        // Currently local and azure sql db
        protected AppDBContext _localDb, _azureDb;
        public AppDBContext GetContextLocal() => _localDb;

        public AppDBContext GetContextAzure() => _azureDb;

        #endregion

        #region Constructor

        public BaseDataModel(ConType conType)
        {
            ConType = conType;
            Role = Permission.R;
            Permissions = "R";
        }
        public BaseDataModel(ConType conType, Permission role)
        {
            ConType = conType;
            Role = role;
            Permissions = AuthHelper.GetPermission(role);
        }
        #endregion

        #region Mthods
        public bool Connect()
        {
            switch (ConType)
            {
                case ConType.Local:

                    _localDb = new AKS.MAUI.Databases.AppDBContext(DBType.Local);
                    return (_localDb != null);

                case ConType.Remote:
                    break;
                case ConType.RemoteDb:
                    _azureDb = new AKS.MAUI.Databases.AppDBContext(DBType.Azure);
                    return (_azureDb != null);

                case ConType.HybridApi:
                    break;
                case ConType.HybridDB:
                    _azureDb = new AKS.MAUI.Databases.AppDBContext(DBType.Azure);
                    _localDb = new AKS.MAUI.Databases.AppDBContext(DBType.Local);
                    return (_azureDb != null && _localDb != null);

                case ConType.Hybrid:
                    _azureDb = new AKS.MAUI.Databases.AppDBContext(DBType.Azure);
                    _localDb = new AKS.MAUI.Databases.AppDBContext(DBType.Local);
                    return (_azureDb != null && _localDb != null);

                default:
                    _localDb = new AKS.MAUI.Databases.AppDBContext(DBType.Local);
                    return (_localDb != null);
            }
            return false;
        }
        public abstract Task<bool> InitContext();
        public abstract Task<string> GenrateID();

        public int Count()
        {
            AppDBContext db;
            switch (Mode)
            {
                case DBType.Local:
                    db = _localDb;
                    break;

                case DBType.Azure:
                    db = _azureDb;
                    break;
                default:
                    db = _localDb; break;
            }

            return db.Set<T>().Count();
        }
        #endregion

        //Get By ID
        protected async Task<T> GetAsync(string id)
        {
            if (Permissions.Contains("R"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        return await _localDb.FindAsync<T>(id);
                    case DBType.Azure:
                        return await _azureDb.FindAsync<T>(id);
                    default:
                        return null;

                }
            }
            else
            {
                IsError = true;
                ErrorMsg = "Not Authozised to access!";
                return null;
            }
        }
        protected async Task<T> GetAsync(int id)
        {
            if (Permissions.Contains("R"))
            {
                switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.FindAsync<T>(id);
                case DBType.Azure:
                    return await _azureDb.FindAsync<T>(id);
                default:
                    return null;

            } }
            else
            {
                IsError = true;
                ErrorMsg = "Not Authozised to access!";
                return null;
            }
        }

        //Get Items
        protected abstract Task<List<T>> GetItemsAsync(string storeid);
        protected abstract List<T> GetFiltered(QueryParam query);
        protected async Task<List<T>> GetItemsAsync()
        {
            if (Permissions.Contains("R"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        return await _localDb.Set<T>().ToListAsync();
                    case DBType.Azure:
                        return await _azureDb.Set<T>().ToListAsync();
                    default:
                        return await _localDb.Set<T>().ToListAsync();
                }
            }
            else
            {
                IsError = true;
                ErrorMsg = "Not Authozised to access!";
                return null;
            }
        }


        //Save
        protected async Task<T> SaveAsync(T value, bool isNew = true)
        {
            if (Permissions.Contains("W"))
            {


                AppDBContext db;
                switch (Mode)
                {
                    case DBType.Local:
                        db = _localDb;
                        break;

                    case DBType.Azure:
                        db = _azureDb;
                        break;
                    default:
                        return null;
                }
                if (isNew)
                    await db.AddAsync<T>(value);
                else
                    db.Update<T>(value);
                if (await db.SaveChangesAsync() > 0) return value;
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return null;
        }

        protected async Task<List<T>> SaveAllAsync(List<T> values, bool isNew = true)
        {
            if (Permissions.Contains("W"))
            {
                AppDBContext db;
            switch (Mode)
            {
                case DBType.Local:
                    db = _localDb;
                    break;
                case DBType.Azure:
                    db = _azureDb;
                    break;
                default:
                    return null;
            }
            if (isNew)
                await db.AddRangeAsync(values);
            else
                db.UpdateRange(values);
            if (await db.SaveChangesAsync() > 0) return values;
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return null;
            
        }

        //Delete
        protected async Task<bool> DeleteAsync(string id)
        {
            if (Permissions.Contains("D"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        var element = await _localDb.FindAsync<T>(id);
                        _localDb.Remove<T>(element);
                        return (await _localDb.SaveChangesAsync()) > 0;
                    case DBType.Azure:
                        var azureEle = await _azureDb.FindAsync<T>(id);
                        _azureDb.Remove<T>(azureEle);
                        return (await _azureDb.SaveChangesAsync()) > 0;
                    default:return false;
             
                }
            }
            IsError=true;
            ErrorMsg = "Access Denied";
            return false;
        }
        protected async Task<bool> Delete(int id)
        {
            if (Permissions.Contains("D")) { 
            switch (Mode)
            {
                case DBType.Local:
                    var element = await _localDb.FindAsync<T>(id);
                    _localDb.Remove<T>(element);
                    return (await _localDb.SaveChangesAsync()) > 0;
                case DBType.Azure:
                    var azureEle = await _azureDb.FindAsync<T>(id);
                    _azureDb.Remove<T>(azureEle);
                    return (await _azureDb.SaveChangesAsync()) > 0;
                default:
                    return false;
            }
        }
        IsError=true;
            ErrorMsg = "Access Denied";
            return false;
        }
        protected async Task<bool> DeleteAsync(T value)
        {
            if (Permissions.Contains("D"))
            {
                switch (Mode)
                {
                    case DBType.Local:

                        _localDb.Remove<T>(value);
                        return (await _localDb.SaveChangesAsync()) > 0;
                    case DBType.Azure:

                        _azureDb.Remove<T>(value);
                        return (await _azureDb.SaveChangesAsync()) > 0;
                    default:
                        return false;
                }
            }
        
        IsError=true;
            ErrorMsg = "Access Denied";
            return false;
        }
        protected async Task<bool> DeleteAsync(List<T> values)
        {
            if (Permissions.Contains("D")) { 
            switch (Mode)
            {
                case DBType.Local:

                    _localDb.RemoveRange(values);
                    return (await _localDb.SaveChangesAsync()) == values.Count;
                case DBType.Azure:

                    _azureDb.RemoveRange(values);
                    return (await _azureDb.SaveChangesAsync()) == values.Count;
                default:
                    return false;
            }
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return false;
        }

        //YearList
        protected abstract List<int> GetYearList(string storeid);
        protected abstract List<int> GetYearList();

        //Exsits
        public async Task<bool> IsExists(string id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (await _localDb.FindAsync<T>(id) != null) return true; else return false;
                    

                case DBType.Azure:
                    if (await _azureDb.FindAsync<T>(id) != null) return true; else return false;
                    

                case DBType.API:
                    return false;
                    

                default:
                    return false;
                    
            }
        }

        public async Task<bool> IsExists(int id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (await _localDb.FindAsync<T>(id) != null) return true; else return false;
                case DBType.Azure:
                    if (await _azureDb.FindAsync<T>(id) != null) return true; else return false;

                case DBType.API:
                    return false;
                default:
                    return false;
                    
            }
        }



    }

    public abstract class BaseDataModel<T, Y> : BaseDataModel<T> where T : class where Y : class
    {
        protected BaseDataModel(ConType conType) : base(conType)
        {
        }

        protected BaseDataModel(ConType conType, Permission role) : base(conType, role)
        {
        }
        public abstract Task<string> GenrateYID();

        public int CountY()
        {
            AppDBContext db;
            switch (Mode)
            {
                case DBType.Local:
                    db = _localDb;
                    break;

                case DBType.Azure:
                    db = _azureDb;
                    break;
                default:
                    db = _localDb; break;
            }

            return db.Set<Y>().Count();
        }


        //Get By ID
        protected async Task<Y> GetYAsync(string id)
        {
            if (Permissions.Contains("R"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        return await _localDb.FindAsync<Y>(id);
                    case DBType.Azure:
                        return await _azureDb.FindAsync<Y>(id);
                    default:
                        return null;

                }
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return null;
        }
        protected async Task<Y> GetYAsync(int id)
        {
            if (Permissions.Contains("R"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        return await _localDb.FindAsync<Y>(id);
                    case DBType.Azure:
                        return await _azureDb.FindAsync<Y>(id);
                    default:
                        return null;

                }
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return null;
        }

        //Get Items
        protected abstract List<Y> GetYItems(string storeid);
        protected abstract List<Y> GetYFiltered(QueryParam query);
        protected async Task<List<Y>> GetYItemsAsync()
        {
            if (Permissions.Contains("R"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        return await _localDb.Set<Y>().ToListAsync();
                    case DBType.Azure:
                        return await _azureDb.Set<Y>().ToListAsync();
                    default:
                        return await _localDb.Set<Y>().ToListAsync();
                }
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return null;
        }

        //Save
        protected async Task<Y> SaveAsync(Y value, bool isNew = true)
        {
            if (Permissions.Contains("W"))
            {
                AppDBContext db;
                switch (Mode)
                {
                    case DBType.Local:
                        db = _localDb;
                        break;

                    case DBType.Azure:
                        db = _azureDb;
                        break;
                    default:
                        return null;
                }
                if (isNew)
                    await db.AddAsync<Y>(value);
                else
                    db.Update<Y>(value);
                if (await db.SaveChangesAsync() > 0) return value;
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return null;
        }
        protected async Task<List<Y>> SaveAllAsync(List<Y> values, bool isNew = true)
        {
            if (Permissions.Contains("W"))
            {
                AppDBContext db;
                switch (Mode)
                {
                    case DBType.Local:
                        db = _localDb;
                        break;
                    case DBType.Azure:
                        db = _azureDb;
                        break;
                    default:
                        return null;
                }
                if (isNew)
                    await db.AddRangeAsync(values);
                else
                    db.UpdateRange(values);
                if (await db.SaveChangesAsync() > 0) return values;
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return null;
        }

        //Delete
        protected async Task<bool> DeleteYAsync(string id)
        {
            if (Permissions.Contains("D"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        var element = await _localDb.FindAsync<Y>(id);
                        _localDb.Remove<Y>(element);
                        return (await _localDb.SaveChangesAsync()) > 0;
                    case DBType.Azure:
                        var azureEle = await _azureDb.FindAsync<Y>(id);
                        _azureDb.Remove<Y>(azureEle);
                        return (await _azureDb.SaveChangesAsync()) > 0;
                    default:
                        return false;
                }
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return false;
        }
        protected async Task<bool> DeleteY(int id)
        {
            if (Permissions.Contains("D"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        var element = await _localDb.FindAsync<Y>(id);
                        _localDb.Remove<Y>(element);
                        return (await _localDb.SaveChangesAsync()) > 0;
                    case DBType.Azure:
                        var azureEle = await _azureDb.FindAsync<Y>(id);
                        _azureDb.Remove<Y>(azureEle);
                        return (await _azureDb.SaveChangesAsync()) > 0;
                    default:
                        return false;
                }
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return false;
        }
        protected async Task<bool> DeleteAsync(Y value)
        {
            if (Permissions.Contains("D"))
            {
                switch (Mode)
                {
                    case DBType.Local:

                        _localDb.Remove<Y>(value);
                        return (await _localDb.SaveChangesAsync()) > 0;
                    case DBType.Azure:

                        _azureDb.Remove<Y>(value);
                        return (await _azureDb.SaveChangesAsync()) > 0;
                    default:
                        return false;
                }
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return false;
        }
        protected async Task<bool> DeleteAsync(List<Y> values)
        {
            if (Permissions.Contains("D"))
            {
                switch (Mode)
                {
                    case DBType.Local:

                        _localDb.RemoveRange(values);
                        return (await _localDb.SaveChangesAsync()) == values.Count;
                    case DBType.Azure:

                        _azureDb.RemoveRange(values);
                        return (await _azureDb.SaveChangesAsync()) == values.Count;
                    default:
                        return false;
                }
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return false;
        }

        //YearList
        protected abstract List<int> GetYearListY(string storeid);
        protected abstract List<int> GetYearListY();

        //Exsits
        public async Task<bool> IsYExists(string id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (await _localDb.FindAsync<T>(id) != null) return true; else return false;
                    break;

                case DBType.Azure:
                    if (await _azureDb.FindAsync<T>(id) != null) return true; else return false;
                    break;

                case DBType.API:
                    return false;
                    break;

                default:
                    return false;
                    break;
            }
        }

        public async Task<bool> IsYExists(int id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (await _localDb.FindAsync<T>(id) != null) return true; else return false;
                    break;

                case DBType.Azure:
                    if (await _azureDb.FindAsync<T>(id) != null) return true; else return false;
                    break;

                case DBType.API:
                    return false;
                    break;

                default:
                    return false;
                    break;
            }
        }

    }
    public abstract class BaseDataModel<T, Y, Z> : BaseDataModel<T, Y> where T : class where Y : class where Z : class
    {
        protected BaseDataModel(ConType conType) : base(conType)
        {
        }

        protected BaseDataModel(ConType conType, Permission role) : base(conType, role)
        {
        }

        public abstract Task<string> GenrateZID();

        public int CountZ()
        {
            AppDBContext db;
            switch (Mode)
            {
                case DBType.Local:
                    db = _localDb;
                    break;

                case DBType.Azure:
                    db = _azureDb;
                    break;
                default:
                    db = _localDb; break;
            }

            return db.Set<Z>().Count();
        }


        #region  Get

        //Get By ID
        protected async Task<Z> GetZAsync(string id)
        {
            if (Permissions.Contains("R"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        return await _localDb.FindAsync<Z>(id);
                    case DBType.Azure:
                        return await _azureDb.FindAsync<Z>(id);
                    default:
                        return null;

                }
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return null;
        }
        protected async Task<Z> GetZAsync(int id)
        {
            if(Permissions.Contains('R'))
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.FindAsync<Z>(id);
                case DBType.Azure:
                    return await _azureDb.FindAsync<Z>(id);
                default:
                    return null;

            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return null;
        }

        //Get Items
        protected abstract List<Z> GetZItems(string storeid);
        protected abstract List<Z> GetZFiltered(QueryParam query);
        protected async Task<List<Z>> GetZItemsAsync()
        {
            if (Permissions.Contains('R'))
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.Set<Z>().ToListAsync();
                case DBType.Azure:
                    return await _azureDb.Set<Z>().ToListAsync();
                default:
                    return await _localDb.Set<Z>().ToListAsync();
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return null;
        }

        #endregion  Get

        #region Save

        //Save
        protected async Task<Z> SaveAsync(Z value, bool isNew = true)
        {
            if (Permissions.Contains("W"))
            {
                AppDBContext db;
                switch (Mode)
                {
                    case DBType.Local:
                        db = _localDb;
                        break;

                    case DBType.Azure:
                        db = _azureDb;
                        break;
                    default:
                        return null;
                }
                if (isNew)
                    await db.AddAsync<Z>(value);
                else
                    db.Update<Z>(value);
                if (await db.SaveChangesAsync() > 0) return value;
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            
            return null;
        }
        protected async Task<List<Z>> SaveAllAsync(List<Z> values, bool isNew = true)
        {

            if (Permissions.Contains("W"))
            {
                AppDBContext db;
                switch (Mode)
                {
                    case DBType.Local:
                        db = _localDb;
                        break;
                    case DBType.Azure:
                        db = _azureDb;
                        break;
                    default:
                        return null;
                }
                if (isNew)
                    await db.AddRangeAsync(values);
                else
                    db.UpdateRange(values);
                if (await db.SaveChangesAsync() > 0) return values;
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            
            return null;
        }

        #endregion
        #region Delete
        //Delete
        protected async Task<bool> DeleteZAsync(string id)
        {
            if (Permissions.Contains("D")) 
            switch (Mode)
            {
                case DBType.Local:
                    var element = await _localDb.FindAsync<Z>(id);
                    _localDb.Remove<Z>(element);
                    return (await _localDb.SaveChangesAsync()) > 0;
                case DBType.Azure:
                    var azureEle = await _azureDb.FindAsync<Z>(id);
                    _azureDb.Remove<Z>(azureEle);
                    return (await _azureDb.SaveChangesAsync()) > 0;
                default:
                    return false;
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return false;
        }
        protected async Task<bool> Delete(int id)
        {
            if(Permissions.Contains("D"))
            switch (Mode)
            {
                case DBType.Local:
                    var element = await _localDb.FindAsync<Z>(id);
                    _localDb.Remove<Z>(element);
                    return (await _localDb.SaveChangesAsync()) > 0;
                case DBType.Azure:
                    var azureEle = await _azureDb.FindAsync<Z>(id);
                    _azureDb.Remove<Z>(azureEle);
                    return (await _azureDb.SaveChangesAsync()) > 0;
                default:
                    return false;
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return false;
        }
        protected async Task<bool> DeleteAsync(Z value)
        {
            if(Permissions.Contains("D"))
            switch (Mode)
            {
                case DBType.Local:

                    _localDb.Remove<Z>(value);
                    return (await _localDb.SaveChangesAsync()) > 0;
                case DBType.Azure:

                    _azureDb.Remove<Z>(value);
                    return (await _azureDb.SaveChangesAsync()) > 0;
                default:
                    return false;
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return false;
        }
        protected async Task<bool> DeleteAsync(List<Z> values)
        {
            if(Permissions.Contains("D"))
            switch (Mode)
            {
                case DBType.Local:

                    _localDb.RemoveRange(values);
                    return (await _localDb.SaveChangesAsync()) == values.Count;
                case DBType.Azure:

                    _azureDb.RemoveRange(values);
                    return (await _azureDb.SaveChangesAsync()) == values.Count;
                default:
                    return false;
            }
            IsError = true;
            ErrorMsg = "Access Denied";
            return true;
        }

        #endregion Delete

        #region YearList
        //YearList
        protected abstract List<int> GetYearListZ(string storeid);
        protected abstract List<int> GetYearListZ();
        #endregion

        #region Exsits
        //Exsits
        public async Task<bool> IsZExists(string id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (await _localDb.FindAsync<T>(id) != null) return true; else return false;

                case DBType.Azure:
                    if (await _azureDb.FindAsync<T>(id) != null) return true; else return false;




                default:
                    return false;

            }
        }

        public async Task<bool> IsZExists(int id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (await _localDb.FindAsync<T>(id) != null) return true; else return false;


                case DBType.Azure:
                    if (await _azureDb.FindAsync<T>(id) != null) return true; else return false;




                default:
                    return false;

            }
        }

        #endregion
    }
}

