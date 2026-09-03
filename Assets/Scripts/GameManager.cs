using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverPanel;
    public GameObject startPanel;
    public GameObject player;
    public GameObject score;

    private bool isBonusActive = false;
    public int maxLives = 3;
    private int currentLives;

    public LivesUI livesUI;
    public GameObject livesPanel;

    private bool isInvincible = false;
    public float invincibilityDuration = 1f;

    public Image maskImage;                  // Ссылка на маску (UI Image)
    public float maskFadeDuration = 1.5f;    // Время появления маски
    public GameObject spawner;   

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentLives = maxLives;
        if (livesUI != null)
        {   
            livesUI.UpdateLives(currentLives);
        }

        Time.timeScale = 0f;

        startPanel.SetActive(true);

	if (maskImage != null)
        {
            maskImage.gameObject.SetActive(false);
            maskImage.color = new Color(1, 1, 1, 0);
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        livesPanel.SetActive(true);
        startPanel.SetActive(false);
        player.SetActive(true);
        score.SetActive(true);
    }

    public void TakeDamage()
    {
        if (isInvincible) return;

        currentLives--;
    
        if (livesUI != null) {
            livesUI.UpdateLives(currentLives);
        }
    
        if (currentLives <= 0) {
            GameOver();
        }
        else {
            StartCoroutine(InvincibilityFrames());
        }
    }

    public void AddLife()
    {
        if (currentLives < maxLives)
        {
            currentLives++;
            Debug.Log($"Жизней стало: {currentLives}");
        
            if (livesUI != null)
            {
                livesUI.UpdateLives(currentLives);
            }
        }
        else
        {
            Debug.Log("Жизней уже максимум!");
        }
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
	
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        yield return new WaitForSecondsRealtime(invincibilityDuration);
        isInvincible = false;
    }

    public void StartBonusMode()
    {
        if (isBonusActive) return;
        isBonusActive = true;

        Debug.Log("БОНУСНЫЙ РЕЖИМ ЗАПУЩЕН!");

        Time.timeScale = 0f;

        if (spawner != null)
            spawner.SetActive(false);

        StartCoroutine(ShowMaskAndStartBonus());
    }

    System.Collections.IEnumerator ShowMaskAndStartBonus()
    {
        if (maskImage == null) yield break;

        maskImage.gameObject.SetActive(true);
        maskImage.color = new Color(1, 1, 1, 0);
        float timer = 0f;

        while (timer < maskFadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / maskFadeDuration);
            maskImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        maskImage.color = new Color(1, 1, 1, 1);

        // Пока просто заглушка:
        Debug.Log("Маска появилась! Скоро начнётся бонус...");
        
        yield return new WaitForSecondsRealtime(2f);

        yield return StartCoroutine(HideMask());

        StartCoroutine(BonusGameMode());
    }

    System.Collections.IEnumerator HideMask()
    {
        if (maskImage == null) yield break;

        float timer = 0f;
        Color c = maskImage.color;

        while (timer < maskFadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / maskFadeDuration);
            maskImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        maskImage.gameObject.SetActive(false);
        Debug.Log("Маска скрыта. Бонусный режим активирован!");

        Time.timeScale = 1f;

        StartCoroutine(BonusGameMode());
    }

    System.Collections.IEnumerator BonusGameMode()
    {
        Debug.Log("БОНУСНЫЙ РЕЖИМ АКТИВЕН!");
        // Здесь будет логика бонусного режима
        yield return null;
    }

    public void StopSpawning()
    {
        if (spawner != null)
        {
            spawner.SetActive(false);
        }
    }

    public void ResumeSpawning()
    {
        if (spawner != null)
        {
            spawner.SetActive(true);
        }
    }

}