using eStore_MauiLib.ViewModels.Auth;
using Microsoft.Maui.Controls.Compatibility.Platform.Android.CollectionView;

namespace eStore_Maui.Pages.Auth;

public partial class UsersPage : ContentPage
{
	public UsersViewModel viewModel;
	public UsersPage()
	{
        InitializeComponent();
        viewModel = new UsersViewModel();
        BindingContext = viewModel;
        RLV.Parent = this;
		RLV.BindingContext=viewModel;
		viewModel.Icon = eStore_Maui.Resources.Styles.IconFont.UserFriends;
	
		
	}
}
