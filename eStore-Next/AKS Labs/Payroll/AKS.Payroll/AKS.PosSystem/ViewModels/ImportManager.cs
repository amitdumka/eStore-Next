//using AKS.Shared.Commons.Models.Inventory;
//using System.Data;

//namespace AKS.POSBilling.Controllers
//{
//    public class ImportManager
//    {
//        /// <summary>
//        /// Obsolute
//        /// </summary>
//        /// <param name="cat"></param>
//        /// <param name="sub"></param>
//        private void UpdateSubCate(string cat, string sub)
//        {
//            var subcat = azureDb.ProductSubCategories.Find(sub);
//            int x = enumList.IndexOf(cat);
//            if (x == -1)
//            {
//                x = enumList.IndexOf(cat);
//                if (cat == "Suiting" || cat == "Shirting") x = 0;
//                else if (cat == "Promo") x = (int)ProductCategory.PromoItems;
//                else if (cat == "Suit Cover") x = (int)ProductCategory.SuitCovers;
//                else
//                {
//                    x = enumList.IndexOf(cat);
//                }
//            }
//            subcat.ProductCategory = (ProductCategory)x;
//            azureDb.ProductSubCategories.Update(subcat);
//        }

//        /// <summary>
//        /// Onbsolute
//        /// </summary>
//        /// <param name="dataTable"></param>
//        /// <param name="lb1"></param>
//        /// <param name="lb2"></param>
//        /// <param name="lb3"></param>
//        public void CreateCategoryList(DataTable dataTable, ListBox lb1, ListBox lb2, ListBox lb3)
//        {
//            enumList = Enum.GetNames(typeof(ProductCategory)).ToList();
//            var data = dataTable.AsEnumerable().GroupBy(c => c["Product Name"]).Select(c => c.Key.ToString()).ToList();
//            List<string> a = new List<string>();
//            List<string> b = new List<string>();
//            List<string> c = new List<string>();

//            foreach (var item in data)
//            {
//                var x = item.Split("/");
//                a.Add(x[0]);
//                b.Add(x[1]);
//                c.Add(x[2]);
//                //  UpdateSubCate(x[0], x[2]);
//            }

//            a = a.Distinct().ToList();
//            b = b.Distinct().ToList();
//            c = c.Distinct().ToList();
//            c.Sort();
//            b.Sort();
//            // Apprel, shirting, suiting
//            lb1.DataSource = a;
//            // Color or stle name
//            lb2.DataSource = b;
//            // Type of Product.
//            lb3.DataSource = c;
//            //Store In table.
//            //int ctr = 0;
//            //foreach (var item in b)
//            //{
//            //    ctr++;
//            //    string p = "";
//            //    if (ctr >= 10) { p = "PT000" + ctr; }
//            //    else p = "PT0000" + ctr;
//            //    ProductType pt = new ProductType {
//            //        ProductTypeName = item, ProductTypeId = p
//            //    };
//            //    azureDb.ProductTypes.Add(pt);
//            //}
//            //foreach (var ic in c)
//            //{
//            //    if (!azureDb.ProductSubCategories.Any(c => c.SubCategory == ic))
//            //    {
//            //        azureDb.ProductSubCategories.Add(new ProductSubCategory { SubCategory=ic, ProductCategory =ProductCategory.Others });
//            //    }
//            //}
//            // azureDb.SaveChanges();
//        }

//        /// <summary>
//        /// obsolute
//        /// </summary>
//        /// <param name="d"></param>
//        /// <param name="dgv"></param>
//        /// <param name="lb1"></param>
//        /// <param name="lb2"></param>
//        public void TryCatnSize(DataTable d, DataGridView dgv, ListBox lb1, ListBox lb2)
//        {
//            //enumList = Enum.GetNames(typeof(ProductCategory)).ToList();
//            //sizeList = Enum.GetNames(typeof(Size)).ToList();
//            //productSubCategories = azureDb.ProductSubCategories.ToList();
//            //productTypeList = azureDb.ProductTypes.ToList();
//            //var data = d.AsEnumerable().Select(c => new { Name = c["Product Name"].ToString(), Style = c["Style Code"].ToString() })
//            //    .ToList();
//            //List<ProductItem> pList = new List<ProductItem>();
//            //foreach (var item in data)
//            //{
//            //    ProductItem pi = new ProductItem
//            //    {
//            //        StyleCode = item.Style,
//            //        Name = item.Name,
//            //        Unit = SetUnit(item.Name)
//            //    };
//            //    pi = SetCatgoryAndSize(pi);
//            //    pList.Add(pi);
//            //}
//            //dgv.DataSource = pList;//.Where(c=>c.Size==Size.NOTVALID);
//            //try
//            //{
//            //    int ex= 0,mis=0;
//            //    pItemList = azureDb.ProductItems.ToList();
//            //    List<string> mBarcode = new List<string>();
//            //    DataTable dataTable = d.Clone();
//            //    for (int i = 0; i < d.Rows.Count; i++)
//            //    {
//            //        if (ProuctItemExist(d.Rows[i]["Barcode"].ToString()))
//            //        {
//            //            d.Rows[i]["ExmillCost"] = "Exist";
//            //            ex++;
//            //        }
//            //        else
//            //        {
//            //            d.Rows[i]["ExmillCost"] = "Missing";
//            //            mis++;
//            //            mBarcode.Add(d.Rows[i]["Barcode"].ToString());
//            //            dataTable.ImportRow(d.Rows[i]);
//            //        }
//            //    }

//            //    mBarcode= mBarcode.Distinct().ToList();
//            //    mBarcode.Sort();
//            //    dgv.DataSource = dataTable;
//            //    lb1.Items.Add($"Total:{d.Rows.Count}");
//            //    lb1.Items.Add($"Missing:{mis}");
//            //    lb1.Items.Add($"Exsit:{ex}");
//            //    lb2.DataSource = mBarcode;
//            //}
//            //catch (Exception e)
//            //{
//            //    MessageBox.Show(e.Message);
//            //}
//        }

//        /// <summary>
//        /// Obsulute
//        /// </summary>
//        private void ProcessStockUpdate()
//        {
//            var stock = azureDb.PurchaseItems
//                .Select(c => new Stock
//                {
//                    Barcode = c.Barcode,
//                    CostPrice = c.CostPrice,
//                    EntryStatus = EntryStatus.Added,
//                    IsReadOnly = true,
//                    MarkedDeleted = false,
//                    StoreId = "ARD",
//                    HoldQty = 0,
//                    SoldQty = 0,
//                    Unit = c.Unit,
//                    UserId = "AUTO",
//                    PurhcaseQty = c.Qty
//                }).ToList();

//            List<Stock> Stocks = new List<Stock>();
//            //search for dplicate barcode

//            //var dup = stock.GroupBy(c => c.Barcode).Select(c=> new {c.Key , ctr=c.Key.Count()}).Where(c => c.ctr > 1).ToList();
//            foreach (var item in stock)
//            {
//                if (Stocks.Any(c => c.Barcode == item.Barcode))
//                {
//                    var s = Stocks.Find(c => c.Barcode == item.Barcode);
//                    Stocks.Remove(s);
//                    s.PurhcaseQty += item.PurhcaseQty;
//                    Stocks.Add(s);
//                }
//                else
//                {
//                    Stocks.Add(item);
//                }
//            }
//            azureDb.Stocks.AddRange(Stocks);
//            var c = azureDb.SaveChanges();
//            if (c > 0)
//            {
//                Console.WriteLine(c);
//            }
//        }

//    }
//}