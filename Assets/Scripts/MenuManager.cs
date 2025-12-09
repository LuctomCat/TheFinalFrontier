using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Tooltip("Exact scene name for gameplay")]
    public string gameplaySceneName = "Gameplay";
    public string mainMenuSceneName = "MainMenu";
    public string gameOverSceneName = "GameOver";

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void ShowMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ShowGameOver()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
