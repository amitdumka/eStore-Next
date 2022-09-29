using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.MAUILib.Services.Print
{
    public interface IPrinterService
    {
        void Print(string filename);
        void Print(Stream inputStream, string fileName);
        void Print(byte[] text, string fileName);

    }
    public interface IPrintService
    {
        IList<string> GetDeviceList();
        Task Print(string devicename, string text);
        Task PrintFile(string device, string path);
    }
   

}
//namespace eStore.MAUILib.Services{
// partial class SaveService
//{
//    public partial void SaveAndView(string filename, string contentType, MemoryStream stream);
//}}