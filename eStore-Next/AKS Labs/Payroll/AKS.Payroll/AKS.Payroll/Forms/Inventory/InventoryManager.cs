using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Inventory;
using System.Data;

namespace AKS.Payroll.Forms.Inventory
{
    public class InventoryManager
    {
        public AzurePayrollDbContext azureDb;
        public LocalPayrollDbContext localDb;
        public string StoreCode;
        private static List<string> enumList;
        private List<ProductType> productTypeList;
        private List<ProductSubCategory> productSubCategories;
        private List<string> sizeList;

        public InventoryManager(AzurePayrollDbContext db, LocalPayrollDbContext ldb, string sc)
        {
            azureDb = db; localDb = ldb; StoreCode = sc;
        }

        /// <summary>
        /// set Unit for item
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Unit SetUnit(string name)
        {
            if (name.StartsWith("Apparel")) { return Unit.Pcs; }
            else if (name.StartsWith("Promo") || name.StartsWith("Suit Cover")) { return Unit.Nos; }
            else if (name.StartsWith("Shirting") || name.StartsWith("Suiting")) { return Unit.Meters; }
            return Unit.NoUnit;
        }

        /// <summary>
        /// Set Catgory of Product
        /// </summary>
        /// <param name="pi"></param>
        private ProductItem SetCatgoryAndSize(ProductItem pi)
        {
            var cats = pi.Name.Split("/");

            int x = 0;
            if (cats[0] == "Suiting" || cats[0] == "Shirting") x = 0;
            else if (cats[0] == "Promo") x = (int)ProductCategory.PromoItems;
            else if (cats[0] == "Suit Cover") x = (int)ProductCategory.SuitCovers;
            else { x = enumList.IndexOf(cats[0]); }
            pi.ProductCategory = (ProductCategory)x;
            string ptid = productTypeList.Where(c => c.ProductTypeName == cats[1]).First().ProductTypeId;
            if (ptid != null)
                pi.ProductTypeId = ptid;
            else
            {
                pi.ProductTypeId = "PT00013";
            }
            pi.SubCategory = cats[2];
            if (pi.Unit == Unit.Meters || pi.Unit == Unit.Nos || pi.Unit == Unit.NoUnit) pi.Size = Size.NS;
            else
            {
                pi.Size = SetSize(pi.StyleCode, cats[2]);
            }
            return pi;
        }
        /// <summary>
        /// Set Size based on style code and Category
        /// </summary>
        /// <param name="style"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public Size SetSize(string style, string category)
        {
            Size size = Size.NOTVALID;
            var name = style.Substring(style.Length - 4, 4);

            // Jeans and Trousers

            if (category.Contains("Boxer") || category.Contains("Socks") || category.Contains("H-Shorts") || category.Contains("Shirt") || category.Contains("Vests") || category.Contains("Briefs") || category.Contains("Jackets") || category.Contains("Sweat Shirt") || category.Contains("Sweater") || category.Contains("T shirts"))
            {


                if (name.EndsWith(Size.XXXL.ToString())) size = Size.XXXL;
                else if (name.EndsWith(Size.XXL.ToString())) size = Size.XXL;
                else if (name.EndsWith(Size.XL.ToString())) size = Size.XL;
                else if (name.EndsWith(Size.L.ToString())) size = Size.L;
                else if (name.EndsWith(Size.M.ToString())) size = Size.M;
                else if (name.EndsWith(Size.S.ToString())) size = Size.S;
                else if (name.EndsWith("FS")) size = Size.FreeSize;
                else
                {
                    var nn = name.Substring(name.Length - 2).Trim();
                    int nx = 0;
                    if (Int32.TryParse(nn, out nx))
                    {
                        switch (nx)
                        {
                            case 39: size = Size.S; break;
                            case 40: size = Size.M; break;
                            case 42: size = Size.L; break;
                            case 44: size = Size.XL; break;
                            case 46: size = Size.XXL; break;
                            case 48: size = Size.XXXL; break;
                            default:
                                size = Size.NOTVALID;
                                break;
                        }
                    }
                    else
                    {

                    }

                }

            }
            else if (category.Contains("Shorts") || category.Contains("Jeans") || category.Contains("Trouser") || category.Contains("Trousers"))
            {
                int x = sizeList.IndexOf($"T{name[2]}{name[3]}");
                size = (Size)x;
            }
            else if (category.Contains("Bundis") || category.Contains("Blazer") || category.Contains("Blazers") || category.Contains("Suits"))
            {

                int x = sizeList.IndexOf($"B{name[2]}{name[3]}");
                if (x == -1)
                {
                    x = sizeList.IndexOf($"B{name[1]}{name[2]}{name[3]}");
                }
                if (x == -1)
                {
                    size = Size.NOTVALID;
                }
                else
                    size = (Size)x;
            }
            else if (category.Contains("Accessories"))
            {
                size = Size.NS;
            }
            else
            {
                size = Size.NOTVALID;
            }
            return size;
        }

