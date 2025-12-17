using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public string gameplaySceneName = "Gameplay";
    public string mainMenuSceneName = "MainMenu";
    public string gameOverSceneName = "GameOver";
    public string controlsSceneName = "Controls";
    public string optionsSceneName = "Options";
    public string creditsSceneName = "Credits";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Time.timeScale = 1f;
    }

    public void StartGame() => Load(gameplaySceneName);
    public void ShowMainMenu() => Load(mainMenuSceneName);
    public void ShowGameOver() => Load(gameOverSceneName);
    public void ShowControls() => Load(controlsSceneName);
    public void ShowOptions() => Load(optionsSceneName);
    public void ShowCredits() => Load(creditsSceneName);

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void Load(string scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
}
