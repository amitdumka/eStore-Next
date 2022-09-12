using eStore_Maui.Test;
using eStore_MauiLib.Helpers;
using eStore_MauiLib.Services;
using eStore_MauiLib.Services.Print;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;

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
        //List<string> lines = new List<string>();
        //var ds = PrintMessage.Split("#");
        //foreach (var item in ds)
        //{
        //    lines.Add(item);
        //}
        //DummyPdf.PrintPdf("Testing", lines, "Testing.pdf");
        TestPaySlip.Print();
    });

    public PrintPageViewModel()
    {
        _blueToothService = DependencyService.Get<IPrintService>();
        if (_blueToothService == null)
        {
            _blueToothService = ServiceHelper.GetService<IPrintService>();
        }
        _aksPrint = ServiceHelper.GetService<IPrinterService>();

        var list = _blueToothService.GetDeviceList();
        if (list != null)
        {
            DeviceList.Clear();
            foreach (var item in list)
            {
                DeviceList.Add(item);
            }
        }
        else
        {
            DeviceList.Add("No Device Found!");
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