using System;

public enum ConType { Local, Remote, RemoteDb, HybridApi, HybridDB, Hybrid }
namespace eStore_MauiLib.DataModels
{
    public class QueryParam
    {
        public int Id { get; set; }
        /// <summary>
        /// Use this when ID is of string type
        /// </summary>
        public string Ids { get; set; }
        public List<string> Command { get; set; }
        public List<string> Query { get; set; }
        public int Order { get; set; }
        public List<string> Filters { get; set; }
        public int StoreId { get; set; }

    }

    public abstract class BaseDataModel<T> //where T:class
	{
        public DBType ConType { get; set; }
        public List<T> Entity { get; set; }

        public BaseDataModel(DBType conType)
        {
            ConType = conType;

        }
        public abstract Task<bool> Delete(int id);
        public abstract Task<bool> Delete(string id);
        public abstract Task<T> GetById(string id);
        public abstract bool IsExists(string id);
        public abstract Task<T> GetById(int id);
        public abstract bool IsExists(int id);
        public abstract Task<T> Save(T item, bool isNew = true);
        public abstract Task<List<T>> FindAsync(QueryParam query);
        public abstract Task<List<T>> GetItems(int storeid);
        public abstract Task<List<T>> GetItems(string storeid);
        public abstract Task<List<T>> GetItems();

    }


    public abstract class BaseDataModel<T, Y> :BaseDataModel<T> //where Y :class 
    {
        public BaseDataModel(DBType conType) : base(conType)
        {
        }
        public abstract Task<bool> DeleteY(int id);
        public abstract Task<bool> DeleteY(string id);
        public abstract Task<Y> GetByIdY(string id);
        public abstract bool IsExistsY(string id);
        public abstract Task<Y> GetByIdY(int id);
        public abstract bool IsExistsY(int id);
        public abstract Task<Y> SaveY(Y item, bool isNew = true);
        public abstract Task<List< Y>> FindAsyncY(QueryParam query);
        public abstract Task<List<Y>> GetItemsY(int storeid);
        public abstract Task<List<Y>> GetItemsY(string storeid);
        public abstract Task<List<Y>> GetItemsY();
    }

    public abstract class BaseDataModel<T, Y, Z> : BaseDataModel<T, Y>
    {
        public BaseDataModel(DBType conType) : base(conType)
        {
        }
        public abstract Task<bool> DeleteZ(int id);
        public abstract Task<bool> DeleteZ(string id);
        public abstract Task<Z> GetByIdZ(string id);
        public abstract bool IsExistsZ(string id);
        public abstract Task<Z> GetByIdZ(int id);
        public abstract bool IsExistsZ(int id);
        public abstract Task<Z> SaveZ(Z item, bool isNew = true);
        public abstract Task<List<Z>> FindAsyncZ(QueryParam query);
        public abstract Task<List<Z>> GetItemsZ(int storeid);
        public abstract Task<List<Z>> GetItemsZ(string storeid);
        public abstract Task<List<Z>> GetItemsZ();
    }
}

