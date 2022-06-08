using AKS.Shared.Commons.Models;
using eStoreMobileX.Config;
using eStoreMobileX.Data.DataModels.Stores;
using System.Collections.ObjectModel;

namespace eStoreMobileX.Data.ViewModels.Stores
{
    public class StoreViewModel : BaseViewModel
    {
        private StoreDataModel dm;
        public ObservableCollection<Store> ItemList { get; set; }
        public Store Item { get; set; }
        
        public StoreViewModel()
        {
            this.Item = new Store
            {
                BeginDate = DateTime.Now,IsActive = false,
                MarkedDeleted=false,City="Dumka", Country="India", State="Jharkand"
                ,EndDate=null 
            };

            InitObject();
        }

        public override void InitObject()
        {
            Title = "Store";
            ItemList = new ObservableCollection<Store>();
            ConType = ConType.HybridDB;
            dm = new StoreDataModel(ConType);
            dm.Mode = mode = AppConfig.DbMode;
        }
        
        protected override async void RefreshDataAsync()
        {
            try
            {
                dm = new StoreDataModel(ConType);
                dm.Mode = AppConfig.DbMode;

                List<Store> Data = await dm.GetItems();

                if (Data == null || Data.Count <= 0)
                {
                    //if (await dm.SyncWithServer())
                    //{
                    //    Data = await dm.GetItems();
                    //}
                    // await App.Current.MainPage.DisplayAlert("Info", "Loading Data from server...", "Ok");
                     DisplayAlert("Warning", "Not able to fetch data or No record found", "Ok");
                    //TODO: Alert user  or download from server.
                }
                if (Data != null)
                {
                    ItemList.Clear();
                    foreach (var item in Data)
                    {
                        ItemList.Add(item);
                    }
                }

            }
            catch (Exception e)
            {
                //TODO: Alert user about error.
                DisplayAlert("Exception", "Error: " + e.Message, "Ok");
            }
        }

        public override void ItemsSourceRefresh()
        {
            RefreshDataAsync();
        }

        // TODO: make is genric as much possible
        public void Save() { }
        public void Delete() { }
        public void DeleteSelected() { }
        public void Get(string key) { }
    }
}
