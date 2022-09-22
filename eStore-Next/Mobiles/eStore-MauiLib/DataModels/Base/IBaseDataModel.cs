using System;
namespace eStore_MauiLib.DataModels.Base
{
    public interface IBaseDataModel<T>
	{
		//Get By ID
		protected abstract T Get(string id);
		protected abstract T Get(int id);

		//Get Items
		protected abstract List<T> GetItems(string storeid);
		protected abstract List<T> GetItems();
		protected abstract List<T> GetFiltered(QueryParam query);

		//Save
		protected abstract T Save(T value,bool isNew = true);
		protected abstract List<T> SaveAll(List<T> values, bool isNew = true);

		//Delete
		protected abstract bool Delete(string id);
		protected abstract bool Delete(int id);
		protected abstract bool Delete(T value);
		protected abstract bool Delete(List<T> values);

		//YearList
		protected abstract List<int> GetYearList(string storeid);
		protected abstract List<int> GetYearList();

	}

	public interface IBaseDataModel<T, Y> : IBaseDataModel<T>
	{
        //Get By ID
        protected Y GetY(string id);
        protected Y GetY(int id);

        //Get Items
        protected List<Y> GetYItems(string storeid);
        protected List<Y> GetYItems();
        protected List<Y> GetYFiltered(QueryParam query);

        //Save
        protected Y Save(Y value, bool isNew = true);
        protected List<Y> SaveAll(List<Y> values, bool isNew = true);

        //Delete
        protected bool DeleteY(string id);
        protected bool DeleteY(int id);
        protected bool Delete(Y value);
        protected bool Delete(List<Y> values);

        //YearList
        protected List<int> GetYearListY(string storeid);
        protected List<int> GetYearListY();
    }
    public interface IBaseDataModel<T, Y,Z> : IBaseDataModel<T,Y>
    {
        //Get By ID
        protected Z GetZ(string id);
        protected Z GetZ(int id);

        //Get Items
        protected List<Z> GetZItems(string storeid);
        protected List<Z> GetZItems();
        protected List<Z> GetZFiltered(QueryParam query);

        //Save
        protected Z Save(Z value, bool isNew = true);
        protected List<Z> SaveAll(List<Z> values, bool isNew = true);

        //Delete
        protected bool DeleteZ(string id);
        protected bool DeleteZ(int id);
        protected bool Delete(Z value);
        protected bool Delete(List<Z> values);

        //YearList
        protected List<int> GetYearListZ(string storeid);
        protected List<int> GetYearListZ();
    }
}

