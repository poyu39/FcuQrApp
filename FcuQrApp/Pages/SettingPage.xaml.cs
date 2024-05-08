namespace FcuQrApp.Pages;

public partial class SettingPage : ContentPage
{

	public SettingPage()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
        NID.Text = Preferences.Default.Get("NID", "null");
        Password.Text = Preferences.Default.Get("Password", "null");
    }

	public void SaveButton_Clicked(object sender, EventArgs e)
	{
        Preferences.Default.Set("NID", NID.Text);
        Preferences.Default.Set("Password", Password.Text);
    }
}