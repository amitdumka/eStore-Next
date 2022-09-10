using System;
namespace eStore_Maui.Test
{
	public interface ISave
	{
          Task SaveAndView(string filename, string contentType, MemoryStream stream);

    }
}

