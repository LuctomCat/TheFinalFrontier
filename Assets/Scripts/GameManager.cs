using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Barricade barricade;
    public UIManager uiManager;

    // ------ under construction
    [Header("Compatibility")]
    public float barricadeTargetX = 8f; // Used by EnemySpawner
    // ------ under construction

    float elapsedTime;
    bool isGameOver;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (isGameOver) return;

        elapsedTime += Time.deltaTime;
        UpdateUI();

        if (barricade != null && barricade.currentHealth <= 0)
            TriggerGameOver();
    }

    void UpdateUI()
    {
        uiManager?.SetTimer(elapsedTime);
        uiManager?.SetBarricadeHealth(barricade.currentHealth, barricade.maxHealth);

        PlayerController pc = Object.FindAnyObjectByType<PlayerController>();
        if (pc != null)
            uiManager?.SetReload(pc.IsReloading);
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        MenuManager.Instance.ShowGameOver();
    }
}

