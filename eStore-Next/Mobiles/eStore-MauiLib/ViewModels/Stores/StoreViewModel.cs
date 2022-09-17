using AKS.Shared.Commons.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eStore_MauiLib.DataModels.Auths;
using eStore_MauiLib.DataModels.Stores;
using System.ComponentModel.DataAnnotations;

namespace eStore_MauiLib.ViewModels.Stores
{
    public partial class StoreViewModel : BaseViewModel<Store, StoreDataModel>
    {
        #region Field
        

        [ObservableProperty]
        private string _storeCode;

        [ObservableProperty]
        [NotifyPropertyChangedRecipients]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        private string? _storeName;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        private string _address;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        private string _city;

        [ObservableProperty]
        private string _country;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        private string _zip;

        #endregion Field

        #region Constructors

        public StoreViewModel() : base()
        {
            InitViewModelAsync();
        }

        #endregion Constructors

        #region Methods

        

        protected async Task InitViewModelAsync()
        {
            _title = "Stores";
            DataModel = new StoreDataModel(ConType.Hybrid);
            DataModel.Mode = DBType.Local;
            DataModel.Connect();
            var x = await DataModel.GetItems();
            UpdateEntities(x);
        }
        protected void UpdateEntities(List<Store> stores)
        {
            if (Entities == null) Entities = new System.Collections.ObjectModel.ObservableCollection<Store>();
            foreach (var store in stores)
            {
                Entities.Add(store);
            }
            RecordCount = Entities.Count;
        }

        protected override async Task<bool> Save(bool isNew = true)
        {
            return false;
        }

        protected override Task<bool> Delete()
        {
            throw new NotImplementedException();
        }

        protected override Task<List<Store>> Filter(string fitler)
        {
            throw new NotImplementedException();
        }

        protected override Task<Store> Get(string id)
        {
            throw new NotImplementedException();
        }

        protected override Task<Store> GetById(int id)
        {
            throw new NotImplementedException();
        }

        protected override async Task<List<Store>> GetList()
        {
            if(Entities==null || Entities.Count == 0)
            {
                UpdateEntities(await DataModel.GetItems());
            }
            return Entities.ToList();
        }

        protected override void AddButton()
        {
            throw new NotImplementedException();
        }

        protected override void DeleteButton()
        {
            throw new NotImplementedException();
        }

        protected override void RefreshButton()
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}