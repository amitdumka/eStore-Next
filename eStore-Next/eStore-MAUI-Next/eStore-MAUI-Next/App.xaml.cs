using eStore_MAUI_Next.Auth;

namespace eStore_MAUI_Next;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

	//	MainPage = new AppShell();
	}

	//TODO: Added from Old prj
	//protected override Window CreateWindow(IActivationState activationState) =>
	//  new Window(new NavigationPage(new MainPage())) { Title = "Weather TwentyOne" };
	protected override Window CreateWindow(IActivationState activationState)
	{
		return new Window(new LoginPage());
	}
}
