using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eStore_MauiLib.DataModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace eStore_MauiLib.ViewModels
{
    [ObservableRecipient]
    public abstract partial class BaseViewModel<T, DM> : ObservableValidator
    {
        public const string Descending = "Descending";
        public const string Ascending = "Ascending";

        #region Field

        protected DM DataModel;

        [ObservableProperty]
        protected Page _currentPage;

        [ObservableProperty]
        protected string _defaultSortedColName;

        [ObservableProperty]
        protected string _defaultSortedOrder = Descending;

        [ObservableProperty]
        protected string _icon;

        [ObservableProperty]
        protected string _title;

        //[ObservableProperty]
        //protected bool _isNew;

        [ObservableProperty]
        protected T _entity;


        [ObservableProperty]
        protected ObservableCollection<T> _entities;

        [ObservableProperty]
        protected int _recordCount;

        [ObservableProperty]
        protected UserType _role;

        #endregion

        public DM GetDataModel() => DataModel;

        #region Constructors
        public BaseViewModel()
        {

        }


        #endregion



        #region Abstractfunctions

        [RelayCommand]
        protected abstract Task<bool> Edit(T value);

        [RelayCommand]
        protected abstract Task<bool> Delete();

        [RelayCommand]
        protected abstract Task<T> Get(string id);

        [RelayCommand]
        protected abstract Task<T> GetById(int id);

        [RelayCommand]
        protected abstract Task<List<T>> GetList();

        [RelayCommand]
        protected abstract Task<List<T>> Filter(string fitler);

        [RelayCommand]
        protected abstract void AddButton();

        [RelayCommand]
        protected abstract void DeleteButton();

        [RelayCommand]
        protected abstract void RefreshButton();

        protected abstract void InitViewModel();
        protected abstract void UpdateEntities(List<T> values);
        #endregion


    }

    [ObservableRecipient]
    public abstract partial class BaseDashoardViewModel<T> : ObservableObject
    {
        public const string Descending = "Descending";
        public const string Ascending = "Ascending";
        protected DashboardDataModel DataModel;

        [ObservableProperty]
        protected Page _currentPage;

        [ObservableProperty]
        protected string _defaultSortedColName;

        [ObservableProperty]
        protected string _defaultSortedOrder = Descending;

        [ObservableProperty]
        protected string _icon;

        [ObservableProperty]
        protected string _title;

        //[ObservableProperty]
        //protected bool _isNew;

        [ObservableProperty]
        protected T _entity;


        // [ObservableProperty]
        // protected ObservableCollection<T> _entities;

        // [ObservableProperty]
        //  protected int _recordCount;

        [ObservableProperty]
        protected UserType _role;

        public DashboardDataModel GetDataModel() => DataModel;

    }
}