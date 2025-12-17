using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public enum Action
    {
        StartGame,
        MainMenu,
        GameOver,
        Controls,
        Options,
        Credits,
        Quit
    }

    public Action action;

    void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(HandleClick);
    }

    void HandleClick()
    {
        switch (action)
        {
            case Action.StartGame: MenuManager.Instance.StartGame(); break;
            case Action.MainMenu: MenuManager.Instance.ShowMainMenu(); break;
            case Action.GameOver: MenuManager.Instance.ShowGameOver(); break;
            case Action.Controls: MenuManager.Instance.ShowControls(); break;
            case Action.Options: MenuManager.Instance.ShowOptions(); break;
            case Action.Credits: MenuManager.Instance.ShowCredits(); break;
            case Action.Quit: MenuManager.Instance.QuitGame(); break;
        }
    }
}
