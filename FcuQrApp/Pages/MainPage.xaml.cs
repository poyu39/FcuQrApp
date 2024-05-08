namespace FcuQrApp.Pages;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    private async void SettingButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("SettingPage");
    }

    private async void QrScannerButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("QrScannerPage");
    }
}