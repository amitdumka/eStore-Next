using AKS.SRPMix.Forms;

namespace AKS.Payroll
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string x = "NzA0Mjc1QDMyMzAyZTMyMmUzMGVlVVJHWjhGTFdndjlqQnhYVTBjblBsYWZPOFA3UUh5Zmg0RXlzeWVuOGs9";

            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(x);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}