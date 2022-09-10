using System.Diagnostics;
using System.Text;
using Android.App;
using Android.Bluetooth;
using eStore_MauiLib.Services; 
using Java.Util;

[assembly: Dependency(typeof(PrintService))]
[assembly: UsesPermission(Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.Bluetooth)]
[assembly: UsesPermission(Android.Manifest.Permission.BluetoothConnect)]
[assembly: UsesFeature("android.hardware.location", Required = false)]
[assembly: UsesFeature("android.hardware.location.gps", Required = false)]
[assembly: UsesFeature("android.hardware.location.network", Required = false)]
namespace eStore_MauiLib.Services
{

    public class PrintService : IPrintService
    {
        BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter;

        public IList<string> GetDeviceList()
        {

            var btdevice = bluetoothAdapter?.BondedDevices
            .Select(i => i.Name).ToList();
            return btdevice;

        }

        public async Task Print(string deviceName, string text)
        {

            BluetoothDevice device = (from bd in bluetoothAdapter?.BondedDevices
                                      where bd?.Name == deviceName
                                      select bd).FirstOrDefault();
            try
            {
                BluetoothSocket bluetoothSocket = device?.
                    CreateRfcommSocketToServiceRecord(
                    UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));

                bluetoothSocket?.Connect();
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                await bluetoothSocket?.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                bluetoothSocket.Close();

            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp.Message);

                throw exp;
            }
        }
        public Task PrintFile(string device, string path)
        {
            throw new NotImplementedException();
        }
    }

}
