/*
 * @Author: Amit Kumar
 * @Version: 1.0
 * @Date: 30/08/2022
 * @Copyrite: Aks Labs(Amit Kumar)
 * @AuthurEmail: amit.dumka@gmail.com
 */

namespace AKS.PosSystem.ViewModels
{
    #region ViewModelTempletes

    public abstract class ViewModel<T, Y, VM, DM>
    {
        public string StoreCode { get; set; }

        public List<VM> _ViewModels { get; set; }
        public List<T> PrimaryEntites { get; set; }
        public List<Y> SecondayEntites { get; set; }
        public T PrimaryEntity { get; set; }
        public Y SecondaryEntity { get; set; }
        public VM _ViewModel { get; set; }
        public DM DataModel { get; set; }

        public List<T> GetPrimaryEntities()
        { return PrimaryEntites; }

        public List<Y> GetSecondaryEntities()
        { return SecondayEntites; }

        public List<VM> GetViewModels()
        { return _ViewModels; }

        //Save Enties
        public abstract bool Save(T entity);

        public bool Save(Y entity)
        { return false; }

        //Delete
        public abstract bool Delete(T entity);

        public bool Delete(Y entity)
        { return false; }

        public abstract bool DeleteRange(List<T> entities);

        public bool DeleteRange(List<Y> entities)
        { return false; }

        public abstract bool InitViewModel();

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
    }

    public abstract class ViewModel<T, DM>
    {
        public string StoreCode { get; set; }
        public List<T> PrimaryEntites { get; set; }
        public T PrimaryEntity { get; set; }
        public DM DataModel { get; set; }

        public List<T> GetPrimaryEntities()
        { return PrimaryEntites; }

        //Save Enties
        public bool Save(T entity)
        { return false; }

        //Delete
        public bool Delete(T entity)
        { return false; }

        public bool DeleteRange(List<T> entities)
        { return false; }

        public abstract bool InitViewModel();
    }

    public abstract class ViewModel<T, Y, DM>
    {
        public string StoreCode { get; set; }
        public List<T> PrimaryEntites { get; set; }
        public List<Y> SecondayEntites { get; set; }
        public T PrimaryEntity { get; set; }
        public Y SecondaryEntity { get; set; }
        public DM DataModel { get; set; }

        public List<T> GetPrimaryEntities()
        { return PrimaryEntites; }

        public List<Y> GetSecondaryEntities()
        { return SecondayEntites; }

        //Save Enties
        public bool Save(T entity)
        { return false; }

        public bool Save(Y entity)
        { return false; }

        //Delete
        public bool Delete(T entity)
        { return false; }

        public bool Delete(Y entity)
        { return false; }

        public bool DeleteRange(List<T> entities)
        { return false; }

        public bool DeleteRange(List<Y> entities)
        { return false; }

        public abstract bool InitViewModel();
    }

    #endregion ViewModelTempletes
}