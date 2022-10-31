using AKS.Shared.Commons.Models.Inventory;

namespace eStore.SetUp.Import
{
    public class ImportHelpers
    {
        public static SortedDictionary<string, string> HSNCodes = new SortedDictionary<string, string>();
        public static SortedDictionary<string, ProductCategory> ProductCategories = new SortedDictionary<string, ProductCategory>();
        public static List<ProductSubCategory> ProductSubCategories = new List<ProductSubCategory>();
        public static List<ProductType> ProductTypes = new List<ProductType>();

        public static List<string> SizeList;

        public static void AddHSN(string key, string value)
        { HSNCodes.Add(key, value); }

        public static void AddProductCategory(string key, ProductCategory cat)
        { ProductCategories.Add(key, cat); }

        public static void AddProductSubCategory(ProductSubCategory cat)
        { ProductSubCategories.Add(cat); }

        public static void AddProductTypes(ProductType pTy)
        { ProductTypes.Add(pTy); }

        public static PayMode GetPaymode(string mode)
        {
            switch (mode)
            {
                case "CAS": return PayMode.Cash;
                case "CRD": return PayMode.Card;
                case "Mix": return PayMode.MixPayments;
                case "SR": return PayMode.SaleReturn;
                default:
                    return PayMode.Cash;
            }
        }

        public static string SetBrandCode(string style, string cat, string type)
        {
            string bcode = "";
            if (cat == "Apparel")
            {
                if (style.StartsWith("FM"))
                {
                    bcode = "FM";
                }
                else if (style.StartsWith("ARI")) bcode = "ADR";
                else if (style.StartsWith("HA")) bcode = "HAN";
                else if (style.StartsWith("AA")) bcode = "ARN";
                else if (style.StartsWith("AF")) bcode = "ARR";
                else if (style.StartsWith("US")) bcode = "USP";
                else if (style.StartsWith("AB")) bcode = "ARR";
                else if (style.StartsWith("AK")) bcode = "ARR";
                else if (style.StartsWith("AN")) bcode = "ARR";
                else if (style.StartsWith("ARE")) bcode = "ARR";
                else if (style.StartsWith("ARG")) bcode = "ARR";
                else if (style.StartsWith("AS")) bcode = "ARS";
                else if (style.StartsWith("AT")) bcode = "ARR";
                else if (style.StartsWith("F2")) bcode = "FM";
                else if (style.StartsWith("UD")) bcode = "UD";
            }
            else if (cat == "Shirting" || cat == "Suiting")
            {
                bcode = "ARD";
            }
            else

            if (cat == "Promo")
            {
                if (type == "Free GV") { bcode = "AGV"; }
                else bcode = "ARP";
            }
            else if (cat == "Suit Cover")
            {
                bcode = "ARA";
            }
            return bcode;
        }

        public static void SetCategoryList()
        {
            if (ProductTypes == null)
                ProductTypes = ImportData.JsonToObject<ProductType>(ImportBasic.GetSetting("ProductType"));
            if (ProductSubCategories == null)
                ProductSubCategories = ImportData.JsonToObject<ProductSubCategory>(ImportBasic.GetSetting("SubCategory"));
            if (ProductCategories.Count <= 0)
            {
                var Cat = ImportData.JsonToObject<PCat>(ImportBasic.GetSetting("ProductCategory"));
                foreach (var item in Cat)
                    ProductCategories.Add(item.Name, ImportHelpers.SetProductCategory(item.Name));
            }
        }

        public static ProductCategory SetProductCategory(string cat)
        {
            switch (cat)
            {
                case "Shirting":
                case "Suiting": return ProductCategory.Fabric;
                case "Apparel": return ProductCategory.Apparel;
                case "Promo": return ProductCategory.PromoItems;
                case "Suit Cover": return ProductCategory.SuitCovers;
                case "Accessories": return ProductCategory.Accessiories;
                case "Tailoring": return ProductCategory.Tailoring;

                default:
                    return ProductCategory.Others;
            }
        }

