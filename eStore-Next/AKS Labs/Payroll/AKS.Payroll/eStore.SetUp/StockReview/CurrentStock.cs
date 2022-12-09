using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.SetUp.StockReview
{
    public class PhyStockHistory
    {
        public string StoreId { get; set; }
        public int PhyStockHistoryId { get; set; }
        public DateTime OnDate { get; set; }
        public decimal Qty { get; set; }
        public string Remark { get; set; }
    }
    public class StockAudit
    {


    }
}
