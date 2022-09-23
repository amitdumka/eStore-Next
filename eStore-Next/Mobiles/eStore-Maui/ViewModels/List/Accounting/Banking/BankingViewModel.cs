using System;
using AKS.Shared.Commons.Models.Banking;
using eStore_MauiLib.DataModels.Accounting;
using eStore_MauiLib.ViewModels;

namespace eStore_Maui.ViewModels.List.Accounting.Banking
{
	public class BankingViewModel:BaseViewModel<Bank, BankingDataModel>
	{
		public BankingViewModel()
		{
		}

        protected override void AddButton()
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> Delete()
        {
            throw new NotImplementedException();
        }

        protected override void DeleteButton()
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> Edit(Bank value)
        {
            throw new NotImplementedException();
        }

        protected override Task<List<Bank>> Filter(string fitler)
        {
            throw new NotImplementedException();
        }

        protected override Task<Bank> Get(string id)
        {
            throw new NotImplementedException();
        }

        protected override Task<Bank> GetById(int id)
        {
            throw new NotImplementedException();
        }

        protected override Task<List<Bank>> GetList()
        {
            throw new NotImplementedException();
        }

        protected override void InitViewModel()
        {
            throw new NotImplementedException();
        }

        protected override void RefreshButton()
        {
            throw new NotImplementedException();
        }

         

        protected override void UpdateEntities(List<Bank> values)
        {
            throw new NotImplementedException();
        }
    }
}

