using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager Instance;
    
    public GameObject defaultBackgroundPrefab;   // Дефолтный фон
    public GameObject background1Prefab;         // Фон 1 (открывается при 100 очках)
    public GameObject background2Prefab;         // Фон 2 (открывается при 300 очках)
    
    private GameObject currentBackgroundObject;  // Текущий фон на сцене
    private Transform backgroundParent;          // Родительский объект для фона (например, пустой объект "Backgrounds")
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        // Создаем контейнер для фонов, если его нет
        GameObject container = GameObject.Find("BackgroundContainer");
        if (container == null)
        {
            container = new GameObject("BackgroundContainer");
        }
        backgroundParent = container.transform;
        
        // Загружаем включенный фон
        int enabledBackground = GetEnabledBackground();
        if (enabledBackground != -1)
        {
            SetBackground(enabledBackground);
        }
        else
        {
            SetBackground(0); // Дефолтный
        }
    }
    
    public void CheckBackgroundUnlock(int score)
    {
        if (score >= 100 && !IsBackgroundUnlocked(1))
        {
            UnlockBackground(1);
            Debug.Log("Открыт фон 1!");
        }
        
        if (score >= 300 && !IsBackgroundUnlocked(2))
        {
            UnlockBackground(2);
            Debug.Log("Открыт фон 2!");
        }
    }
    
    void UnlockBackground(int id)
    {
        PlayerPrefs.SetInt($"Background_{id}_unlocked", 1);
    }
    
    public bool IsBackgroundUnlocked(int id)
    {
        return PlayerPrefs.GetInt($"Background_{id}_unlocked", 0) == 1;
    }
    
    public int GetEnabledBackground()
    {
        for (int i = 1; i <= 2; i++)
        {
            if (PlayerPrefs.GetInt($"Background_{i}_enabled", 0) == 1)
            {
                return i;
            }
        }
        return -1;
    }
    
    public void ToggleBackground(int id)
    {
        if (!IsBackgroundUnlocked(id))
        {
            Debug.Log($"Фон {id} еще не открыт!");
            return;
        }
        
        int currentEnabled = GetEnabledBackground();
        
        if (currentEnabled == id)
        {
            // Выключаем
            PlayerPrefs.SetInt($"Background_{id}_enabled", 0);
            SetBackground(0);
            Debug.Log($"🎨 Фон {id} → OFF");
        }
        else
        {
            // Выключаем текущий
            if (currentEnabled != -1)
            {
                PlayerPrefs.SetInt($"Background_{currentEnabled}_enabled", 0);
            }
            // Включаем новый
            PlayerPrefs.SetInt($"Background_{id}_enabled", 1);
            SetBackground(id);
            Debug.Log($"🎨 Фон {id} → ON");
        }
    }
    
    void SetBackground(int id)
    {
        // Удаляем текущий фон
        if (currentBackgroundObject != null)
        {
            Destroy(currentBackgroundObject);
        }
        
        // Создаем новый фон
        GameObject prefabToSpawn = null;
        
        switch (id)
        {
            case 0:
                prefabToSpawn = defaultBackgroundPrefab;
                break;
            case 1:
                prefabToSpawn = background1Prefab;
                break;
            case 2:
                prefabToSpawn = background2Prefab;
                break;
        }
        
        if (prefabToSpawn != null)
        {
            currentBackgroundObject = Instantiate(prefabToSpawn, backgroundParent);
            Debug.Log($"Фон {id} загружен");
        }
    }
    
    public void RefreshBackground()
    {
        int enabledBackground = GetEnabledBackground();
        if (enabledBackground != -1)
        {
            SetBackground(enabledBackground);
        }
        else
        {
            SetBackground(0);
        }
    }
}
