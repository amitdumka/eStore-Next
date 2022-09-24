using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DevExpress.Maui.DataForm;


namespace eStore_MauiLib.ViewModels
{
    [ObservableRecipient]
    public abstract partial class BaseEntryViewModel<T,DM> : ObservableValidator
    {
        
        protected DM DataModel;
   
        [ObservableProperty]
        protected string _title;
   
        [ObservableProperty]
        protected string _icon;
   
        [ObservableProperty]
        protected bool _isNew;
   
        [ObservableProperty]
        protected UserType _role;

        [RelayCommand]
        protected abstract void Save();

        [RelayCommand]
        protected abstract void Cancle();

        [RelayCommand]
        protected abstract void InitViewModel();
    }
}
