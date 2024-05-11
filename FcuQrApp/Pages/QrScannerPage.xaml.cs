using BarcodeScanning;

namespace FcuQrApp.Pages;

public partial class QrScannerPage : ContentPage
{

    private readonly BarcodeDrawable _drawable = new();

    public QrScannerPage()
	{
        InitializeComponent();
    }   

    protected override async void OnAppearing()
    {
        await Methods.AskForRequiredPermissionAsync();
        base.OnAppearing();

        Barcode.CameraEnabled = true;
        Graphics.Drawable = _drawable;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Barcode.CameraEnabled = false;
    }

    private void ContentPage_Unloaded(object sender, EventArgs e)
    {
        Barcode.Handler?.DisconnectHandler();
    }

    private void CameraView_OnDetectionFinished(object sender, OnDetectionFinishedEventArg e)
    {
        _drawable.barcodeResults = e.BarcodeResults;
        Graphics.Invalidate();
        if (e.BarcodeResults.Length > 0)
        {
            Barcode.PauseScanning = true;
            string qrcode = e.BarcodeResults[0].DisplayValue;
            Shell.Current.GoToAsync($"ClassClockInPage?qrcode={qrcode}");
        }
    }

    private class BarcodeDrawable : IDrawable
    {
        public BarcodeResult[]? barcodeResults;
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (barcodeResults is not null && barcodeResults.Length > 0)
            {
                canvas.StrokeSize = 15;
                canvas.StrokeColor = Colors.DarkBlue;
                var scale = 1 / canvas.DisplayScale;
                canvas.Scale(scale, scale);

                foreach (var barcode in barcodeResults)
                {
                    canvas.DrawRectangle(barcode.PreviewBoundingBox);
                }
            }
        }
    }
}