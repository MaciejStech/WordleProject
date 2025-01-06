namespace WordleProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Audio.InitializeAudio();
            Audio.PlayMusic();
            MainPage = new AppShell();
        }
    }
}
