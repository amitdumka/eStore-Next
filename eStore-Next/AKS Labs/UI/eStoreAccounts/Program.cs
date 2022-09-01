using AKS.Payroll;

namespace eStoreAccounts
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            string x = "NzA0Mjc1QDMyMzAyZTMyMmUzMGVlVVJHWjhGTFdndjlqQnhYVTBjblBsYWZPOFA3UUh5Zmg0RXlzeWVuOGs9";

            //Register Sync fusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(x);
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm(new MainForm()));
        }
    }
}