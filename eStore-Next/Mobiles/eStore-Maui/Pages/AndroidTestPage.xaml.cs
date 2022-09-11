

using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using eStore_Maui.Test;
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
        //using var stream =
        //         await FileSystem.OpenAppPackageFileAsync("gis.pdf");
        //_aksPrint.Print(stream, "gis.pdf");
        //PrintMessage += " Printer  is awesome!";
        //byte[] bytes = Encoding.ASCII.GetBytes(PrintMessage);
        //_aksPrint.Print(bytes, "test2.txt");
        //await _blueToothService.Print(SelectedDevice, PrintMessage);
        DummyPdf.Get();
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
    public static Stream GetStreamFromFile(string filename)
    {
        var assembly = typeof(App).GetTypeInfo().Assembly;
        var assemblyName = assembly.GetName().Name;

        var stream = assembly.GetManifestResourceStream($"{assemblyName}.{filename}");

        return stream;
    }

}

