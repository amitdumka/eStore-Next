namespace eStore_MAUI_Next.Auth;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
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
        //UsersDataModel dm = new UsersDataModel();
        //return dm.DoLogin(username, password);
        return true;

    }
}