using eStoreMobileX.Core.Database;
using eStoreMobileX.Data.RemoteServer;
namespace eStoreMobileX.Data.DataModels.Base
{
    public abstract class HybridDataModel<T> where T : class
    {
        public List<T> Entity { get; set; }
        protected AppDBContext _context;
        protected RemoteServer<T> service;
        protected AzureDBContext _azureDb;

        public HybridDataModel(string url, string name)
        {
            service = new RemoteServer<T>(url, name);
        }
        public async Task<T> GetById(int id, bool isLocal = true) { return null; }
        public async Task<bool> Save(T item, bool isNew = true, bool isLocal = true) { return false; }
        public async Task<bool> Delete(int id, bool isLocal = true) { return true; }
        public bool IsExists(int id) { return false; }
        public abstract Task<List<T>> FindAsync(QueryParam query, bool isLocal = true);
        public abstract Task<List<T>> GetItems(int storeid, bool isLocal = true);

    }
    public abstract class HybridDataModel2<T> where T : class
    {
        public List<T> Entity { get; set; }
        protected AppDBContext _context;
        protected RemoteServer<T> service;
        protected AzureDBContext _azureDb;

        public HybridDataModel2(string url, string name)
        {
            service = new RemoteServer<T>(url, name);
        }
        public async Task<bool> Delete(int id, bool isLocal = true)
        {
            if (isLocal)
            {
                using (_context = new AppDBContext())
                {
                    var element = await _context.FindAsync<T>(id);
                    _context.Remove<T>(element);
                    return (await _context.SaveChangesAsync()) > 0;
                }
            }
            else
            {
                return await service.DeleteAsync(id);

            }
        }
        public async Task<T> GetById(int id, bool isLocal = true)
        {
            if (isLocal)
                using (_context = new AppDBContext())
                {
                    return _context.Find<T>(id);
                }
            else
            {
                return await service.GetByIdAsync(id);
            }
        }
        /// <summary>
        /// It is a function which check in local database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsExists(int id)
        {
            if (_context.Find<T>(id) != null) return true; else return false;
        }

        /// <summary>
        ///  Save Data to Local or Server.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isNew"></param>
        /// <param name="isLocal"></param>
        /// <returns></returns>
        public async Task<bool> Save(T item, bool isNew = true, bool isLocal = true)
        {
            if (isLocal)
            {
                using (_context = new AppDBContext())
                {

                    if (isNew)
                    {
                        await _context.AddAsync<T>(item);

                    }
                    else
                    {
                        _context.Update<T>(item);
                    }
                    return (await _context.SaveChangesAsync()) > 0;
                }
            }
            else
            {
                return await service.SaveAsync(item, isNew);
            }
        }
        public abstract Task<List<T>> FindAsync(QueryParam query, bool isLocal = true);
        public abstract Task<List<T>> GetItems(int storeid, bool isLocal = true);



    }


}
