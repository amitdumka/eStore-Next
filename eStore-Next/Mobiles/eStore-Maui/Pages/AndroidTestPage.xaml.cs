

using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using eStore_MauiLib.Services;
using eStore_MauiLib.Services.Print;
using Org.Apache.Http.Authentication;

namespace eStore_Maui.Pages;

public partial class AndroidTestPage : ContentPage
{

    public AndroidTestPage()
    {
        InitializeComponent();
        //var x= PrintService.GetDeviceList();
        //printerList.ItemsSource = x!=null ? x : new List<string>() { "NOT", "aviable" };
        BindingContext = new PrintPageViewModel();
    }

    private void print()
    {
        //byte[] bytes = Encoding.ASCII.GetBytes(author);
        //DependencyService.Get<IPrintService>().Print(stream, "test.txt");
    }




}
public class PrintPageViewModel
{
    private readonly IPrintService _blueToothService;
    private readonly IPrinterService _aksPrint;

    private IList<string> _deviceList;
    public IList<string> DeviceList
    {
        get
        {
            if (_deviceList == null)
                _deviceList = new ObservableCollection<string>();
            return _deviceList;
        }
        set
        {
            _deviceList = value;
        }
    }

    private string _printMessage;
    public string PrintMessage
    {
        get
        {
            return _printMessage;
        }
        set
        {
            _printMessage = value;
        }
    }

    private string _selectedDevice;
    public string SelectedDevice
    {
        get
        {
            return _selectedDevice;
        }
        set
        {
            _selectedDevice = value;
        }
    }

    public ICommand PrintCommand => new Command(async () =>
    {
        PrintMessage += " Xamarin Forms is awesome!";
        await _blueToothService.Print(SelectedDevice, PrintMessage);
    });
    public ICommand Print2Command => new Command(async () =>
    {
        PrintMessage += " Printer  is awesome!";
        byte[] bytes = Encoding.ASCII.GetBytes(PrintMessage);
        _aksPrint.Print(bytes, "test2.txt");
        //await _blueToothService.Print(SelectedDevice, PrintMessage);
    });

    public PrintPageViewModel()
    {
        _blueToothService = DependencyService.Get<IPrintService>();
        if (_blueToothService == null)
        {
           _blueToothService= ServiceHelper.GetService<IPrintService>();
        }
        _aksPrint= ServiceHelper.GetService<IPrinterService>();

        var list = _blueToothService.GetDeviceList();
        DeviceList.Clear();
        foreach (var item in list)
        {
            DeviceList.Add(item);
        }
    }

}

public static class ServiceHelper
{

    public static T GetService<T>() => Current.GetService<T>();

    public static IServiceProvider Current =>
#if WINDOWS10_0_17763_0_OR_GREATER
    MauiWinUIApplication.Current.Services;
#elif ANDROID
        MauiApplication.Current.Services;
#elif IOS || MACCATALYST
    MauiUIApplicationDelegate.Current.Services;
#else
    null;
#endif
}