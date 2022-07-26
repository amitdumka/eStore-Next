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

        // Still development
        public void CreateCategoryList(DataTable dataTable)
        {
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
            }

            a = a.Distinct().ToList();
            b = b.Distinct().ToList();
            c = c.Distinct().ToList();
            c.Sort();

            // Apprel, shirting, suiting
            //listBox1.DataSource = a;
            // Color or stle name
            //listBox2.DataSource = b;
            // Type of Product. 
           // listBox3.DataSource = c;
            //Store In table.
        }
       
        public static global::Size SetSize(string style, string category)
        {
            global::Size size = global::Size.NOTVALID;
            var name = style.Substring(style.Length - 4, 4);
            var enumList = Enum.GetNames(typeof(global::Size)).ToList();
            // Jeans and Trousers
            if (category.Contains("Jeans") || category.Contains("Trousers"))
            {
                size = (global::Size)enumList.IndexOf($"T{name[2]}{name[3]}");
            }
            else if (category.Contains("Shirt") || category.Contains("TShirt"))
            {
                if (name.EndsWith(global::Size.XXXL.ToString()))
                    size = global::Size.XXXL;
                else if (name.EndsWith(global::Size.XXL.ToString())) { size = global::Size.XXL; }
                else if (name.EndsWith(global::Size.XL.ToString())) { size = global::Size.XL; }
                else if (name.EndsWith(global::Size.L.ToString())) { size = global::Size.L; }
                else if (name.EndsWith(global::Size.M.ToString())) { size = global::Size.M; }
                else if (name.EndsWith(global::Size.S.ToString())) { size = global::Size.S; }

            }
            return size;
        }
        private void ProcessProductItem( DataTable DataTable)
        {
            for (int i = 0; i < DataTable.Rows.Count; i++)

            {
                if (!ProuctItemExist(DataTable.Rows[i]["Barcode"].ToString()))
                {
                    ProductItem item = new ProductItem
                    {
                        Barcode = DataTable.Rows[i]["Barcode"].ToString(),
                        StyleCode = DataTable.Rows[i]["StyleCode"].ToString(),
                        Description = DataTable.Rows[i]["Item Desc"].ToString(),
                        MRP = decimal.Parse(DataTable.Rows[i]["Item Desc"].ToString().Trim()),
                        HSNCode = "",
                        Name = DataTable.Rows[i]["Product Name"].ToString(),
                        TaxType = TaxType.GST,
                        Unit = SetUnit(DataTable.Rows[i]["Product Name"].ToString()),
                    };
                    var names = DataTable.Rows[i]["Product Name"].ToString().Split("/");
                    if (item.Unit == Unit.Meters || item.Unit == Unit.Nos || item.Unit == Unit.NoUnit) item.Size = global::Size.NS;
                    else
                    {

                    }
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
            return azureDb.ProductItems.Any(c => c.Barcode == barcode);
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