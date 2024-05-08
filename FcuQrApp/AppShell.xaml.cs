using FcuQrApp.Pages;

namespace FcuQrApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("SettingPage", typeof(SettingPage));
            Routing.RegisterRoute("QrScannerPage", typeof(QrScannerPage));
            Routing.RegisterRoute("ClassClockInPage", typeof(ClassClockInPage));
        }
    }
}
