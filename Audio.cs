using Plugin.Maui.Audio;
using System;

namespace WordleProject;

public static class Audio
{
    private static IAudioPlayer audioplayer;
    private static bool PlayingAudio = false;

    public static void InitializeAudio()
    {
        var audioManager = AudioManager.Current;
        audioplayer = audioManager.CreatePlayer(FileSystem.OpenAppPackageFileAsync("night-imaginations-249599.mp3").Result);
    }

    public static void PlayMusic()
    {
        if (audioplayer == null)
        {
            InitializeAudio();
        }

        if (PlayingAudio != true)
        {
            audioplayer.Play();
            PlayingAudio = true;
        }
    }

    public static void StopMusic()
    {
        if (audioplayer != null && PlayingAudio == true)
        {
            audioplayer.Stop();
            PlayingAudio = false;
        }
    }

    public static bool playingAudio => PlayingAudio;
}

