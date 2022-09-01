using AKS.Shared.Commons.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Shared.Commons.Models.Banking
{
    public enum DebitCredit { In, Out }
    public class BankTranscation : BaseST
    {
        [Key]
        public int BankTranscationId { get; set; }
        public string AccountNumber { get; set; }
        public DateTime OnDate { get; set; }
        public string Naration { get; set; }
        public string RefNumber { get; set; }
        public decimal Amount { get; set; }//In is postive and Out is Negative.
        public decimal Balance { get; set; }
        public DebitCredit DebitCredit { get; set; }
        public DateTime? BankDate { get; set; }
        public bool Verified { get; set; }

        [ForeignKey("AccountNumber")]
        public virtual BankAccount BankAccount { get; set; }
    }
    [Table("V1_Banks")]
    public class Bank
    {
        public string BankId { get; set; }
        public string Name { get; set; }
    }

    public class BankAccountBase
    {
        [Key]
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }

        public string BankId { get; set; }
        public virtual Bank Bank { get; set; }

        public string IFSCCode { get; set; }
        public string BranchName { get; set; }
        public AccountType AccountType { get; set; }

        public bool IsActive { get; set; }
    }

    [Table("V1_BankAccounts")]
    public class BankAccount : BankAccountBase
    {
        public bool DefaultBank { get; set; }
        public bool SharedAccount { get; set; }
        public decimal OpenningBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime OpenningDate { get; set; }
        public DateTime? ClosingDate { get; set; }

        public string StoreId { get; set; }
        public bool MarkedDeleted { get; set; }


    }
    [Table("V1_VendorBankAccounts")]
    public class VendorBankAccount : BankAccountBase
    {
        public string VendorId { get; set; }
        public decimal OpenningBalance { get; set; }

        public DateTime OpenningDate { get; set; }
        public DateTime? ClosingDate { get; set; }

        public string StoreId { get; set; }
        public bool MarkedDeleted { get; set; }
    }
    [Table("V1_BankAccountList")]
    public class BankAccountList : BankAccountBase
    {
        public bool SharedAccount { set; get; }
        public string StoreId { get; set; }
        public bool MarkedDeleted { get; set; }
    }
    [Table("V1_ChequeeBooks")]
    public class ChequeBook : BaseST
    {
        public string ChequeBookId { get; set; }
        public string AccountId { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public DateTime IssuedDate { get; set; }
        public long StartingNumber { get; set; }
        public long EndingNumber { get; set; }
        public int Count { get; set; }
        public int NoOfChequeIssued { get; set; }
        public int NoOfPDC { get; set; }
        public int NoOfClearedCheques { get; set; }
    }
    [Table("V1_ChequeeIssued")]
    public class ChequeIssued : BaseST
    {
        public string ChequeIssuedId { get; set; }
        public DateTime OnDate { get; set; }
        public string InFavourOf { get; set; }
        public decimal Amount { get; set; }
        public string AccountId { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public string ChequeBookId { get; set; }
        public virtual ChequeBook ChequeBook { get; set; }
        public long ChequeNumber { get; set; }
    }
    [Table("V1_ChequeeLogs")]
    public class ChequeLog : BaseST
    {
        public string ChequeLogId { get; set; }
        public DateTime OnDate { get; set; }
        public string InFavourOf { get; set; }
        public decimal Amount { get; set; }
        public string AccountNumber { get; set; }
        public string ChequeIssuer { get; set; }
        public string BankId { get; set; }
        public long ChequeNumber { get; set; }
        public string Status { get; set; }
    }
}