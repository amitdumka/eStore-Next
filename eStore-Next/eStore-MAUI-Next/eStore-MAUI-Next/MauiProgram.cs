//using DevExpress.Maui;

namespace eStore_MAUI_Next;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
               //.UseDevExpress()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("UIFontIcons.ttf", "FontIcons");
				fonts.AddFont("Montserrat-Regular.ttf", "MontRegular");
				fonts.AddFont("Montserrat-Bold.ttf", "MontBold");
				fonts.AddFont("Montserrat-Medium.ttf", "MontMedium");
				fonts.AddFont("Montserrat-SemiBold.ttf", "MontSemiBold");
			});

		return builder.Build();
	}
}
