using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace FcuQrApp.Pages;

[QueryProperty("Qrcode", "qrcode")]
public partial class ClassClockInPage : ContentPage
{

	private string NID = Preferences.Default.Get("NID", "null");
	private string Password = Preferences.Default.Get("Password", "null");
    string ClassClockInURLBase = "https://signin.fcu.edu.tw/clockin/ClassClockIn.aspx?param=";
    private string qrcode = "";

    public string TextData
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
        await Shell.Current.GoToAsync("..");
    }

    protected override void OnAppearing()
    {
        ClassClockInWeb.Source = ClassClockInURLBase;
        DisplayAlert("qrcode", qrcode, "OK");
        /*
        bool success = NIDLogin();
        if (success)
        {
            DisplayAlert("Success", "登入成功", "OK");
            ClassClockInWeb.Source = ClassClockInURLBase + qrcode;
        }
        else
        {
            DisplayAlert("Error", "登入失敗", "OK");
        }
        */
    }

	private bool NIDLogin()
	{
		if (this.NID == "null" || this.Password == "null")
		{
            DisplayAlert("Error", "請在設定輸入 NID 與密碼", "OK");
			return false;
        }

        LoginParams loginParams = new LoginParams
        {
            BeaconMinor = "0",
            BeaconUUID = "0",
            RedirectService = "Sing-in",
            Password = this.Password,
            Account = this.NID,
            BeaconMajor = ""
        };

        Redirect redirect = new Redirect(loginParams);
        return redirect.postAsync().Result;
    }

    public class LoginParams
    {
        public required string BeaconMinor { get; set; }
        public required string BeaconUUID { get; set; }
        public required string RedirectService { get; set; }
        public required string Password { get; set; }
        public required string Account { get; set; }
        public required string BeaconMajor { get; set; }
    }

    public class Redirect(LoginParams loginParams)
	{
        string ClassClockInURL = "https://service206-sds.fcu.edu.tw/mobileservice/RedirectService.svc/Redirect";
        
        HttpClient _client = new HttpClient();

        public async Task<bool> postAsync()
        {
            var _content = new StringContent(JsonConvert.SerializeObject(loginParams), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(ClassClockInURL, _content);
            var responseContent = await response.Content.ReadAsStringAsync();
            return (response.StatusCode == HttpStatusCode.OK);
        }
    }
}