using Microsoft.Maui.Storage;
using System.IO;

namespace WordleProject;

public partial class LogIn : ContentPage
{
	public LogIn()
	{
		InitializeComponent();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		TitleLogIn.Text = "Log In";
        MemberPrompt.IsVisible = false;
    }

    //Creates file and brings player to main menu once "logged in"
    private void UsernameGo_Clicked(object sender, EventArgs e)
    {
        string username = Username.Text;

        //File Path Line generated using ChatGPT
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{username}.txt");
        DisplayAlert("Logged In", "You have successfully logged in.", "Ok");
        AppSettings.Log = 1;
        AppSettings.UserSave = filePath;
        Navigation.PopAsync();
        
    }

    private void Return_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}