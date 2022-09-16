using AKS.MAUI.Databases;
using Microsoft.EntityFrameworkCore;

namespace eStore_MauiLib.DataModels
{
    public abstract class BaseDataModel<T> where T : class
    {
        //TODO: remove default;
        public string StoreCode = "ARD";

        public DBType Mode { get; set; }
        public ConType ConType { get; set; }
        public List<T> Entity { get; set; }

        // Currently local and azure sql db
        protected AppDBContext _localDb, _azureDb;

        /// <summary>
        /// Return Current DatabaseContext;
        /// </summary>
        /// <returns></returns>

        public AppDBContext GetContextLocal() => _localDb;

        public AppDBContext GetContextAzure() => _azureDb;

        public abstract List<int> GetYearList();

        public BaseDataModel(ConType conType)
        {
            ConType = conType;
        }
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

        /// <summary>
        /// Delete a record whose ID is type of INT
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int id)
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

                //case DBType.API:
                //    return await _service.DeleteAsync(id);
                //    break;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Delete an record whose ID is type of STRING
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string id)
        {
            switch (Mode)
            {
                case DBType.Local:

                    var element = await _localDb.FindAsync<T>(id);
                    _localDb.Remove<T>(element);
                    return (await _localDb.SaveChangesAsync()) > 0;
                    break;

                case DBType.Azure:
                    var azureEle = await _azureDb.FindAsync<T>(id);
                    _azureDb.Remove<T>(azureEle);
                    return (await _azureDb.SaveChangesAsync()) > 0;
                    break;
                //case DBType.API:
                //    return await _service.DeleteAsync(id);
                //    break;
                default:
                    return false;
                    break;
            }
        }

        /// <summary>
        /// GetById whose ID is type string
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetById(string id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.FindAsync<T>(id);
                    break;

                case DBType.Azure:
                    return await _azureDb.FindAsync<T>(id);
                    break;
                //case DBType.API:
                //    return await _service.GetByIdAsync(id);
                //    break;
                default:
                    return null;
                    break;
            }
        }

        /// <summary>
        /// Get By Id whose id is type of INT
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetById(int id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.FindAsync<T>(id);
                    break;

                case DBType.Azure:
                    return await _azureDb.FindAsync<T>(id);
                    break;
                //case DBType.API:
                //    return await _service.GetByIdAsync(id);
                //    break;
                default:
                    return null;
                    break;
            }
        }

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

        /// <summary>
        /// Save or Update record
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        public async Task<T> Save(T item, bool isNew = true)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (isNew)
                    {
                        await _localDb.AddAsync<T>(item);
                    }
                    else
                    {
                        _localDb.Update<T>(item);
                    }

                    if (await _localDb.SaveChangesAsync() > 0) return item;

                    break;

                case DBType.Azure:
                    if (isNew)
                    {
                        await _azureDb.AddAsync<T>(item);
                    }
                    else
                    {
                        _azureDb.Update<T>(item);
                    }
                    if (await _azureDb.SaveChangesAsync() > 0) return item;
                    break;
                //case DBType.API:
                //    return await _service.SaveAsync(item, isNew);
                //    break;
                default:
                    return null;
            }
            return null;
        }

        public abstract Task<List<T>> FindAsync(QueryParam query);

        public abstract Task<List<T>> GetItems(int storeid);

        public abstract Task<List<T>> GetItems(string storeid);

        /// <summary>
        /// Get all items. It is Expermimental
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetItems()
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.Set<T>().ToListAsync();
                    

                case DBType.Azure:
                    return await _azureDb.Set<T>().ToListAsync();
                    
                //case DBType.API:
                //    return await _service.RefreshDataAsync();
                //    break;
                default:
                    return await _localDb.Set<T>().ToListAsync();

            }
        }

        /// <summary>
        /// Init database based on Contype
        /// </summary>
        /// <returns></returns>
        //protected bool InitDatabase()
        //{
        //    try
        //    {
        //        switch (this.ConType)
        //        {
        //            case ConType.Local:
        //                _localDb = new AppDBContext();
        //                break;
        //            case ConType.Remote:
        //                if (!string.IsNullOrEmpty(_url) && !string.IsNullOrEmpty(_name))
        //                    _service = new RemoteServer<T>(_url, _name);
        //                else return false;
        //                break;
        //            case ConType.RemoteDb:
        //                _azureDb = new AzureDBContext();
        //                break;
        //            case ConType.HybridApi:
        //                if (!string.IsNullOrEmpty(_url) && !string.IsNullOrEmpty(_name))
        //                    _service = new RemoteServer<T>(_url, _name);
        //                _localDb = new AppDBContext();
        //                break;
        //            case ConType.HybridDB:
        //                _azureDb = new AzureDBContext();
        //                _localDb = new AppDBContext();
        //                break;
        //            case ConType.Hybrid:
        //                _localDb = new AppDBContext();
        //                _azureDb = new AzureDBContext();
        //                if (!string.IsNullOrEmpty(_url) && !string.IsNullOrEmpty(_name))
        //                    _service = new RemoteServer<T>(_url, _name);
        //                break;
        //            default:
        //                return false;
        //                break;
        //        }

        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}
    }

    public abstract class BaseDataModel<T, Y> : BaseDataModel<T> where Y : class where T : class
    {
        public BaseDataModel(ConType conType) : base(conType)
        {
        }

        /// <summary>
        /// Delete a record whose ID is type of INT
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteY(int id)
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

                //case DBType.API:
                //    return await _service.DeleteAsync(id);
                //    break;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Delete an record whose ID is type of STRING
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteY(string id)
        {
            switch (Mode)
            {
                case DBType.Local:

                    var element = await _localDb.FindAsync<Y>(id);
                    _localDb.Remove<Y>(element);
                    return (await _localDb.SaveChangesAsync()) > 0;
                    break;

                case DBType.Azure:
                    var azureEle = await _azureDb.FindAsync<Y>(id);
                    _azureDb.Remove<Y>(azureEle);
                    return (await _azureDb.SaveChangesAsync()) > 0;
                    break;
                //case DBType.API:
                //    return await _service.DeleteAsync(id);
                //    break;
                default:
                    return false;
                    break;
            }
        }

        /// <summary>
        /// GetById whose ID is type string
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Y> GetYById(string id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.FindAsync<Y>(id);
                    break;

                case DBType.Azure:
                    return await _azureDb.FindAsync<Y>(id);
                    break;
                //case DBType.API:
                //    return await _service.GetByIdAsync(id);
                //    break;
                default:
                    return null;
                    break;
            }
        }

        /// <summary>
        /// Get By Id whose id is type of INT
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Y> GetYById(int id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.FindAsync<Y>(id);
                    break;

                case DBType.Azure:
                    return await _azureDb.FindAsync<Y>(id);
                    break;
                //case DBType.API:
                //    return await _service.GetByIdAsync(id);
                //    break;
                default:
                    return null;
                    break;
            }
        }

        public async Task<bool> IsYExists(string id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (await _localDb.FindAsync<Y>(id) != null) return true; else return false;
                    break;

                case DBType.Azure:
                    if (await _azureDb.FindAsync<Y>(id) != null) return true; else return false;
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
                    if (await _localDb.FindAsync<Y>(id) != null) return true; else return false;
                    break;

                case DBType.Azure:
                    if (await _azureDb.FindAsync<Y>(id) != null) return true; else return false;
                    break;

                case DBType.API:
                    return false;
                    break;

                default:
                    return false;
                    break;
            }
        }

        /// <summary>
        /// Save or Update record
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        public async Task<Y> SaveY(Y item, bool isNew = true)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (isNew)
                    {
                        await _localDb.AddAsync<Y>(item);
                    }
                    else
                    {
                        _localDb.Update<Y>(item);
                    }

                    if (await _localDb.SaveChangesAsync() > 0) return item;

                    break;

                case DBType.Azure:
                    if (isNew)
                    {
                        await _azureDb.AddAsync<Y>(item);
                    }
                    else
                    {
                        _azureDb.Update<Y>(item);
                    }
                    if (await _azureDb.SaveChangesAsync() > 0) return item;
                    break;
                //case DBType.API:
                //    return await _service.SaveAsync(item, isNew);
                //    break;
                default:
                    return null;
            }
            return null;
        }

        public abstract Task<List<Y>> FindYAsync(QueryParam query);

        public abstract Task<List<Y>> GetYItems(int storeid);

        public abstract Task<List<Y>> GetYItems(string storeid);

        /// <summary>
        /// Get all items. It is Expermimental
        /// </summary>
        /// <returns></returns>
        public async Task<List<Y>> GetYItems()
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.Set<Y>().ToListAsync();
                    break;

                case DBType.Azure:
                    return await _azureDb.Set<Y>().ToListAsync();
                    break;
                //case DBType.API:
                //    return await _service.RefreshDataAsync();
                //    break;
                default:
                    return null;
                    break;
            }
        }
    }

    public abstract class BaseDataModel<T, Y, Z> : BaseDataModel<T> where Y : class where T : class where Z : class
    {
        public BaseDataModel(ConType conType) : base(conType)
        {
        }

        /// <summary>
        /// Delete a record whose ID is type of INT
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteZ(int id)
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

                //case DBType.API:
                //    return await _service.DeleteAsync(id);
                //    break;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Delete an record whose ID is type of STRING
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteZ(string id)
        {
            switch (Mode)
            {
                case DBType.Local:

                    var element = await _localDb.FindAsync<Z>(id);
                    _localDb.Remove<Z>(element);
                    return (await _localDb.SaveChangesAsync()) > 0;
                    break;

                case DBType.Azure:
                    var azureEle = await _azureDb.FindAsync<Z>(id);
                    _azureDb.Remove<Z>(azureEle);
                    return (await _azureDb.SaveChangesAsync()) > 0;
                    break;
                //case DBType.API:
                //    return await _service.DeleteAsync(id);
                //    break;
                default:
                    return false;
                    break;
            }
        }

        /// <summary>
        /// GetById whose ID is type string
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Z> GetZById(string id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.FindAsync<Z>(id);
                    break;

                case DBType.Azure:
                    return await _azureDb.FindAsync<Z>(id);
                    break;
                //case DBType.API:
                //    return await _service.GetByIdAsync(id);
                //    break;
                default:
                    return null;
                    break;
            }
        }

        /// <summary>
        /// Get By Id whose id is type of INT
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Z> GetZById(int id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.FindAsync<Z>(id);
                    break;

                case DBType.Azure:
                    return await _azureDb.FindAsync<Z>(id);
                    break;
                //case DBType.API:
                //    return await _service.GetByIdAsync(id);
                //    break;
                default:
                    return null;
                    break;
            }
        }

        public async Task<bool> IsZExists(string id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (await _localDb.FindAsync<Z>(id) != null) return true; else return false;
                    break;

                case DBType.Azure:
                    if (await _azureDb.FindAsync<Z>(id) != null) return true; else return false;
                    break;

                case DBType.API:
                    return false;
                    break;

                default:
                    return false;
                    break;
            }
        }

        public async Task<bool> IsZExists(int id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (await _localDb.FindAsync<Z>(id) != null) return true; else return false;
                    break;

                case DBType.Azure:
                    if (await _azureDb.FindAsync<Z>(id) != null) return true; else return false;
                    break;

                case DBType.API:
                    return false;
                    break;

                default:
                    return false;
                    break;
            }
        }

        /// <summary>
        /// Save or Update record
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        public async Task<Z> SaveZ(Z item, bool isNew = true)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (isNew)
                    {
                        await _localDb.AddAsync<Z>(item);
                    }
                    else
                    {
                        _localDb.Update<Z>(item);
                    }

                    if (await _localDb.SaveChangesAsync() > 0) return item;

                    break;

                case DBType.Azure:
                    if (isNew)
                    {
                        await _azureDb.AddAsync<Z>(item);
                    }
                    else
                    {
                        _azureDb.Update<Z>(item);
                    }
                    if (await _azureDb.SaveChangesAsync() > 0) return item;
                    break;
                //case DBType.API:
                //    return await _service.SaveAsync(item, isNew);
                //    break;
                default:
                    return null;
            }
            return null;
        }

        public abstract Task<List<Z>> FindZAsync(QueryParam query);

        public abstract Task<List<Z>> GetZItems(int storeid);

        public abstract Task<List<Z>> GetZItems(string storeid);

        /// <summary>
        /// Get all items. It is Expermimental
        /// </summary>
        /// <returns></returns>
        public async Task<List<Z>> GetZItems()
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.Set<Z>().ToListAsync();
                    break;

                case DBType.Azure:
                    return await _azureDb.Set<Z>().ToListAsync();
                    break;
                //case DBType.API:
                //    return await _service.RefreshDataAsync();
                //    break;
                default:
                    return null;
                    break;
            }
        }
    }
}