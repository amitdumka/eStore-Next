using AKS.Shared.Commons.Ops;
using eStore_MauiLib.RemoteService;

namespace eStore_Maui.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        viewModel.AppShell = new AppShell();
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
                //signUpButton.IsEnabled = true;
            }
            else
            {
                CurrentSession.LocalStatus = true;
                ButtonControls.IsVisible = CurrentSession.LocalStatus;
                signInButton.IsEnabled = true;
                //signUpButton.IsEnabled = true;
            }
        }
    }
}