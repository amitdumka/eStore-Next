﻿using AKS.Shared.Commons.Models;
using Syncfusion.Maui.DataGrid;

namespace eStore_Maui.Pages.Stores;

public partial class StorePage : ContentPage
{
	public StorePage()
	{
		InitializeComponent();
        Intit();
	}
    //async void OnLeft(object sender, EventArgs e)
    //{
    //    await DisplayAlert("Left!", "Left Swipe.", "OK");
    //}
    //async void OnRight(object sender, EventArgs e)
    //{
    //    await DisplayAlert("Right!", "Right Swipe.", "OK");
    //}
    //private void ExportToExcel_Clicked(object sender, EventArgs e)
    //{
    //    DataGridExcelExportingController excelExport = new DataGridExcelExportingController();
    //    var excelEngine = excelExport.ExportToExcel(this.dataGrid);
    //    var workbook = excelEngine.Excel.Workbooks[0];
    //    MemoryStream stream = new MemoryStream();
    //    workbook.SaveAs(stream);
    //    workbook.Close();
    //    excelEngine.Dispose();

    //    //.Forms.DependencyService.Get<ISave>().Save("DataGrid.xlsx", "application/msexcel", stream);
    //}
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
