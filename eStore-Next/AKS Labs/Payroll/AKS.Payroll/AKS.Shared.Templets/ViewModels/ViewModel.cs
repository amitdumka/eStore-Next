namespace AKS.Shared.Templets.ViewModels
{
    #region ViewModelTempletes

    public abstract class ViewModel<T, Y, VM, DM>
    {
        public VM _ViewModel { get; set; }
        public List<VM> _ViewModels { get; set; }
        public DM DataModel { get; set; }
        public List<T> PrimaryEntites { get; set; }
        public T PrimaryEntity { get; set; }
        public Y SecondaryEntity { get; set; }
        public List<Y> SecondayEntites { get; set; }
        public string StoreCode { get; set; }
        public bool isNew { get; set; }
        /// <summary>
        /// Alert to UI
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        /// <param name="func"></param>
        public void Alert(string msg, AlertType type, Action<string, AlertType> func)
        {
            func(msg, type);
        }

        //Delete
        public abstract bool Delete(T entity);

        public abstract bool Delete(Y entity);

        public abstract bool DeleteRange(List<T> entities);

        public abstract bool DeleteRange(List<Y> entities);

        public List<T> GetPrimaryEntities()
        { return PrimaryEntites; }

        public List<Y> GetSecondaryEntities()
        { return SecondayEntites; }

        public List<VM> GetViewModels()
        { return _ViewModels; }

        public abstract bool InitViewModel();

        //Save Enties
        public abstract bool Save(T entity);

        public abstract bool Save(Y entity);
    }

    public abstract class ViewModel<T, DM>
    {
        public DM DataModel { get; set; }
        public List<T> PrimaryEntites { get; set; }
        public T PrimaryEntity { get; set; }
        public string StoreCode { get; set; }
        public bool isNew { get; set; }

        /// <summary>
        /// Alert to UI
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        /// <param name="func"></param>
        public void Alert(string msg, AlertType type, Action<string, AlertType> func)
        {
            func(msg, type);
        }

        //Delete
        public abstract bool Delete(T entity);

        public abstract bool DeleteRange(List<T> entities);


        public List<T> GetPrimaryEntities()
        { return PrimaryEntites; }

        public abstract bool InitViewModel();

        //Save Enties
        public abstract bool Save(T entity);
    }

    public abstract class ViewModel<T, Y, DM>
    {
        public DM DataModel { get; set; }
        public List<T> PrimaryEntites { get; set; }
        public T PrimaryEntity { get; set; }
        public Y SecondaryEntity { get; set; }
        public List<Y> SecondayEntites { get; set; }
        public string StoreCode { get; set; }
        public bool isNew { get; set; }

        /// <summary>
        /// Alert to UI
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        /// <param name="func"></param>
        public void Alert(string msg, AlertType type, Action<string, AlertType> func)
        {
            func(msg, type);
        }

        //Delete
        public abstract bool Delete(T entity);

        public abstract bool Delete(Y entity);

        public abstract bool DeleteRange(List<T> entities);

        public abstract bool DeleteRange(List<Y> entities);

        public List<T> GetPrimaryEntities()
        { return PrimaryEntites; }

        public List<Y> GetSecondaryEntities()
        { return SecondayEntites; }

        public abstract bool InitViewModel();

        //Save Enties
        public abstract bool Save(T entity);

        public abstract bool Save(Y entity);
    }

    #endregion ViewModelTempletes
}