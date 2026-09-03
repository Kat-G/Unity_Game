using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI highScoreText;

    int score;
    int highScore;

    public ItemSpawner spawner;

    public int bonusInterval = 500;
    private int nextBonusThreshold = 500;    // Следующий порог

    void Awake()
    {
        instance = this;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
	UpdateHighScoreUI();
    }

    public void AddScore(int value)
    {
        score += value;
         
        AchievementManager.Instance.CheckScoreAchievements(score);
	BackgroundManager.Instance.CheckBackgroundUnlock(score);

        scoreText.text = score.ToString();

	if (score % 50 == 0)
        {
            spawner.IncreaseDifficulty();
        }

	if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScoreUI();
        }

        if (score >= nextBonusThreshold && GameManager.instance != null)
        {
            GameManager.instance.StartBonusMode();
            nextBonusThreshold += bonusInterval;
        }
    }

    void UpdateHighScoreUI()
    {
        if (highScoreText != null)
            highScoreText.text = $"Лучший счёт: {highScore}";
    }
}