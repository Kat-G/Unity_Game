using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource source;

    public AudioClip defaultTrack;
    public AudioClip track1;
    public AudioClip track2;

    void Awake()
    {
        Instance = this;

        RefreshMusic();
    }

    public void RefreshMusic()
    {
        AudioClip selected = defaultTrack;
        int trackId = 0;

        if (AchievementManager.Instance.IsEnabled(2) &&
            AchievementManager.Instance.IsUnlocked(2))
        {
            selected = track2;
            trackId = 2;
        }
        else if (AchievementManager.Instance.IsEnabled(1) &&
                 AchievementManager.Instance.IsUnlocked(1))
        {
            selected = track1;
            trackId = 1;
        }

        Debug.Log("🎵 MusicManager → selected track: " + trackId);

        if (source.clip != selected)
        {
            Debug.Log("🔁 Switching track to: " + trackId);

            source.clip = selected;
            source.Play();
        }
        else
        {
            Debug.Log("⏸ Track already playing: " + trackId);
        }
    }
}