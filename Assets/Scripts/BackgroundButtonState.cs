using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundButtonState : MonoBehaviour
{
    public int backgroundId;  // 1 или 2
    private Button btn;
    private Image buttonImage;  // Чтобы показывать, включен ли фон
    
    void Start()
    {
        btn = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        
        // Проверяем, открыт ли фон
        if (!BackgroundManager.Instance.IsBackgroundUnlocked(backgroundId))
        {
            btn.interactable = false;
        }
        else
        {
            UpdateButtonVisual();
        }
    }
    
    void Update()
    {
        // Обновляем визуал (если кнопка уже активна)
        if (btn.interactable)
        {
            UpdateButtonVisual();
        }
    }
    
    void UpdateButtonVisual()
    {
        int enabledBackground = BackgroundManager.Instance.GetEnabledBackground();
        
        if (enabledBackground == backgroundId)
        {
            // Этот фон включен — подсвечиваем кнопку
            if (buttonImage != null)
            {
                buttonImage.color = new Color(0f, 0.8f, 0f, 0.5f);  // Или любой другой цвет
            }
        }
        else
        {
            // Фон не включен
            if (buttonImage != null)
            {
                buttonImage.color = Color.white;
            }
        }
    }
    
    // Эту функцию вешаем на onClick кнопки
    public void OnClick()
    {
        BackgroundManager.Instance.ToggleBackground(backgroundId);
    }
}
