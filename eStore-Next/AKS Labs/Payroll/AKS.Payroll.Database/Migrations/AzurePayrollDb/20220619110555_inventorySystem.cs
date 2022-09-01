using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations.AzurePayrollDb
{
    public partial class inventorySystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductSubCategories",
                columns: table => new
                {
                    SubCategory = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubCategories", x => x.SubCategory);
                });

            migrationBuilder.CreateTable(
                name: "V1_CardPaymentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Card = table.Column<int>(type: "int", nullable: false),
                    CardType = table.Column<int>(type: "int", nullable: false),
                    CardLastDigit = table.Column<int>(type: "int", nullable: false),
                    AuthCode = table.Column<int>(type: "int", nullable: false),
                    EDCTerminalId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_CardPaymentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_V1_CardPaymentDetails_V1_EDCMachine_EDCTerminalId",
                        column: x => x.EDCTerminalId,
                        principalTable: "V1_EDCMachine",
                        principalColumn: "EDCTerminalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "V1_ProductSales",
                columns: table => new
                {
                    InvoiceCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceType = table.Column<int>(type: "int", nullable: false),
                    BilledQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FreeQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalTaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Taxed = table.Column<bool>(type: "bit", nullable: false),
                    Adjusted = table.Column<bool>(type: "bit", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    SalesmanId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tailoring = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_ProductSales", x => x.InvoiceCode);
                    table.ForeignKey(
                        name: "FK_V1_ProductSales_V1_Salesmen_SalesmanId",
                        column: x => x.SalesmanId,
                        principalTable: "V1_Salesmen",
                        principalColumn: "SalesmanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_V1_ProductSales_V1_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "V1_Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "V1_Vendors",
                columns: table => new
                {
                    VendorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorType = table.Column<int>(type: "int", nullable: false),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_Vendors", x => x.VendorId);
                    table.ForeignKey(
                        name: "FK_V1_Vendors_V1_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "V1_Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "V1_Products",
                columns: table => new
                {
                    Barcode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StyleCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxType = table.Column<int>(type: "int", nullable: false),
                    MRP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    ProductCategory = table.Column<int>(type: "int", nullable: false),
                    SubCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HSNCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    ProductSubCategorySubCategory = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_Products", x => x.Barcode);
                    table.ForeignKey(
                        name: "FK_V1_Products_ProductSubCategories_ProductSubCategorySubCategory",
                        column: x => x.ProductSubCategorySubCategory,
                        principalTable: "ProductSubCategories",
                        principalColumn: "SubCategory",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "V1_SalePaymentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayMode = table.Column<int>(type: "int", nullable: false),
                    RefId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductSaleInvoiceCode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_SalePaymentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_V1_SalePaymentDetails_V1_ProductSales_ProductSaleInvoiceCode",
                        column: x => x.ProductSaleInvoiceCode,
                        principalTable: "V1_ProductSales",
                        principalColumn: "InvoiceCode");
                });

            migrationBuilder.CreateTable(
                name: "V1_PurchaseProducts",
                columns: table => new
                {
                    InvoiceNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VendorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InvoiceType = table.Column<int>(type: "int", nullable: false),
                    TaxType = table.Column<int>(type: "int", nullable: false),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BasicAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    BillQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FreeQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_PurchaseProducts", x => x.InvoiceNo);
                    table.ForeignKey(
                        name: "FK_V1_PurchaseProducts_V1_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "V1_Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_V1_PurchaseProducts_V1_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "V1_Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "V1_SaleItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BilledQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FreeQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Adjusted = table.Column<bool>(type: "bit", nullable: false),
                    LastPcs = table.Column<bool>(type: "bit", nullable: false),
                    ProductSaleInvoiceCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductItemBarcode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_SaleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_V1_SaleItems_V1_Products_ProductItemBarcode",
                        column: x => x.ProductItemBarcode,
                        principalTable: "V1_Products",
                        principalColumn: "Barcode");
                    table.ForeignKey(
                        name: "FK_V1_SaleItems_V1_ProductSales_ProductSaleInvoiceCode",
                        column: x => x.ProductSaleInvoiceCode,
                        principalTable: "V1_ProductSales",
                        principalColumn: "InvoiceCode");
                });

            migrationBuilder.CreateTable(
                name: "V1_Stocks",
                columns: table => new
                {
                    Barcode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PurhcaseQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoldQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HoldQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductBarcode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_Stocks", x => x.Barcode);
                    table.ForeignKey(
                        name: "FK_V1_Stocks_V1_Products_ProductBarcode",
                        column: x => x.ProductBarcode,
                        principalTable: "V1_Products",
                        principalColumn: "Barcode");
                    table.ForeignKey(
                        name: "FK_V1_Stocks_V1_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "V1_Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "V1_ProductItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FreeQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseProductInvoiceNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductItemBarcode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_ProductItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_V1_ProductItems_V1_Products_ProductItemBarcode",
                        column: x => x.ProductItemBarcode,
                        principalTable: "V1_Products",
                        principalColumn: "Barcode");
                    table.ForeignKey(
                        name: "FK_V1_ProductItems_V1_PurchaseProducts_PurchaseProductInvoiceNo",
                        column: x => x.PurchaseProductInvoiceNo,
                        principalTable: "V1_PurchaseProducts",
                        principalColumn: "InvoiceNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_V1_CardPaymentDetails_EDCTerminalId",
                table: "V1_CardPaymentDetails",
                column: "EDCTerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_ProductItems_ProductItemBarcode",
                table: "V1_ProductItems",
                column: "ProductItemBarcode");

            migrationBuilder.CreateIndex(
                name: "IX_V1_ProductItems_PurchaseProductInvoiceNo",
                table: "V1_ProductItems",
                column: "PurchaseProductInvoiceNo");

            migrationBuilder.CreateIndex(
                name: "IX_V1_Products_ProductSubCategorySubCategory",
                table: "V1_Products",
                column: "ProductSubCategorySubCategory");

            migrationBuilder.CreateIndex(
                name: "IX_V1_ProductSales_SalesmanId",
                table: "V1_ProductSales",
                column: "SalesmanId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_ProductSales_StoreId",
                table: "V1_ProductSales",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_PurchaseProducts_StoreId",
                table: "V1_PurchaseProducts",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_PurchaseProducts_VendorId",
                table: "V1_PurchaseProducts",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_SaleItems_ProductItemBarcode",
                table: "V1_SaleItems",
                column: "ProductItemBarcode");

            migrationBuilder.CreateIndex(
                name: "IX_V1_SaleItems_ProductSaleInvoiceCode",
                table: "V1_SaleItems",
                column: "ProductSaleInvoiceCode");

            migrationBuilder.CreateIndex(
                name: "IX_V1_SalePaymentDetails_ProductSaleInvoiceCode",
                table: "V1_SalePaymentDetails",
                column: "ProductSaleInvoiceCode");

            migrationBuilder.CreateIndex(
                name: "IX_V1_Stocks_ProductBarcode",
                table: "V1_Stocks",
                column: "ProductBarcode");

            migrationBuilder.CreateIndex(
                name: "IX_V1_Stocks_StoreId",
                table: "V1_Stocks",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_Vendors_StoreId",
                table: "V1_Vendors",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "V1_CardPaymentDetails");

            migrationBuilder.DropTable(
                name: "V1_ProductItems");

            migrationBuilder.DropTable(
                name: "V1_SaleItems");

            migrationBuilder.DropTable(
                name: "V1_SalePaymentDetails");

            migrationBuilder.DropTable(
                name: "V1_Stocks");

            migrationBuilder.DropTable(
                name: "V1_PurchaseProducts");

            migrationBuilder.DropTable(
                name: "V1_ProductSales");

            migrationBuilder.DropTable(
                name: "V1_Products");

            migrationBuilder.DropTable(
                name: "V1_Vendors");

            migrationBuilder.DropTable(
                name: "ProductSubCategories");
        }
    }
}
