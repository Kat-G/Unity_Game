using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public Image[] lifeIcons;  // Перетащите сюда 3 картинки-моськи
    
    public void UpdateLives(int currentLives)
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            // Если индекс меньше количества жизней — показываем иконку
            if (lifeIcons[i] != null)
            {
                lifeIcons[i].enabled = (i < currentLives);
            }
        }
    }
}