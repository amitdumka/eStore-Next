using AKS.Shared.Commons.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using eStore_MauiLib.DataModels.Stores;
using System.ComponentModel.DataAnnotations;

namespace eStore_MauiLib.ViewModels
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
        }

        #endregion Constructors

        #region Methods

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

        protected override Task<List<Store>> GetList()
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}