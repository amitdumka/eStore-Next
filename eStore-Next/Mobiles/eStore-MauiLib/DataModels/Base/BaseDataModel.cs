using System;

namespace eStore_MauiLib.DataModels.Base
{
	public abstract class BaseDataModel<T>: IBaseDataModel<T> where T : class
    {
		public BaseDataModel()
		{
		}

        bool IBaseDataModel<T>.Delete(string id)
        {
            throw new NotImplementedException();
        }

        bool IBaseDataModel<T>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        bool IBaseDataModel<T>.Delete(T value)
        {
            throw new NotImplementedException();
        }

        bool IBaseDataModel<T>.Delete(List<T> values)
        {
            throw new NotImplementedException();
        }

        T IBaseDataModel<T>.Get(string id)
        {
            throw new NotImplementedException();
        }

        T IBaseDataModel<T>.Get(int id)
        {
            throw new NotImplementedException();
        }

        List<T> IBaseDataModel<T>.GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        List<T> IBaseDataModel<T>.GetItems(string storeid)
        {
            throw new NotImplementedException();
        }

        List<T> IBaseDataModel<T>.GetItems()
        {
            throw new NotImplementedException();
        }

        List<int> IBaseDataModel<T>.GetYearList(string storeid)
        {
            throw new NotImplementedException();
        }

        List<int> IBaseDataModel<T>.GetYearList()
        {
            throw new NotImplementedException();
        }

        T IBaseDataModel<T>.Save(T value, bool isNew)
        {
            throw new NotImplementedException();
        }

        List<T> IBaseDataModel<T>.SaveAll(List<T> values, bool isNew)
        {
            throw new NotImplementedException();
        }
    }
}

