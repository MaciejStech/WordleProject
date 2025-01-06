namespace WordleProject;

public partial class SettingPage : ContentPage
{
	public SettingPage()
	{
		InitializeComponent();
    }

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
    }

    private void Return_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void TimerToggle_Clicked(object sender, EventArgs e)
    {
        int timerToggle = AppSettings.Toggle;
        if(timerToggle == 0)
        {
            AppSettings.Toggle = 1;
        }
        else
        {
            AppSettings.Toggle = 0;
        }
        
    }
}