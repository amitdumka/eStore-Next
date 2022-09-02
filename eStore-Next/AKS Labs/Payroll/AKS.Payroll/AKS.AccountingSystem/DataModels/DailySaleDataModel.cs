using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Templets.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKS.AccountingSystem.DataModels
{
    public class DailySaleDataModel : DataModel<DailySale, CustomerDue, DueRecovery>
    {
        public override DailySale Get(string id)
        {
            throw new NotImplementedException();
        }

        public override DailySale Get(int id)
        {
            throw new NotImplementedException();
        }

        public override List<DailySale> GetList()
        {
            throw new NotImplementedException();
        }

        public override DueRecovery GetSeconday(string id)
        {
            throw new NotImplementedException();
        }

        public override DueRecovery GetSeconday(int id)
        {
            throw new NotImplementedException();
        }

        public override List<DueRecovery> GetSecondayList()
        {
            throw new NotImplementedException();
        }

        public override CustomerDue GetY(string id)
        {
            throw new NotImplementedException();
        }

        public override CustomerDue GetY(int id)
        {
            throw new NotImplementedException();
        }

        public override List<CustomerDue> GetYList()
        {
            throw new NotImplementedException();
        }
    }
}
