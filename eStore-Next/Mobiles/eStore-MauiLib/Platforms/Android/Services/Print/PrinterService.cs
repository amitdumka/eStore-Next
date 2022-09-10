using System;
using Android.Content;
using Android.OS;
using Android.Print;
using eStore_MauiLib.Platforms.Android.Services.Print;
using eStore_MauiLib.Services;
using eStore_MauiLib.Services.Print;
using Java.IO;
using Microsoft.Maui.Platform;
using FileNotFoundException = Java.IO.FileNotFoundException;
using IOException = Java.IO.IOException;

[assembly: Dependency(typeof(PrinterService))]
namespace eStore_MauiLib.Platforms.Android.Services.Print
{
    public class PrinterService : IPrinterService
    {

        public void Print(byte[] text, string fileName)
        {
            try
            {

                string createdFilePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileName);
                //Save the stream to the created file
                using (var dest = System.IO.File.OpenWrite(createdFilePath))
                {
                    dest.Write(text, 0, 10);
                }
                string filePath = createdFilePath;
                var activity = Platform.CurrentActivity;
                //Microsoft.Maui.Essentials
                //Xamarin.Essentials.Platform.CurrentActivity;

                PrintManager printManager = (PrintManager)activity.GetSystemService(Context.PrintService);
                PrintDocumentAdapter pda = new CustomPrintDocumentAdapter(filePath);
                //Print with null PrintAttributes
                printManager.Print(fileName, pda, null);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }
        public void Print(Stream inputStream, string fileName)
        {
            if (inputStream.CanSeek)
                //Reset the position of PDF document stream to be printed
                inputStream.Position = 0;
            //Create a new file in the Personal folder with the given name
            string createdFilePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileName);
            //Save the stream to the created file
            using (var dest = System.IO.File.OpenWrite(createdFilePath))
                inputStream.CopyTo(dest);
            string filePath = createdFilePath;

            var activity = Platform.CurrentActivity;
            //Microsoft.Maui.Essentials
            //Xamarin.Essentials.Platform.CurrentActivity;

            PrintManager printManager = (PrintManager)activity.GetSystemService(Context.PrintService);
            PrintDocumentAdapter pda = new CustomPrintDocumentAdapter(filePath);
            //Print with null PrintAttributes
            printManager.Print(fileName, pda, null);
        }

        public void Print(string fileName)
        {
            string createdFilePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileName);
            string filePath = createdFilePath;
            var activity = Platform.CurrentActivity;
            //Microsoft.Maui.Essentials
            //Xamarin.Essentials.Platform.CurrentActivity;

            PrintManager printManager = (PrintManager)activity.GetSystemService(Context.PrintService);
            PrintDocumentAdapter pda = new CustomPrintDocumentAdapter(filePath);
            //Print with null PrintAttributes
            printManager.Print(fileName, pda, null);
            throw new NotImplementedException();
        }
    }

    internal class CustomPrintDocumentAdapter : PrintDocumentAdapter
    {
        internal string FileToPrint { get; set; }

        internal CustomPrintDocumentAdapter(string fileDesc)
        {
            FileToPrint = fileDesc;
        }
        public override void OnLayout(PrintAttributes oldAttributes, PrintAttributes newAttributes, CancellationSignal cancellationSignal, LayoutResultCallback callback, Bundle extras)
        {
            if (cancellationSignal.IsCanceled)
            {
                callback.OnLayoutCancelled();
                return;
            }


            PrintDocumentInfo pdi = new PrintDocumentInfo.Builder(FileToPrint).SetContentType(PrintContentType.Unknown).Build(); //Android.Print.PrintContentType.Document).Build();

            callback.OnLayoutFinished(pdi, true);
        }

        public override void OnWrite(PageRange[] pages, ParcelFileDescriptor destination, CancellationSignal cancellationSignal, WriteResultCallback callback)
        {
            InputStream input = null;
            OutputStream output = null;

            try
            {
                //Create FileInputStream object from the given file
                input = new FileInputStream(FileToPrint);
                //Create FileOutputStream object from the destination FileDescriptor instance
                output = new FileOutputStream(destination.FileDescriptor);

                byte[] buf = new byte[1024];
                int bytesRead;

                while ((bytesRead = input.Read(buf)) > 0)
                {
                    //Write the contents of the given file to the print destination
                    output.Write(buf, 0, bytesRead);
                }

                callback.OnWriteFinished(new PageRange[] { PageRange.AllPages });

            }
            catch (FileNotFoundException ee)
            {
                //Catch exception
            }
            catch (Exception e)
            {
                //Catch exception
            }
            finally
            {
                try
                {
                    input.Close();
                    output.Close();
                }
                catch (IOException e)
                {
                    e.PrintStackTrace();
                }
            }
        }
    }
}

