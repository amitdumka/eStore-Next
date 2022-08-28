using System;
using System.Collections.Generic;
using System.Text;
using AKS.Shared.Commons.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Shared.Accounting.Models.Banking
{
    public enum DebitCredit { In, Out}
    public class BankTranscation:BaseST
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
}
