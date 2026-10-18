using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Main Scoring")]
    [SerializeField] int targetScore = 10;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject winPanel;
    int score;

    [Header("Spawner Reference")]
    [SerializeField] Spawner spawner;

    [Header("Bonus UI & Tracking")]
    [SerializeField] TMP_Text bonusScoreText;
    [SerializeField] int targetBonusCoins = 50;
    int bonusCoinsCollected = 0;
    bool isBonusActive = false;
    bool redCoinSpawned = false;

    void Awake() => Instance = this;

    void Start()
    {
        UpdateUI();
        if (bonusScoreText != null) bonusScoreText.gameObject.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();

        if (score >= 1 && score <= 10 && !redCoinSpawned)
        {
            redCoinSpawned = true;
            if (spawner != null)
            {
                spawner.SpawnRedCoin();
            }
        }

        if (score >= targetScore) Win();
    }

    public void ActivateBonusMode()
    {
        isBonusActive = true;
        bonusCoinsCollected = 0;
        if (bonusScoreText != null)
        {
            bonusScoreText.gameObject.SetActive(true);
            UpdateBonusUI();
        }
    }

    public void AddBonusScore(int amount)
    {
        bonusCoinsCollected += amount;
        UpdateBonusUI();

        if (bonusCoinsCollected >= targetBonusCoins)
        {
            Win();
        }
    }

    void UpdateUI() => scoreText.text = $"Score: {score} / {targetScore}";

    void UpdateBonusUI()
    {
        if (bonusScoreText != null)
            bonusScoreText.text = $"BonusCoins: {bonusCoinsCollected}/{targetBonusCoins}";
    }

    void Win()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
