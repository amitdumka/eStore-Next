//using AKS.Shared.Commons.Models.Accounts;
using AKS.AccountingSystem.DataModels;
using AKS.AccountingSystem.DTO;
using AKS.AccountingSystem.Helpers;
using AKS.Payroll.Database;
using AKS.Printers.Thermals;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Ops;
using AKS.Shared.Commons.ViewModels.Accounts;
using AKS.Shared.Templets.ViewModels;

namespace AKS.AccountingSystem.ViewModels
{
    public class VoucherCashViewModel : ViewModel<Voucher, CashVoucher, VoucherCashDataModel>
    {
        #region Constructors

        public VoucherCashViewModel(VoucherType type)
        {
            voucherType = type;
        }

        #endregion Constructors

        #region VoucherEntry

        public string deleteVoucherNumber;
        public bool isNew = false;
        public string voucherNumber;
        private CashVoucher cashVoucher;
        private Voucher voucher;
        public CashVoucher SavedCashVoucher { get; set; }
        public Voucher SavedVoucher { get; set; }
        #endregion VoucherEntry

        #region Common

        public VoucherType voucherType;
        private CommonDataModel CommonDataModel;// { get; set; }

        #endregion Common

        #region VoucherView

        private ObservableListSource<CashVoucherVM> cashVoucherVMs;

        private SortedDictionary<string, bool> DataDictionary = new SortedDictionary<string, bool>();

        private List<int> DataList;

        // private AzurePayrollDbContext azureDb;
        // private LocalPayrollDbContext localDb;
        private int SelectedYear;

        private ObservableListSource<VoucherVM> voucherVMs;

        #endregion VoucherView

        public override bool InitViewModel()
        {
            CommonDataModel = new CommonDataModel();
            DataModel = new VoucherCashDataModel();
            DMMapper.InitializeAutomapper();
            voucherVMs = new ObservableListSource<VoucherVM>();
            cashVoucherVMs = new ObservableListSource<CashVoucherVM>();
            DataList = new List<int>();
            return true;
        }

        #region Methods

        public List<CashVoucherVM> GetCashVouchers(VoucherType vType, int year = 0)
        {
            if (year <= 0)
                return cashVoucherVMs.Where(c => c.VoucherType == vType).OrderByDescending(c => c.OnDate).ToList();
            else
                return cashVoucherVMs.Where(c => c.VoucherType == vType && c.OnDate.Year == year).OrderByDescending(c => c.OnDate).ToList();
        }

        public List<VoucherVM> GetVouchers(VoucherType vType, int year = 0)
        {
            if (year <= 0)
                return voucherVMs.Where(c => c.VoucherType == vType).OrderByDescending(c => c.OnDate).ToList();
            else
                return voucherVMs.Where(c => c.VoucherType == vType && c.OnDate.Year == year).OrderByDescending(c => c.OnDate).ToList();
        }

        public void LoadGridData(VoucherType type, int year)
        {
            if (!DataDictionary.ContainsKey(type.ToString() + year))
            {
                if (type == VoucherType.CashPayment || type == VoucherType.CashReceipt)

                    UpdateCashVoucherList(DataModel.GetCashVouchers(type, year, CurrentSession.StoreCode));
                else

                    UpdateVoucherList(DataModel.GetVouchers(type, year, CurrentSession.StoreCode));

                DataDictionary.Add(type.ToString() + year, true);
            }
        }

        public void RemoveCashVoucher(string voucherNumber)
        {
            cashVoucherVMs.Remove(cashVoucherVMs.Where(c => c.VoucherNumber == voucherNumber).First());
        }

        public void RemoveVoucher(string voucherNumber)
        {
            voucherVMs.Remove(voucherVMs.Where(c => c.VoucherNumber == voucherNumber).First());
        }

        public void UpdateCashVoucherList(CashVoucherVM oldData, CashVoucher newData, bool isNew = true)
        {
            if (!isNew)
                cashVoucherVMs.Remove(oldData);
            cashVoucherVMs.Add(DMMapper.Mapper.Map<CashVoucherVM>(newData));
        }

