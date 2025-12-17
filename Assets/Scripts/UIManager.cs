using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI barricadeHealthText;
    public TextMeshProUGUI reloadText;

    public void SetTimer(float time)
    {
        if (timerText != null)
            timerText.text = time.ToString("F2");
    }

    public void SetBarricadeHealth(int current, int max)
    {
        if (barricadeHealthText != null)
            barricadeHealthText.text = $"Health: {current}/{max}";
    }

    public void SetReload(bool reloading)
    {
        if (reloadText != null)
            reloadText.text = reloading ? "RELOADING..." : "";
    }
}
