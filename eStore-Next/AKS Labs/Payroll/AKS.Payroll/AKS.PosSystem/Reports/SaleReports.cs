/*
 * @Author: Amit Kumar
 * @Version: 1.0
 * @Date: 30/08/2022
 * @Copyrite: Aks Labs(Amit Kumar)
 * @AuthurEmail: amit.dumka@gmail.com
 */
using AKS.PosSystem.DataModels;
using AKS.PosSystem.Models.VM;

namespace AKS.PosSystem.Reports
{
    public class SaleReports
    {
        private static SaleDataModel DataModel;
        public SaleReports(SaleDataModel dm) { DataModel = dm; }

        public SortedDictionary<int, List<List<SaleReportVM>>> SaleReport(string storeCode, InvoiceType iType)
        {
            if (DataModel == null) DataModel = new SaleDataModel();
            SortedDictionary<int, List<List<SaleReportVM>>> report = new SortedDictionary<int, List<List<SaleReportVM>>>();
            var yearList = DataModel.GetYearList(storeCode, iType);
            foreach (var year in yearList)
            {
                report.Add(year, SaleReport(storeCode, year, iType));
            }
            return report;
        }

        private List<List<SaleReportVM>> SaleReport(string storeCode, int year, InvoiceType iType)
        {
            List<List<SaleReportVM>> saleReports = new List<List<SaleReportVM>>();
            for (int i = 1; i <= 12; i++)
            {
                saleReports.Add(DataModel.SaleReports(storeCode, year, i, iType));
            }
            return saleReports;
        }
    }
}