        public void UpdateVoucherList(VoucherVM oldData, Voucher newData, bool isNew = true)
        {
            if (!isNew)
                voucherVMs.Remove(oldData);
            voucherVMs.Add(DMMapper.Mapper.Map<VoucherVM>(newData));
        }

        #endregion Methods

        #region OpsFunctions

        public List<int> GetYearList()
        {
            return DataModel.GetYearList(CurrentSession.StoreCode);
        }

        public string PrintCashVoucher(CashVoucher voucher)
        {
            if (voucher.VoucherType == VoucherType.CashReceipt || voucher.VoucherType == VoucherType.CashPayment)
            {
                if (voucher.PartyId != null)
                {
                    voucher.Partys = DataModel.GetParty(voucher.PartyId);
                }
                voucher.TranscationMode = DataModel.GetTranscationMode(voucher.TranscationId);
                voucher.Employee = DataModel.GetEmployee(voucher.EmployeeId);

                VoucherPrint print = new VoucherPrint
                {
                    Amount = voucher.Amount,
                    IsVoucherSet = true,
                    NoOfCopy = 1,
                    Page2Inch = false,
                    OnDate = voucher.OnDate,
                    Particulars = voucher.Particulars,
                    PartyName = voucher.PartyName,
                    LedgerName = voucher.Partys != null ? voucher.Partys.PartyName : "",
                    Reprint = false,
                    PaymentMode = PaymentMode.Cash,
                    Voucher = voucher.VoucherType,
                    VoucherNo = voucher.VoucherNumber,
                    TranscationMode = null,
                    PaymentDetails = voucher.TranscationMode.TranscationName,
                    StoreCode = voucher.StoreId,
                    Remarks = voucher.Remarks + " SlipNo:" + voucher.SlipNumber,
                    IssuedBy = voucher.Employee.StaffName,
                    PrintType = PrintType.PaymentVoucher
                };

                return print.PrintPdf();
                //ShowPrintDialog(fileName);
                //return fileName;
            }

            return null;
        }

        public string PrintVoucher(Voucher voucher)
        {
            if (voucher.VoucherType == VoucherType.Receipt || voucher.VoucherType == VoucherType.Expense || voucher.VoucherType == VoucherType.Payment)
            {
                if (voucher.PartyId != null)
                {
                    voucher.Partys = DataModel.GetParty(voucher.PartyId);
                }
                voucher.Employee = DataModel.GetEmployee(voucher.EmployeeId);

                VoucherPrint print = new VoucherPrint
                {
                    Amount = voucher.Amount,
                    IsVoucherSet = true,
                    NoOfCopy = 1,
                    Page2Inch = false,
                    OnDate = voucher.OnDate,
                    Particulars = voucher.Particulars,
                    PartyName = voucher.PartyName,
                    LedgerName = voucher.Partys != null ? voucher.Partys.PartyName : "",
                    Reprint = false,
                    PaymentMode = voucher.PaymentMode,
                    Voucher = VoucherType.Payment,
                    VoucherNo = voucher.VoucherNumber,
                    TranscationMode = null,
                    PaymentDetails = voucher.PaymentDetails,
                    StoreCode = voucher.StoreId,
                    Remarks = voucher.Remarks + " SlipNo:" + voucher.SlipNumber,
                    IssuedBy = voucher.Employee.StaffName,
                    PrintType = PrintType.PaymentVoucher
                };
                return print.PrintPdf();
                //ShowPrintDialog(fileName);
                //return fileName;
            }

            return null;
        }

        public void UpdateCashVoucherList(List<CashVoucher> vouchers)
        {
            foreach (var vou in vouchers)
            {
                cashVoucherVMs.Add(DMMapper.Mapper.Map<CashVoucherVM>(vou));
            }
        }

        public void UpdateVoucherList(List<Voucher> vouchers)
        {
            foreach (var vou in vouchers)
            {
                voucherVMs.Add(DMMapper.Mapper.Map<VoucherVM>(vou));
            }
        }
        #endregion OpsFunctions

