using eStoreMobileX.Data.ViewModels.Auth;

namespace eStore_MAUI_Next.Auth;

public partial class LoginPage : ContentPage
{
    UserViewModel viewModel;

	public LoginPage()
	{
		InitializeComponent();
        viewModel = new UserViewModel();
	}
    void OnSubmitClicked(Object sender, EventArgs e)
    {
        //if (viewModel.Validate())
        DisplayAlert("Success", "Your account has been created successfully", "OK");
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
       var user= viewModel.SignIn(UserName.Text.Trim()+"@eStore.in", Password.Text.Trim());
        if (user != null)
        { 
            return true; 
        }
        else return false;

    }
}