
using AKS.MAUI.Databases;
using Microsoft.EntityFrameworkCore;

namespace eStore_MauiLib.DataModels.Base
{

    public abstract class BaseDataModel<T> where T : class
    {
        #region Fields
        public string StoreCode;
        public Permission Role;

        public DBType Mode { get; set; }
        public ConType ConType { get; set; }
        public List<T> Entity { get; set; }

        // Currently local and azure sql db
        protected AppDBContext _localDb, _azureDb;
        public AppDBContext GetContextLocal() => _localDb;

        public AppDBContext GetContextAzure() => _azureDb;

        #endregion

        #region Constructor

        public BaseDataModel(ConType conType)
        {
            ConType = conType;
            Role = Permission.Read;
        }
        public BaseDataModel(ConType conType, Permission role)
        {
            ConType = conType;
            Role = role;
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
            if (Role == Permission.None || Role == Permission.Write || Role == Permission.Self)
                return null;
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
        protected async Task<T> GetAsync(int id)
        {
            if (Role == Permission.None || Role == Permission.Write || Role == Permission.Self)
                return null;
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

        //Get Items
        protected abstract Task<List<T>> GetItemsAsync(string storeid);
        protected abstract List<T> GetFiltered(QueryParam query);
        protected async Task<List<T>> GetItemsAsync()
        {
            if (Role == Permission.None || Role == Permission.Write || Role == Permission.Self)
                return null;
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


        //Save
        protected async Task<T> SaveAsync(T value, bool isNew = true)
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
            return null;
        }

        protected async Task<List<T>> SaveAllAsync(List<T> values, bool isNew = true)
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
            return null;
        }

        //Delete
        protected async Task<bool> DeleteAsync(string id)
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
                default:
                    return false;
            }
        }
        protected async Task<bool> Delete(int id)
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
                default:
                    return false;
            }
        }
        protected async Task<bool> DeleteAsync(T value)
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
        protected async Task<bool> DeleteAsync(List<T> values)
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

        public async Task<bool> IsExists(int id)
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
        protected async Task<Y> GetYAsync(int id)
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

        //Get Items
        protected abstract List<Y> GetYItems(string storeid);
        protected abstract List<Y> GetYFiltered(QueryParam query);
        protected async Task<List<Y>> GetYItemsAsync()
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

        //Save
        protected async Task<Y> SaveAsync(Y value, bool isNew = true)
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
            return null;
        }
        protected async Task<List<Y>> SaveAllAsync(List<Y> values, bool isNew = true)
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
            return null;
        }

        //Delete
        protected async Task<bool> DeleteYAsync(string id)
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
        protected async Task<bool> DeleteY(int id)
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
        protected async Task<bool> DeleteAsync(Y value)
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
        protected async Task<bool> DeleteAsync(List<Y> values)
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
        protected async Task<Z> GetZAsync(int id)
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

        //Get Items
        protected abstract List<Z> GetZItems(string storeid);
        protected abstract List<Z> GetZFiltered(QueryParam query);
        protected async Task<List<Z>> GetZItemsAsync()
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.Set<Z>().ToListAsync();
                case DBType.Azure:
                    return await _azureDb.Set<Z>().ToListAsync();
                default:
                    return await _localDb.Set<Z>().ToListAsync();
            }
        }

        #endregion  Get

        #region Save

        //Save
        protected async Task<Z> SaveAsync(Z value, bool isNew = true)
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
            return null;
        }
        protected async Task<List<Z>> SaveAllAsync(List<Z> values, bool isNew = true)
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
            return null;
        }

        #endregion
        #region Delete
        //Delete
        protected async Task<bool> DeleteZAsync(string id)
        {
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
        }
        protected async Task<bool> Delete(int id)
        {
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
        }
        protected async Task<bool> DeleteAsync(Z value)
        {
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
        }
        protected async Task<bool> DeleteAsync(List<Z> values)
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

