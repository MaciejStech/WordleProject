namespace WordleProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //Playing Music on Start
            Audio.InitializeAudio();
            Audio.PlayMusic();

            MainPage = new AppShell();
        }
    }
}
