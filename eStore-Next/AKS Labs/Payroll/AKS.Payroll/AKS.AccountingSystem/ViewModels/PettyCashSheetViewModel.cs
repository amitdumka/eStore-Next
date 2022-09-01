using AKS.AccountingSystem.DataModels;
using AKS.AccountingSystem.DTO;
using AKS.Payroll.Database;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Templets.ViewModels;

namespace AKS.AccountingSystem.ViewModels
{
    public class PettyCashSheetViewModel : ViewModel<PettyCashSheet, CashDetail, PettyCashSheetDataModel>
    {
        #region Declaration

       // private CommonDataModel CommonDataModel;
        private ObservableListSource<PettyCashSheet> ItemList;
        public List<int> YearList; 
        private List<int> DataList;
        private string pNar, rNar, dNar, rcNar;
        public int TotalCurreny = 0, TotalCurrenyAmount = 0;
        private decimal tPay, tRec, tDue, tdRec;
        private CashDetail cashDetail;
        public CashDetail SavedCashDetail;
        private PettyCashSheet? SavedPettyCash;
        private bool EnableCashAdd;

        #endregion Declaration

        #region OverrideMethods

        public override bool Delete(PettyCashSheet entity)
        {
            return DataModel.Delete(entity);
        }

        public override bool Delete(CashDetail entity)
        {
            return DataModel.Delete(entity);
        }

        public override bool DeleteRange(List<PettyCashSheet> entities)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRange(List<CashDetail> entities)
        {
            throw new NotImplementedException();
        }

        public override bool InitViewModel()
        {
            
            DataModel = new PettyCashSheetDataModel();
            DMMapper.InitializeAutomapper();
            this.PrimaryEntites = new List<PettyCashSheet>();
            this.PrimaryEntity = new PettyCashSheet
            {
                OnDate = DateTime.Now
            };
            //TODO: use if usefull other wise remove it
            ItemList = new ObservableListSource<PettyCashSheet>();
            YearList = DataModel.GetYearList();
            tRec = tPay = (decimal)0.0;
            DataList = new List<int>();
            DataList.Add(DateTime.Today.Year);

            UpdateItemList(DataModel.GetList(DateTime.Today.Year));
            SecondayEntites = DataModel.GetYList(DateTime.Today.Year);

            return true;
        }

        public override bool Save(PettyCashSheet entity)
        {
            SavedPettyCash = DataModel.Save(entity);
            if (SavedPettyCash == null) return false;
            return true;
        }

        public override bool Save(CashDetail entity)
        {
            SavedCashDetail = DataModel.Save(entity);
            if (SavedCashDetail == null) return false;
            return true;
        }

        #endregion OverrideMethods

        #region OpsFunctions
        public void SetAdd()
        {
            isNew = true;
            tPay = tRec = tDue = tdRec = 0;
        }
        public void SetEdit() { }
        public bool SavePettyCash(bool read)
        {
            if (Save(PrimaryEntity))
            {
                if (!isNew)
                {
                    ItemList.Remove(ItemList.Where(c => c.Id == PrimaryEntity.Id).FirstOrDefault());
                }
                ItemList.Add(PrimaryEntity);
                if (isNew) EnableCashAdd = true;
            }
            else
            {
                if (isNew)
                    Delete(SavedPettyCash);
            }
            return false;
        }
        public bool SaveCashDetail(bool read)
        {
            return Save(SecondaryEntity);
        }
        public bool DeletePettyCash(PettyCashSheet pcs)
        {
           if(Delete(pcs))
            {
                ItemList.Remove(pcs);
                return true;
            }
            return false;
        }
        public List<PettyCashSheet> GetGridData()
        {
            return ItemList.ToList();
        }
        public List<PettyCashSheet> GetCurrentMonth() { return ItemList.Where(c => c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.Month).ToList(); }
        public List<PettyCashSheet> GetLastMonth() { return ItemList.Where(c => c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.AddMonths(-1).Month).ToList(); }
        public List<PettyCashSheet> GetCurrentYearly() { return ItemList.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList(); }

        public List<CashDetail> GetCashCurrentMonth() { return SecondayEntites.Where(c => c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.Month).ToList(); }
        public List<CashDetail> GetCashLastMonth() { return SecondayEntites.Where(c => c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.AddMonths(-1).Month).ToList(); }
        public List<CashDetail> GetCashCurrentYearly() { return SecondayEntites.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList(); }

        public List<DynVM> GetStoreList()
        {
            return CommonDataModel.GetStoreList(DataModel.GetDatabaseInstance());
        }
        #endregion
        #region PrivateOpsFuncs
        private ObservableListSource<PettyCashSheet> UpdateItemList(List<PettyCashSheet> items)
        {
            foreach (var item in items)
            {
                ItemList.Add(item);
            }
            return ItemList;
        }

        #endregion

        #region EntryFunctions

        #endregion

        #region ReportSections  
        public string GeneratePettyCashSheetPdf()
        {
            return "";
        }
        #endregion
    }
}