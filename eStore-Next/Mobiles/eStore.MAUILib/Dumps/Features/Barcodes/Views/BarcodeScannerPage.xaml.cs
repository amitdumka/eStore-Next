using ZXing.Net.Maui;

namespace eStore_Maui.Features.Barcodes.Views;

public partial class BarcodeScannerPage : ContentPage
{
    public BarcodeScannerPage()
    {
        InitializeComponent();
    }
    void Init()
    {
        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.OneDimensional,
            AutoRotate = true,
            Multiple = true
        };


    }
    void TorchOn()
    {
        cameraBarcodeReaderView.IsTorchOn = !cameraBarcodeReaderView.IsTorchOn;
    }
    void FlipCamera()
    {
        cameraBarcodeReaderView.CameraLocation
  = cameraBarcodeReaderView.CameraLocation == CameraLocation.Rear ? CameraLocation.Front : CameraLocation.Rear;
    }
    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        foreach (var barcode in e.Results)
           txtBarcode.Text+= $"\tBarcodes: {barcode.Format} -> {barcode.Value}";
    }

    void Add_Clicked(System.Object sender, System.EventArgs e)
    {
    }
}