        #region VoucherEntryMethods

        public override bool Delete(Voucher voucher)
        {
            if (DataModel.Delete(voucher))
            {
                deleteVoucherNumber = voucherNumber;
                PrimaryEntites.Remove(voucher);
                return true;
            }
            return false;
        }

        public override bool Delete(CashVoucher voucher)
        {
            if (DataModel.Delete(voucher))
            {
                deleteVoucherNumber = voucherNumber;
                SecondayEntites.Remove(voucher);
                return true;
            }
            return false;
        }

        public int GetCount(DateTime onDate, VoucherType type)
        {
            if (type == VoucherType.CashReceipt || type == VoucherType.CashPayment)
            {
                return DataModel.GetCashVoucherCount(CurrentSession.StoreCode, onDate, type);
            }
            else
            {
                return DataModel.GetVoucherCount(CurrentSession.StoreCode, onDate, type);
            }
        }

        public bool Save(Voucher voucher, bool isNew)
        {
            SavedVoucher = DataModel.Save(voucher, isNew);
            if (SavedVoucher != null) return true;
            return false;
        }

        public bool Save(CashVoucher voucher, bool isNew)
        {
            SavedCashVoucher = DataModel.Save(voucher, isNew);
            if (SavedCashVoucher != null) return true;
            return false;
        }

        public bool SaveCashVoucher(bool isDataSet)
        {
            SecondaryEntity.VoucherNumber = isNew ? VoucherStatic.GenerateVoucherNumber(SecondaryEntity.VoucherType, SecondaryEntity.OnDate, SecondaryEntity.StoreId,
                DataModel.GetCashVoucherCount(SecondaryEntity.StoreId, SecondaryEntity.OnDate, SecondaryEntity.VoucherType)) : this.voucherNumber;

            SavedCashVoucher = DataModel.Save(SecondaryEntity, isNew);
            if (SavedCashVoucher != null) return true;
            return false;
        }

        public bool SaveVoucher(bool isDataSet)
        {
            //TODO: Move VoucherNumber Creation ViewModel/DataModel Side
            PrimaryEntity.VoucherNumber = isNew ? VoucherStatic.GenerateVoucherNumber(PrimaryEntity.VoucherType, PrimaryEntity.OnDate, PrimaryEntity.StoreId,
                DataModel.GetCashVoucherCount(PrimaryEntity.StoreId, PrimaryEntity.OnDate, PrimaryEntity.VoucherType)) : this.voucherNumber;

            SavedVoucher = DataModel.Save(PrimaryEntity, isNew);
            if (SavedVoucher != null) return true;
            return false;
        }
        public void Update(Voucher voucher)
        { PrimaryEntity = voucher; }

        public void Update(CashVoucher voucher)
        { SecondaryEntity = voucher; }

        #endregion VoucherEntryMethods

        #region CommonDataModelsFunctions

        public override bool DeleteRange(List<Voucher> entities)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRange(List<CashVoucher> entities)
        {
            throw new NotImplementedException();
        }

        public List<DynVM> GetBankAccountList()
        { return CommonDataModel.GetBankAccount(DataModel.GetDatabaseInstance()); }

        public List<DynVM> GetEmployeeList()
        {
            return CommonDataModel.GetEmployeeList(DataModel.GetDatabaseInstance());
        }

        public List<DynVM> GetPartyList()
        { return CommonDataModel.GetParty(DataModel.GetDatabaseInstance()); }

        public List<DynVM> GetStoreList()
        {
            return CommonDataModel.GetStoreList(DataModel.GetDatabaseInstance());
        }
        public List<DynVM> GetTranscationList()
        { return CommonDataModel.GetTranscation(DataModel.GetDatabaseInstance()); }
        public override bool Save(Voucher entity)
        {
            throw new NotImplementedException();
        }

        public override bool Save(CashVoucher entity)
        {
            throw new NotImplementedException();
        }

        #endregion CommonDataModelsFunctions
    }
}