        /// <summary>
        /// Obsolute
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="sub"></param>
        private void UpdateSubCate(string cat, string sub)
        {
            var subcat = azureDb.ProductSubCategories.Find(sub);
            int x = enumList.IndexOf(cat);
            if (x == -1)
            {
                x = enumList.IndexOf(cat);
                if (cat == "Suiting" || cat == "Shirting") x = 0;
                else if (cat == "Promo") x = (int)ProductCategory.PromoItems;
                else if (cat == "Suit Cover") x = (int)ProductCategory.SuitCovers;
                else
                {
                    x = enumList.IndexOf(cat);
                }
            }
            subcat.ProductCategory = (ProductCategory)x;
            azureDb.ProductSubCategories.Update(subcat);
        }

        /// <summary>
        /// Onbsolute
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="lb1"></param>
        /// <param name="lb2"></param>
        /// <param name="lb3"></param>
        public void CreateCategoryList(DataTable dataTable, ListBox lb1, ListBox lb2, ListBox lb3)
        {
            enumList = Enum.GetNames(typeof(ProductCategory)).ToList();
            var data = dataTable.AsEnumerable().GroupBy(c => c["Product Name"]).Select(c => c.Key.ToString()).ToList();
            List<string> a = new List<string>();
            List<string> b = new List<string>();
            List<string> c = new List<string>();

            foreach (var item in data)
            {
                var x = item.Split("/");
                a.Add(x[0]);
                b.Add(x[1]);
                c.Add(x[2]);
                //  UpdateSubCate(x[0], x[2]);
            }

            a = a.Distinct().ToList();
            b = b.Distinct().ToList();
            c = c.Distinct().ToList();
            c.Sort();
            b.Sort();
            // Apprel, shirting, suiting
            lb1.DataSource = a;
            // Color or stle name
            lb2.DataSource = b;
            // Type of Product.
            lb3.DataSource = c;
            //Store In table.
            //int ctr = 0;
            //foreach (var item in b)
            //{
            //    ctr++;
            //    string p = "";
            //    if (ctr >= 10) { p = "PT000" + ctr; }
            //    else p = "PT0000" + ctr;
            //    ProductType pt = new ProductType {
            //        ProductTypeName = item, ProductTypeId = p
            //    };
            //    azureDb.ProductTypes.Add(pt);
            //}
            //foreach (var ic in c)
            //{
            //    if (!azureDb.ProductSubCategories.Any(c => c.SubCategory == ic))
            //    {
            //        azureDb.ProductSubCategories.Add(new ProductSubCategory { SubCategory=ic, ProductCategory =ProductCategory.Others });
            //    }
            //}
            // azureDb.SaveChanges();
        }
        
        
        public void TryCatnSize(DataTable d, DataGridView dgv, ListBox lb1, ListBox lb2 )
        {
            //enumList = Enum.GetNames(typeof(ProductCategory)).ToList();
            //sizeList = Enum.GetNames(typeof(Size)).ToList();
            //productSubCategories = azureDb.ProductSubCategories.ToList();
            //productTypeList = azureDb.ProductTypes.ToList();
            //var data = d.AsEnumerable().Select(c => new { Name = c["Product Name"].ToString(), Style = c["Style Code"].ToString() })
            //    .ToList();
            //List<ProductItem> pList = new List<ProductItem>();
            //foreach (var item in data)
            //{
            //    ProductItem pi = new ProductItem
            //    {
            //        StyleCode = item.Style,
            //        Name = item.Name,
            //        Unit = SetUnit(item.Name)
            //    };
            //    pi = SetCatgoryAndSize(pi);
            //    pList.Add(pi);
            //}
            //dgv.DataSource = pList;//.Where(c=>c.Size==Size.NOTVALID);
            try
            {
                int ex= 0,mis=0;
                pItemList = azureDb.ProductItems.ToList();
                List<string> mBarcode = new List<string>();
                DataTable dataTable = d.Clone();
                for (int i = 0; i < d.Rows.Count; i++)
                {
                    if (ProuctItemExist(d.Rows[i]["Barcode"].ToString()))
                    {
                        d.Rows[i]["ExmillCost"] = "Exist";
                        ex++;
                    }
                    else
                    {
                        d.Rows[i]["ExmillCost"] = "Missing";
                        mis++;
                        mBarcode.Add(d.Rows[i]["Barcode"].ToString());
                        dataTable.ImportRow(d.Rows[i]);
                    }
                }

                mBarcode= mBarcode.Distinct().ToList();
                mBarcode.Sort();
                dgv.DataSource = dataTable;
                lb1.Items.Add($"Total:{d.Rows.Count}");
                lb1.Items.Add($"Missing:{mis}");
                lb1.Items.Add($"Exsit:{ex}");
                lb2.DataSource = mBarcode;
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

        }
        private List<ProductItem> pItemList;
        private void ProcessProductItem(DataTable DataTable)
        {
            enumList = Enum.GetNames(typeof(ProductCategory)).ToList();
            productSubCategories = azureDb.ProductSubCategories.ToList();
            productTypeList = azureDb.ProductTypes.ToList();
            pItemList = azureDb.ProductItems.ToList();

            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                if (!ProuctItemExist(DataTable.Rows[i]["Barcode"].ToString()))
                {
                    ProductItem item = new ProductItem
                    {
                        Barcode = DataTable.Rows[i]["Barcode"].ToString(),
                        StyleCode = DataTable.Rows[i]["StyleCode"].ToString(),
                        Description = DataTable.Rows[i]["Item Desc"].ToString(),
                        MRP = decimal.Parse(DataTable.Rows[i]["MRP"].ToString().Trim()),
                        HSNCode = "",
                        Name = DataTable.Rows[i]["Product Name"].ToString(),
                        TaxType = TaxType.GST,
                        Unit = SetUnit(DataTable.Rows[i]["Product Name"].ToString()),
                    };
                    item = SetCatgoryAndSize(item);

                    var names = DataTable.Rows[i]["Product Name"].ToString().Split("/");
                }
                else
                {
                    DataTable.Rows[i]["ExmilCost"] = "Exist";
                }
            }

        }