        public static Size SetSize(string style, string category)
        {
            Size size = Size.NOTVALID;
            var name = style.Substring(style.Length - 4, 4);

            // Jeans and Trousers

            if (category.Contains("Boxer") || category.Contains("Socks") || category.Contains("H-Shorts") || category.Contains("Shirt") || category.Contains("Vests") || category.Contains("Briefs") || category.Contains("Jackets") || category.Contains("Sweat Shirt") || category.Contains("Sweater") || category.Contains("T shirts"))
            {
                if (name.EndsWith(Size.M.ToString())) size = Size.M;
                else if (name.EndsWith(Size.S.ToString())) size = Size.S;
                else if (name.EndsWith(Size.XXXL.ToString())) size = Size.XXXL;
                else if (name.EndsWith(Size.XXL.ToString())) size = Size.XXL;
                else if (name.EndsWith(Size.XL.ToString())) size = Size.XL;
                else if (name.EndsWith(Size.L.ToString())) size = Size.L;
                else if (name.EndsWith("FS")) size = Size.FreeSize;
                else
                {
                    var nn = name.Substring(name.Length - 2).Trim();
                    int nx = 0;
                    if (Int32.TryParse(nn, out nx))
                    {
                        size = nx switch
                        {
                            39 => Size.S,
                            40 => Size.M,
                            42 => Size.L,
                            44 => Size.XL,
                            46 => Size.XXL,
                            48 => Size.XXXL,
                            _ => Size.NOTVALID,
                        };
                    }
                    else
                    {
                    }
                }
            }
            else if (category.Contains("Shorts") || category.Contains("Jeans") || category.Contains("Trouser") || category.Contains("Trousers"))
            {
                int x = ImportHelpers.SizeList.IndexOf($"T{name[2]}{name[3]}");
                size = (Size)x;
            }
            else if (category.Contains("Bundis") || category.Contains("Blazer") || category.Contains("Blazers") || category.Contains("Suits"))
            {
                int x = ImportHelpers.SizeList.IndexOf($"B{name[2]}{name[3]}");
                if (x == -1)
                {
                    x = ImportHelpers.SizeList.IndexOf($"B{name[1]}{name[2]}{name[3]}");
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

        public static Unit SetUnit(string pname)
        {
            if (pname.StartsWith("Suiting") || pname.StartsWith("Shirting")) return Unit.Meters;
            else if (pname.StartsWith("Apparel")) return Unit.Pcs;
            else if (pname.StartsWith("Promo") || pname.StartsWith("Suit Cover")) return Unit.Nos;
            else return Unit.Nos;
        }

        public static decimal ToDecimal(string num)
        {
            Decimal.TryParse(num, out decimal result);
            return Math.Round(result, 2);
        }

        public static decimal ToDecimal(object num)
        {
            Decimal.TryParse(num.ToString(), out decimal result);
            return Math.Round(result, 2);
        }

        public static string VendorMapping(string supplier)
        {
            string id = supplier switch
            {
                "TAS RMG Warehouse - Bangalore" => "ARD/VIN/0003",
                "TAS - Warhouse -FOFO" => "ARD/VIN/0003",
                "Bangalore WH" => "ARD/VIN/0003",
                "Arvind Brands Limited" => "ARD/VIN/0002",
                "TAS RTS -Warhouse" => "ARD/VIN/0002",
                "Arvind Limited" => "ARD/VIN/0001",
                "Khush" => "ARD/VIN/0005",
                "Safari Industries India Ltd" => "ARD/VIN/0004",
                "DTR Packed WH" => "ARD/VIN/0002",
                "DTR - TAS Warehouse" => "ARD/VIN/0002",
                "Aprajita Retails - Jamshedpur" => "ARD/VIN/0007",
                _ => "ARD/VIN/0002",
            };
            return id;
        }
    }
}
