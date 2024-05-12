namespace FcuQrApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            // close dark mode
            UserAppTheme = AppTheme.Light;
        }
    }
}