        /// <summary>
        /// No Use
        /// </summary>
        private void ProcessStockUpdate()
        {
            var stock = azureDb.PurchaseItems
                .Select(c => new Stock
                {
                    Barcode = c.Barcode,
                    CostPrice = c.CostPrice,
                    EntryStatus = EntryStatus.Added,
                    IsReadOnly = true,
                    MarkedDeleted = false,
                    StoreId = "ARD",
                    HoldQty = 0,
                    SoldQty = 0,
                    Unit = c.Unit,
                    UserId = "AUTO",
                    PurhcaseQty = c.Qty
                }).ToList();

            List<Stock> Stocks = new List<Stock>();
            //search for dplicate barcode

            //var dup = stock.GroupBy(c => c.Barcode).Select(c=> new {c.Key , ctr=c.Key.Count()}).Where(c => c.ctr > 1).ToList();
            foreach (var item in stock)
            {
                if (Stocks.Any(c => c.Barcode == item.Barcode))
                {
                    var s = Stocks.Find(c => c.Barcode == item.Barcode);
                    Stocks.Remove(s);
                    s.PurhcaseQty += item.PurhcaseQty;
                    Stocks.Add(s);
                }
                else
                {
                    Stocks.Add(item);
                }
            }
            azureDb.Stocks.AddRange(Stocks);
            var c = azureDb.SaveChanges();
            if (c > 0)
            {
                Console.WriteLine(c);
            }
        }

        public bool ProuctItemExist(string barcode)
        {
            return pItemList.Any(c => c.Barcode == barcode);
            //return azureDb.ProductItems.Any(c => c.Barcode == barcode);
        }
    }

    public class Utils
    {
        public static int ReadInt(TextBox t)
        {
            return Int32.Parse(t.Text.Trim());
        }
    }
}