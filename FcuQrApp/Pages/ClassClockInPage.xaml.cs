using System.Net.Http.Headers;
namespace FcuQrApp.Pages;

[QueryProperty(nameof(Qrcode), "qrcode")]
public partial class ClassClockInPage : ContentPage
{
    private string qrcode = "";
    private string NID = Preferences.Default.Get("NID", "null");
	private string Password = Preferences.Default.Get("Password", "null");

    public string Qrcode
    {
        get => qrcode;
        set
        {
            qrcode = value;
            OnPropertyChanged();
        }
    }

    public ClassClockInPage()
	{
        InitializeComponent();
	}

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("../../");
    }

    protected override async void OnAppearing()
    {
        _ = DisplayAlert("qrcode", qrcode, "OK");
        LoginParams loginParams = new()
        {
            username = this.NID,
            password = this.Password,
            token = this.qrcode
        };
        QRredirect redirect = new(loginParams);
        string result = await redirect.postAsync();
        ClassClockInWeb.Source = new HtmlWebViewSource
        {
            Html = result
        };
        ClassClockInWeb.Reload();
    }

    public class LoginParams
    {
        public required string username { get; set; }
        public required string password { get; set; }
        public required string token { get; set; }
    }

    public class QRredirect(LoginParams loginParams)
	{
        public async Task<string> postAsync()
        {
            try
            {
                var parameters = new Dictionary<string, string>();
                var url = "https://signin.fcu.edu.tw/clockIn/ClassClockinQR.aspx";
                parameters.Add("username", loginParams.username);
                parameters.Add("password", loginParams.password);
                parameters.Add("token", loginParams.token);

                using (HttpClient client = new())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.PostAsync(url, new FormUrlEncodedContent(parameters)).Result;
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}