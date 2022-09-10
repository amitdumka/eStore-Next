//using System;
namespace eStore_MauiLib.Services.Print
{
//    public interface IPrintService
//    {
//        void Print(Stream inputStream, string fileName);
//    }

    public interface IPrinterService
    {
        void Print(string filename);
        void Print(Stream inputStream, string fileName);
        void Print(byte[] text, string fileName);




    }
}

