using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public Barricade barricade;
    public EnemySpawner spawner;
    public UIManager uiManager;
    public float barricadeTargetX = 8f;

    [Header("Time & Scaling")]
    public float elapsedTime = 0f;
    public float healthScaleEverySeconds = 20f; // every X seconds increase difficulty
    public float healthScaleFactor = 1.15f;     // multiply enemy health by this every interval

    public float speedBaseMultiplier = 1f;

    // internal multiplier (accumulates over time)
    float healthMultiplier = 1f;

    bool isGameOver = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (barricade == null)
            Debug.LogWarning("Barricade not assigned in GameManager.");
        if (spawner == null)
            Debug.LogWarning("Spawner not assigned in GameManager.");
    }

    void Update()
    {
        if (isGameOver) return;

        elapsedTime += Time.deltaTime;
        UpdateScaling();
        UpdateUI();
    }

    void UpdateScaling()
    {
        // simple scaling using how many full intervals have passed
        int intervals = Mathf.FloorToInt(elapsedTime / healthScaleEverySeconds);
        healthMultiplier = Mathf.Pow(healthScaleFactor, intervals);

        if (spawner != null)
        {
            spawner.groupInterval = Mathf.Max(0.4f, 2.0f - (elapsedTime / 60f)); // gradually faster up to a limit
            spawner.groupMax = 1 + Mathf.FloorToInt(elapsedTime / 30f); // increase group size every 30s
            spawner.maxSimultaneousEnemies = 12 + Mathf.FloorToInt(elapsedTime / 10f); // more allowed at later times
        }
    }

    public float GetHealthMultiplier()
    {
        return healthMultiplier;
    }

    // Optionally vary speed per type as time increases
    public float GetSpeedMultiplierForEnemy(Enemy.EnemyType type)
    {
        // fast enemies scale a little more with time to make them threatening
        float extra = (elapsedTime / 120f); // small ramp
        if (type == Enemy.EnemyType.Fast) return 1f + extra * 1.2f;
        return 1f + extra;
    }

    void UpdateUI()
    {
        if (uiManager != null)
            uiManager.SetTimer(elapsedTime);
        if (uiManager != null && barricade != null)
            uiManager.SetBarricadeHealth(barricade.currentHealth, barricade.maxHealth);
    }

    [Header("Events")]
    public UnityEvent onGameOver;

    public void TriggerGameOver()
    {
        if (isGameOver) return;
        GameOver();
        onGameOver?.Invoke();
    }
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("Game Over at " + FormatTime(elapsedTime));
        MenuManager.Instance?.ShowGameOver();
    }

    public static string FormatTime(float seconds)
    {
        int mins = Mathf.FloorToInt(seconds / 60f);
        int secs = Mathf.FloorToInt(seconds % 60f);
        return string.Format("{0:00}:{1:00}", mins, secs);
    }
}
