using AKS.Shared.Commons.Ops;
namespace eStore.MAUILib.Pages.Auth;

public partial class LoginPage : ContentPage
{
    public LoginPage(Shell appshell)
    {
        InitializeComponent();
        viewModel.AppShell = appshell;
        viewModel.SyncLocal();
    }

    
}