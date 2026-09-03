using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackButtonState : MonoBehaviour
{
    public int trackId;
    Button btn;
    private Image buttonImage;

    void Start()
    {
        btn = GetComponent<Button>();
        buttonImage = GetComponent<Image>(); 

        if (!AchievementManager.Instance.IsUnlocked(trackId))
            btn.interactable = false;
        else
        {
            UpdateButtonVisual();  // Добавить
        }
    }

    void Update()
    {
        // Добавить блок
        if (btn.interactable)
        {
            UpdateButtonVisual();
        }
    }

    // Добавить новый метод
    void UpdateButtonVisual()
    {
        if (buttonImage == null) return;
        
        bool isEnabled = AchievementManager.Instance.IsEnabled(trackId);
        buttonImage.color = isEnabled ? new Color(0f, 0.8f, 0f, 0.5f) : Color.white;
    }
}
