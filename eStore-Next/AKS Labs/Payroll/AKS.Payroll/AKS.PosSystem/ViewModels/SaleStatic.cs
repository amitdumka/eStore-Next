/*
 * @Author: Amit Kumar
 * @Version: 1.0
 * @Date: 30/08/2022
 * @Copyrite: Aks Labs(Amit Kumar)
 * @AuthurEmail: amit.dumka@gmail.com
 */

using AKS.PosSystem.Helpers;

namespace AKS.PosSystem.ViewModels
{
    #region Helpers

    public static class SaleStatic
    {
        public static string GenerateInvoiceNumber(InvoiceType iType, int count, string scode)
        {
            string ino = $"{scode}/{DateTime.Now.Year}/{DateTime.Now.Month}/";
            switch (iType)
            {
                case InvoiceType.Sales:
                    ino += $"IN/{SaleUtils.INCode(count)}";
                    break;

                case InvoiceType.SalesReturn:
                    ino += $"SR/{SaleUtils.INCode(count)}";
                    break;

                case InvoiceType.ManualSale:
                    ino += $"MIN/{SaleUtils.INCode(count)}";
                    break;

                case InvoiceType.ManualSaleReturn:
                    ino += $"SRM/{SaleUtils.INCode(count)}";
                    break;

                default:
                    ino += $"IN/{SaleUtils.INCode(count)}";
                    break;
            }
            return ino;
        }

        public static decimal CalculateRate(string dis, string qty, string rate)
        {
            try
            {
                if (dis.Contains('%'))
                {
                    var x = decimal.Parse(qty.Trim()) * decimal.Parse(rate.Trim());
                    x -= x * decimal.Parse(dis.Replace('%', ' ').Trim()) / 100;
                    return x;
                }
                else
                {
                    var x = decimal.Parse(qty.Trim()) * decimal.Parse(rate.Trim())
                        - decimal.Parse(dis.Trim());
                    return x;
                }
            }
            catch
            {
                return 0;
            }
        }
    }

    #endregion SaleModel

}