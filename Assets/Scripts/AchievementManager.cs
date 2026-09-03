using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckScoreAchievements(int score)
    {
        if (score >= 100)
            UnlockTrack(1);

        if (score >= 300)
            UnlockTrack(2);
    }

    void UnlockTrack(int id)
    {
        PlayerPrefs.SetInt($"Track_{id}_unlocked", 1);
    }

    public bool IsUnlocked(int id)
    {
        return PlayerPrefs.GetInt($"Track_{id}_unlocked", id == 0 ? 1 : 0) == 1;
    }

    public bool IsEnabled(int id)
    {
        return PlayerPrefs.GetInt($"Track_{id}_enabled", 0) == 1;
    }

    public void ToggleTrack(int id)
    {
        int current = PlayerPrefs.GetInt($"Track_{id}_enabled", 0);
        int newValue = current == 1 ? 0 : 1;

        PlayerPrefs.SetInt($"Track_{id}_enabled", newValue);

        Debug.Log($"🎛 Track {id} → {(newValue == 1 ? "ON" : "OFF")}");

        MusicManager.Instance.RefreshMusic();
    }
}