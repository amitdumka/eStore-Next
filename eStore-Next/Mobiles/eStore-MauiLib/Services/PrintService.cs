using System;
namespace eStore_MauiLib.Services
{

    public interface IPrintService
	{
		IList<string> GetDeviceList();
		Task Print(string devicename, string text);
		Task PrintFile(string device, string path);
	}

	 
}

