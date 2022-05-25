namespace eStore_MAUI_Next;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }
    private void OnSyncMenuClicked(object sender, EventArgs e)
    {
        DisplayAlert("Info", "Your account has been created successfully", "OK");
    }
    private void DoLogout(object sender, EventArgs e)
    {
        Application.Current.MainPage = new LoginPage();
    }
}
