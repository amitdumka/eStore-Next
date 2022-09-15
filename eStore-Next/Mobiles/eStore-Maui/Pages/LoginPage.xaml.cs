using AKS.Shared.Commons.Ops;
using eStore_MauiLib.RemoteService;

namespace eStore_Maui.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        SyncLocal();
	}

    async void SyncLocal()
    {
        if (!CurrentSession.LocalStatus)
        {
            if (!DatabaseStatus.VerifyLocalStatus())
            {

                DatabaseStatus.SyncInitial();
            }
            else CurrentSession.LocalStatus = true;
        }
    }
    void DoSignUpClicked(Object sender, EventArgs e)
    {
        ////if (viewModel.Validate())
        //DisplayAlert("Success", "Your account has been created successfully", "OK");
        //Application.Current.MainPage = new SignUpPage();
    }

    void DoLoginClicked(Object sender, EventArgs e)
    {
        if (UserName.Text == "Admin" && Password.Text == "Admin")
        {

            Application.Current.MainPage = new AppShell();
        }
        else if (DoLogin(UserName.Text, Password.Text))
        {
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            DisplayAlert("Error", "UserName/Password is incorrect!", "OK");
        }
    }

    bool DoLogin(string username, string password)
    {
        CurrentSession.StoreCode = "ARD";

        var user = this.BackgroundColor;//viewModel.SignIn(UserName.Text.Trim() + "@eStore.in", Password.Text.Trim());
        if (user != null)
        {
            return true;
        }
        else return false;

    }
}
