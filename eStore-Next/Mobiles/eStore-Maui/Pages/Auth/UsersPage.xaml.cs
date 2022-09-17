using AKS.Shared.Commons.Models.Auth;
using eStore_MauiLib.ViewModels.Auth;
using Microsoft.Maui.Controls.Compatibility.Platform.Android.CollectionView;
using Syncfusion.Maui.DataGrid;

namespace eStore_Maui.Pages.Auth;

public partial class UsersPage : ContentPage
{
	public UsersViewModel viewModel;
    public static ColumnCollection gridColumns;
    public UsersPage()
	{
        InitializeComponent();
        viewModel = new UsersViewModel();
        BindingContext = viewModel;
        RLV.BindingContext=viewModel;
	    viewModel.Icon = eStore_Maui.Resources.Styles.IconFont.UserFriends;
        RLV.Cols= SetGridCols();
	
		
	}
    protected ColumnCollection SetGridCols()
    {
         gridColumns = new();
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(User.UserName), MappingName = nameof(User.UserName) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(User.GuestName),MappingName = nameof(User.GuestName) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(User.UserType),MappingName = nameof(User.UserType) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(User.Role),MappingName = nameof(User.Role) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(User.StoreId),MappingName = nameof(User.StoreId) });
        gridColumns.Add(new DataGridCheckBoxColumn() { HeaderText = nameof(User.Enabled),MappingName = nameof(User.Enabled) });
        return gridColumns;
    }
}
