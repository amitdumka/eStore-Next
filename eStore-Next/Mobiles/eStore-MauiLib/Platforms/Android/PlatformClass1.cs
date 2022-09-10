using Android.App;
using eStore_MauiLib;
using eStore_MauiLib.Services;
using static Android.Net.Wifi.WifiEnterpriseConfig;




[assembly: UsesPermission(Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.Bluetooth)]
[assembly: UsesPermission(Android.Manifest.Permission.BluetoothConnect)]
[assembly: UsesFeature("android.hardware.location", Required = false)]
[assembly: UsesFeature("android.hardware.location.gps", Required = false)]
[assembly: UsesFeature("android.hardware.location.network", Required = false)]
namespace eStore_MauiLib
{
    // All the code in this file is only included on Android.
    public class PlatformClass1
    {
    }




}