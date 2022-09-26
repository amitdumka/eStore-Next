using AKS.Shared.Commons.Ops;
using eStore_MauiLib.RemoteService;

namespace eStore_MauiLib.Pages.Auth;

public partial class LoginPage : ContentPage
{
    public LoginPage(Shell appshell)
    {
        InitializeComponent();
        viewModel.AppShell = appshell;
        SyncLocal();
    }

    private async void SyncLocal()
    {
        if (!CurrentSession.LocalStatus)
        {
            if (!DatabaseStatus.VerifyLocalStatus())
            {
                DatabaseStatus.SyncInitial();
                ButtonControls.IsVisible = CurrentSession.LocalStatus;
                signInButton.IsEnabled = true;
            }
            CurrentSession.LocalStatus = true;
            ButtonControls.IsVisible = CurrentSession.LocalStatus;
            signInButton.IsEnabled = true;
        }

        CurrentSession.LocalStatus = true;
        ButtonControls.IsVisible = CurrentSession.LocalStatus;
        signInButton.IsEnabled = true;

    }
}