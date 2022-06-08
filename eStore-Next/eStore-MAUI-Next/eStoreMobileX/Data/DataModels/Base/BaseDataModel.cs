using eStoreMobileX.Core.Database;
using eStoreMobileX.Data.RemoteServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum ConType { Local, Remote, RemoteDb, HybridApi, HybridDB, Hybrid }
namespace eStoreMobileX.Data.DataModels.Base
{
   

    /// <summary>
    /// This is base class for all basic functions which essenticals
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseDataModel<T> where T : class
    {
        public ConType ConType { get; set; }
        public List<T> Entity { get; set; }
        
        public BaseDataModel(ConType conType )
        {
            ConType = conType;
            
        }

        public abstract Task<bool> Delete(int id);
        public abstract Task<bool> Delete(string id);
        public abstract Task<T> GetById(string id);
        public abstract bool IsExists(string id);
        public abstract Task<T> GetById(int id);
        public abstract bool IsExists(int id);
        public abstract Task<bool> Save(T item, bool isNew = true);
        public abstract Task<List<T>> FindAsync(QueryParam query);
        public abstract Task<List<T>> GetItems(int storeid);
        public abstract Task<List<T>> GetItems(string storeid);
        public abstract Task<List<T>> GetItems();

    }

}
