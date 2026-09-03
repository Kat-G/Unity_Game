using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackToggleButton : MonoBehaviour
{
    public int trackId;

    public void Toggle()
    {
        AchievementManager.Instance.ToggleTrack(trackId);
    }
}
