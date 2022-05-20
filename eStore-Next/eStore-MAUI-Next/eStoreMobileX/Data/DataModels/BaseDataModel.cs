using eStoreMobileX.Core.Database;
using eStoreMobileX.Data.RemoteServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreMobileX.Data.DataModels.Base
{
    public class QueryParam
    {
        public int Id { get; set; }
        public List<string> Command { get; set; }
        public List<string> Query { get; set; }
        public int Order { get; set; }
        public List<string> Filters { get; set; }
        public int StoreId { get; set; }

    }
    public abstract class HybridDataModel<T> where T : class
    {
        public List<T> Entity { get; set; }
        protected AppDBContext _context;
        protected RemoteServer<T> service;

        public HybridDataModel(string url, string name)
        {
            service = new RemoteServer<T>(url, name);
        }
        public async Task<T> GetById(int id, bool isLocal = true) { return null;  }
        public async Task<bool> Save(T item, bool isNew = true, bool isLocal = true) { return false; }
        public  async Task<bool> Delete(int id, bool isLocal = true) { return true; }
        public bool IsExists(int id) { return false; }
        public abstract Task<List<T>> FindAsync(QueryParam query, bool isLocal = true);
        public abstract Task<List<T>> GetItems(int storeid, bool isLocal = true);

    }
    public abstract class ServerDataModel<T> where T : class
    {
        public List<T> Entity { get; set; }
        protected AppDBContext _context;

        public void Get() { }
        public void GetById(int id) { }
        public void GetById(string id) { }

        public void Find(QueryParam queryParams) { }

        public void Save(T item, bool isNew = true) { }
        public void Save(List<T> items, bool isNew = true) { }

        public void Delete(int id) { }
        public void DeleteSelected(List<int> ids) { }
        public void Delete(string id) { }
        public void DeleteSelected(List<string> ids) { }

    }
    public abstract class LocalDataModel<T> where T : class
    {
        public List<T> Entity { get; set; }
        protected AppDBContext _context;
        public async Task<bool> Delete(int id)
        {
            using (_context = new AppDBContext())
            {
                var element = await _context.FindAsync<T>(id);
                _context.Remove<T>(element);
                return (await _context.SaveChangesAsync()) > 0;
            }
        }
        public async Task<T> GetById(int id)
        {
            using (_context = new AppDBContext())
            {
                return await _context.FindAsync<T>(id);
            }
        }
        public bool IsExists(int id)
        {
            if (_context.Find<T>(id) != null) return true; else return false;
        }

        public async Task<bool> Save(T item, bool isNew = true)
        {
            using (_context = new AppDBContext())
            {
                await _context.AddAsync<T>(item);
                return (await _context.SaveChangesAsync()) > 0;
            }
        }
        public abstract Task<List<T>> FindAsync(QueryParam query);
        public abstract Task<List<T>> GetItems(int storeid);
        //{using (_context = new AppDBContext()) return _context.Query<T>().ToList();}

    }
    public abstract class HybridDataModel2<T> where T : class
    {
        public List<T> Entity { get; set; }
        protected AppDBContext _context;
        protected RemoteServer<T> service;

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
        //{using (_context = new AppDBContext()) return _context.Query<T>().ToList();}


    }
}
