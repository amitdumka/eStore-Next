using eStoreMobileX.Core.Database;
using eStoreMobileX.Data.RemoteServer;
using Microsoft.EntityFrameworkCore;

public enum DbType { Local, Azure, API }
namespace eStoreMobileX.Data.DataModels.Base
{
    /// <summary>
    /// New Hybrid Model 
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public abstract class HybridDataMode<T> : BaseDataModel<T> where T : class
    {
        protected RemoteServer<T> _service;
        protected AzureDBContext _azureDb;
        protected AppDBContext _localDb;
        protected string _url, _name;
        public DbType Mode;
        public bool FailSafe = false;

        protected HybridDataMode(ConType conType) : base(conType)
        {
            this.InitDatabase();
        }
        protected HybridDataMode(ConType conType, string url, string name) : base(conType)
        {
            _url = url; _name = name;
            this.InitDatabase();
        }

        public override async Task<bool> Delete(int id)
        {
            switch (Mode)
            {
                case DbType.Local:

                    var element = await _localDb.FindAsync<T>(id);
                    _localDb.Remove<T>(element);
                    return (await _localDb.SaveChangesAsync()) > 0;
                    break;
                case DbType.Azure:
                    var azureEle = await _azureDb.FindAsync<T>(id);
                    _azureDb.Remove<T>(azureEle);
                    return (await _azureDb.SaveChangesAsync()) > 0;
                    break;
                case DbType.API:
                    return await _service.DeleteAsync(id);
                    break;
                default:
                    return false;
                    break;
            }

        }

        public override async Task<bool> Delete(string id)
        {
            switch (Mode)
            {
                case DbType.Local:

                    var element = await _localDb.FindAsync<T>(id);
                    _localDb.Remove<T>(element);
                    return (await _localDb.SaveChangesAsync()) > 0;

                    break;
                case DbType.Azure:
                    var azureEle = await _azureDb.FindAsync<T>(id);
                    _azureDb.Remove<T>(azureEle);
                    return (await _azureDb.SaveChangesAsync()) > 0;
                    break;
                case DbType.API:
                    return await _service.DeleteAsync(id);
                    break;
                default:
                    return false;
                    break;
            }
        }

        public override async Task<T> GetById(string id)
        {
            switch (Mode)
            {
                case DbType.Local:
                    return await _localDb.FindAsync<T>(id);
                    break;
                case DbType.Azure:
                    return await _azureDb.FindAsync<T>(id);
                    break;
                case DbType.API:
                    return await _service.GetByIdAsync(id);
                    break;
                default:
                    return null;
                    break;
            }
        }

        public override async Task<T> GetById(int id)
        {
            switch (Mode)
            {
                case DbType.Local:
                    return await _localDb.FindAsync<T>(id);
                    break;
                case DbType.Azure:
                    return await _azureDb.FindAsync<T>(id);
                    break;
                case DbType.API:
                    return await _service.GetByIdAsync(id);
                    break;
                default:
                    return null;
                    break;
            }
        }

        public override bool IsExists(string id)
        {
            switch (Mode)
            {
                case DbType.Local:
                    if (_localDb.Find<T>(id) != null) return true; else return false;
                    break;
                case DbType.Azure:
                    if (_azureDb.Find<T>(id) != null) return true; else return false;
                    break;
                case DbType.API:
                    return false;
                    break;
                default:
                    return false;
                    break;
            }
        }

        public override bool IsExists(int id)
        {
            switch (Mode)
            {
                case DbType.Local:
                    if (_localDb.Find<T>(id) != null) return true; else return false;
                    break;
                case DbType.Azure:
                    if (_azureDb.Find<T>(id) != null) return true; else return false;
                    break;
                case DbType.API:
                    return false;
                    break;
                default:
                    return false;
                    break;
            }
        }

        public override async Task<bool> Save(T item, bool isNew = true)
        {
            switch (Mode)
            {
                case DbType.Local:
                    if (isNew)
                    {
                        await _localDb.AddAsync<T>(item);

                    }
                    else
                    {
                        _localDb.Update<T>(item);
                    }
                    return (await _localDb.SaveChangesAsync()) > 0;

                    break;
                case DbType.Azure:
                    if (isNew)
                    {
                        await _azureDb.AddAsync<T>(item);

                    }
                    else
                    {
                        _azureDb.Update<T>(item);
                    }
                    return (await _azureDb.SaveChangesAsync()) > 0;
                    break;
                case DbType.API:
                    return await _service.SaveAsync(item, isNew);
                    break;
                default:
                    return false;
                    break;
            }
        }

        /// <summary>
        /// Experimental 
        /// </summary>
        /// <returns></returns>
        public override async Task<List<T>> GetItems()
        {
            switch (Mode)
            {
                case DbType.Local:
                    return await _localDb.Set<T>().ToListAsync();
                    break;
                case DbType.Azure:
                    return await _azureDb.Set<T>().ToListAsync();
                    break;
                case DbType.API:
                    return await _service.RefreshDataAsync();
                    break;
                default:
                    return null;
                    break;
            }
        }

        /// <summary>
        /// Init database based on Contype
        /// </summary>
        /// <returns></returns>
        protected bool InitDatabase()
        {
            try
            {

                switch (this.ConType)
                {
                    case ConType.Local:
                        _localDb = new AppDBContext();
                        break;
                    case ConType.Remote:
                        if (!string.IsNullOrEmpty(_url) && !string.IsNullOrEmpty(_name))
                            _service = new RemoteServer<T>(_url, _name);
                        else return false;
                        break;
                    case ConType.RemoteDb:
                        _azureDb = new AzureDBContext();
                        break;
                    case ConType.HybridApi:
                        if (!string.IsNullOrEmpty(_url) && !string.IsNullOrEmpty(_name))
                            _service = new RemoteServer<T>(_url, _name);
                        _localDb = new AppDBContext();
                        break;
                    case ConType.HybridDB:
                        _azureDb = new AzureDBContext();
                        _localDb = new AppDBContext();
                        break;
                    case ConType.Hybrid:
                        _localDb = new AppDBContext();
                        _azureDb = new AzureDBContext();
                        if (!string.IsNullOrEmpty(_url) && !string.IsNullOrEmpty(_name))
                            _service = new RemoteServer<T>(_url, _name);
                        break;
                    default:
                        return false;
                        break;
                }

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }

    }//End of class


}
