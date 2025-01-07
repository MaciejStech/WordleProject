namespace WordleProject;

public partial class SettingPage : ContentPage
{
    int music = 0;
	public SettingPage()
	{
		InitializeComponent();
    }

    //Handling Music being turned on/off and changing button colour
    private void MusicOnOff_Clicked(object sender, EventArgs e)
    {
        if(Audio.playingAudio)
        {
            Audio.StopMusic();
        }
        else
        {
            Audio.PlayMusic();
        }
        if (music == 0)
        {
            MusicOnOff.BackgroundColor = Color.FromArgb("#ff0000");
            music = 1;
        }
        else
        {
            music = 0;
            MusicOnOff.BackgroundColor = Color.FromArgb("#00ff00");
        }
    }

    private void Return_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    
    //Handling Timer visibility and changing button colour
    private void TimerToggle_Clicked(object sender, EventArgs e)
    {
        int timerToggle = AppSettings.Toggle;
        if(timerToggle == 0)
        {
            AppSettings.Toggle = 1;
            TimerToggle.BackgroundColor = Color.FromArgb("#00ff00");
        }
        else
        {
            AppSettings.Toggle = 0;
            TimerToggle.BackgroundColor = Color.FromArgb("#ff0000");
        }
        
    }
}