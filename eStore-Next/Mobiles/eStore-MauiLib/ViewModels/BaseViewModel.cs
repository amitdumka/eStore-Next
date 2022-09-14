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
        protected bool _isNew;

        [ObservableProperty]
        protected T _entity;

        [ObservableProperty]
        protected ObservableCollection<T> _entities;

        #endregion


        #region Constructors
        public BaseViewModel()
        {

        }
        #endregion
        #region EventHandler
        #endregion
        #region Commands

        #endregion

        #region Methods

        #endregion

        #region Abstractfunctions
        [RelayCommand]
        protected abstract Task<bool> Save(bool isNew = false);
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
        #endregion


    }
}

//public class <ClassName> : BaseViewModel<T>
//{
//    #region Field

//    #endregion
//    #region Constructors

//    #endregion
//    #region EventHandler
//    #endregion
//    #region Commands

//    #endregion

//    #region Methods

//    #endregion
//    #region Abstractfunctions

//    #endregion
//}


