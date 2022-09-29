using AKS.Shared.Commons.Models.Auth;
using eStore.MAUILib.ViewModels.Auth;
using Syncfusion.Maui.DataGrid;

namespace eStore.MAUILib.Pages.Auth;

public partial class UsersPage : ContentPage
{
    public AuthViewModel viewModel;
    public static ColumnCollection gridColumns;

    public UsersPage()
    {
        InitializeComponent();
        viewModel = new AuthViewModel();
        BindingContext = viewModel;
        RLV.BindingContext = viewModel;
        viewModel.Icon = MAUILib.Resources.Styles.IconFont.UserFriends;
        RLV.Cols = SetGridCols();
    }

    protected ColumnCollection SetGridCols()
    {
        gridColumns = new();
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(User.UserName), MappingName = nameof(User.UserName) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(User.GuestName), MappingName = nameof(User.GuestName) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(User.UserType), MappingName = nameof(User.UserType) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(User.Role), MappingName = nameof(User.Role) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(User.StoreId), MappingName = nameof(User.StoreId) });
        gridColumns.Add(new DataGridCheckBoxColumn() { HeaderText = nameof(User.Enabled), MappingName = nameof(User.Enabled) });
        return gridColumns;
    }
}