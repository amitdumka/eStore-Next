using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace eStore_MauiLib.ViewModels
{
    [ObservableRecipient]
    public abstract partial class BaseViewModel<T, DM> : ObservableValidator
    {
        #region Field

        protected DM DataModel;

        [ObservableProperty]
        protected Page _currentPage;

        [ObservableProperty]
        protected string _defaultSortedColName;

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
}