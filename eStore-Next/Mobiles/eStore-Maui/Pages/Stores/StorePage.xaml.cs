using AKS.Shared.Commons.Models;
using Syncfusion.Maui.DataGrid;

namespace eStore_Maui.Pages.Stores;

public partial class StorePage : ContentPage
{
	public StorePage()
	{
		InitializeComponent();
        Intit();
	}
	protected void Intit()
	{
        ColumnCollection gridColumns = new ColumnCollection();
        gridColumns.Add(new DataGridTextColumn() { HeaderText = "Code", MappingName = "StoreCode" });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = "Name", MappingName = "StoreName" });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = "City", MappingName = nameof(Store.City) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = "Date", MappingName = nameof(Store.BeginDate), Format="dd/MMM/yyyy"  });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = "Zip", MappingName = nameof(Store.ZipCode) });
        gridColumns.Add(new DataGridCheckBoxColumn() { HeaderText = "", MappingName = nameof(Store.IsActive) });
        this.dataGrid.Columns = gridColumns;
		


    }
}
