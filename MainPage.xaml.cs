using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using WordleProject;
using System.Net;

namespace WordleProject
{
    public partial class MainPage : ContentPage
    {
        private readonly FileDownloader _fileDownloader;
        
        public MainPage()
        {
            InitializeComponent();
            _fileDownloader = new FileDownloader();
        }
       
        private async void Play_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Get a random word after ensuring the file is downloaded
                string randomWord = await _fileDownloader.GetRandomWordAsync();

                // Display the random word
                DisplayAlert("Random Word : ", randomWord, "Ok");

                await Navigation.PushAsync(new GamePage(randomWord));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Unable to fetch a word: {ex.Message}", "Ok");
            }            
        }

        private async void LogIn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogIn());
        }

        private async void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingPage());
        }
    }
}



       

