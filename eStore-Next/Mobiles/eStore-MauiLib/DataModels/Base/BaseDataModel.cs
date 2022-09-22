
using AKS.MAUI.Databases;

namespace eStore_MauiLib.DataModels.Base
{
    public abstract class BaseDataModel<T>
	{
        #region Fields
        public string StoreCode;
        public UserType Role;

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
            Role = UserType.Employees;
        }
        public BaseDataModel(ConType conType, UserType role)
        {
            ConType = conType;
            Role = role;
        }
        #endregion


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

	public abstract class BaseDataModel<T, Y> : BaseDataModel<T>
	{
        protected BaseDataModel(ConType conType) : base(conType)
        {
        }

        protected BaseDataModel(ConType conType, UserType role) : base(conType, role)
        {
        }

        //Get By ID
        protected abstract Y GetY(string id);
        protected abstract Y GetY(int id);

        //Get Items
        protected abstract List<Y> GetYItems(string storeid);
        protected abstract List<Y> GetYItems();
        protected abstract List<Y> GetYFiltered(QueryParam query);

        //Save
        protected abstract Y Save(Y value, bool isNew = true);
        protected abstract List<Y> SaveAll(List<Y> values, bool isNew = true);

        //Delete
        protected abstract bool DeleteY(string id);
        protected abstract bool DeleteY(int id);
        protected abstract bool Delete(Y value);
        protected abstract bool Delete(List<Y> values);

        //YearList
        protected abstract List<int> GetYearListY(string storeid);
        protected abstract List<int> GetYearListY();
    }
    public abstract class BaseDataModel<T, Y,Z> : BaseDataModel<T,Y>
    {
        protected BaseDataModel(ConType conType) : base(conType)
        {
        }

        protected BaseDataModel(ConType conType, UserType role) : base(conType, role)
        {
        }

        #region  Get

        
        //Get By ID
        protected abstract Z GetZ(string id);
        protected abstract Z GetZ(int id);

        //Get Items
        protected abstract List<Z> GetZItems(string storeid);
        protected abstract List<Z> GetZItems();
        protected abstract List<Z> GetZFiltered(QueryParam query);

        #endregion  Get
        #region Save

        //Save
        protected abstract Z Save(Z value, bool isNew = true);
        protected abstract List<Z> SaveAll(List<Z> values, bool isNew = true);
        #endregion
        #region Delete
        //Delete
        protected abstract bool DeleteZ(string id);
        protected abstract bool DeleteZ(int id);
        protected abstract bool Delete(Z value);
        protected abstract bool Delete(List<Z> values);
        #endregion Delete

        #region YearList
        //YearList
        protected abstract List<int> GetYearListZ(string storeid);
        protected abstract List<int> GetYearListZ();
        #endregion
    }
}

