using System.Windows.Input;

namespace eStore_Maui;

public partial class AppShell : Shell
{
    public ICommand HelpCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
    public AppShell()
	{
		InitializeComponent();
	}

    void MenuItem_Clicked(System.Object sender, System.EventArgs e)
    {
    }
}
