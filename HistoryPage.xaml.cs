using System;
using System.IO;

namespace WordleProject;

public partial class HistoryPage : ContentPage
{
	private static string filePathHistory = AppSettings.UserSave;
	public HistoryPage()
	{
		InitializeComponent();
		LoadData();
	}

	public async void LoadData()
	{
		if(File.Exists(filePathHistory))
		{
            string fileContents = File.ReadAllText(filePathHistory);

			if(string.IsNullOrWhiteSpace(filePathHistory))
			{
				await DisplayAlert("Error", "No content in this file", "Ok");
				await Navigation.PopAsync();
			}
			else
			{
				FileContents.Text = fileContents;
                
            }
        }
	}
}