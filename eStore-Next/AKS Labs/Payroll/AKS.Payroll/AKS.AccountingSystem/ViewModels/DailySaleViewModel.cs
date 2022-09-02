using AKS.AccountingSystem.DataModels;
using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Templets.ViewModels;

namespace AKS.AccountingSystem.ViewModels
{
    public class DailySaleViewModel : ViewModel<DailySale, CustomerDue, DailySaleVM, DailySaleDataModel>
    {
        //TODO: Use this class for CustomerDue entry  and for listing and editing for dues use dueviewmodel
        #region Declarations
        //private AzurePayrollDbContext azureDb;
        //private LocalPayrollDbContext localDb;
        private ObservableListSource<DailySaleVM> dailySaleVMs;//PrimaryVM
        private List<int> YearList;
        private List<int> YearDataList;
        private static string StoreCode = "ARD";
        private List<DueRecovery> dueRecoveryList;//
        private List<CustomerDue> customerDues;//Secondary
        #endregion
        #region OverrideMethods

        public override bool Delete(DailySale entity)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(CustomerDue entity)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRange(List<DailySale> entities)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRange(List<CustomerDue> entities)
        {
            throw new NotImplementedException();
        }

        public override bool InitViewModel()
        {
            throw new NotImplementedException();
        }

        public override bool Save(DailySale entity)
        {
            throw new NotImplementedException();
        }

        public override bool Save(CustomerDue entity)
        {
            throw new NotImplementedException();
        }

        #endregion OverrideMethods
    }


    public class DueViewModel : ViewModel<CustomerDue, DueRecovery, DailySaleDataModel>
    {
        #region Decalarations
        //Primary Enity Due, Seconday Enity Recovery
        #endregion
        #region OverrideMethods


        public override bool Delete(CustomerDue entity)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(DueRecovery entity)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRange(List<CustomerDue> entities)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRange(List<DueRecovery> entities)
        {
            throw new NotImplementedException();
        }

        public override bool InitViewModel()
        {
            throw new NotImplementedException();
        }

        public override bool Save(CustomerDue entity)
        {
            throw new NotImplementedException();
        }

        public override bool Save(DueRecovery entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}