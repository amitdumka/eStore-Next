using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace eStore_MAUI_Next;

class Program : MauiApplication
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	static void Main(string[] args)
	{
		var app = new Program();
		app.Run(args);
	}
}
